using System;
using System.Collections.Generic;

namespace Events.Impl
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Delegate, Action<object>> _callbackMap = new();
        private readonly Dictionary<Type, List<Action<object>>> _subscribers = new();

        public void Post<T>(T evt)
        {
            var eventType = typeof(T);
            if (!_subscribers.TryGetValue(eventType, out var subscribers))
            {
                return;
            }

            foreach (var subscriber in subscribers)
            {
                subscriber(evt);
            }
        }

        public void On<T>(Action<T> callback)
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers[eventType] = new List<Action<object>>();
            }

            Action<object> action = e => callback((T)e);
            _subscribers[eventType].Add(action);
            _callbackMap[callback] = action;
        }

        public void Off<T>(Action<T> callback)
        {
            var eventType = typeof(T);
            if (!_subscribers.TryGetValue(eventType, out var subscribers))
            {
                return;
            }

            if (_callbackMap.TryGetValue(callback, out var action))
            {
                subscribers.Remove(action);
                _callbackMap.Remove(callback);
            }

            if (_subscribers[eventType].Count == 0)
            {
                _subscribers.Remove(eventType);
            }
        }
    }
}