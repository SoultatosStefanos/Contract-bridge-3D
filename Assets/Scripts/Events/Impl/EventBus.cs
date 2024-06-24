using System;
using System.Collections.Generic;

namespace Events.Impl
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Action<object>>> _subscribers = new();

        public void Post<T>(T evt)
        {
            var eventType = typeof(T);
            if (!_subscribers.TryGetValue(eventType, out var subscriber1)) return;

            foreach (var subscriber in subscriber1) subscriber(evt);
        }

        public void On<T>(Action<T> callback)
        {
            var eventType = typeof(T);
            if (!_subscribers.ContainsKey(eventType)) _subscribers[eventType] = new List<Action<object>>();

            _subscribers[eventType].Add(e => callback((T)e));
        }

        public void Off<T>(Action<T> callback)
        {
            var eventType = typeof(T);
            if (!_subscribers.TryGetValue(eventType, out var subscriber)) return;

            subscriber.Remove(e => callback((T)e));

            if (_subscribers[eventType].Count == 0) _subscribers.Remove(eventType);
        }
    }
}