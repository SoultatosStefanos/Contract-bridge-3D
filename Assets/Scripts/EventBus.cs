using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, List<Action<object>>> Subscribers =
        new Dictionary<Type, List<Action<object>>>();

    public static void Post<T>(T evt)
    {
        var eventType = typeof(T);
        if (!Subscribers.TryGetValue(eventType, out var subscriber1)) return;

        foreach (var subscriber in subscriber1) subscriber(evt);
    }

    public static void On<T>(Action<T> callback)
    {
        var eventType = typeof(T);
        if (!Subscribers.ContainsKey(eventType)) Subscribers[eventType] = new List<Action<object>>();

        Subscribers[eventType].Add(e => callback((T)e));
    }

    public static void Off<T>(Action<T> callback)
    {
        var eventType = typeof(T);
        if (!Subscribers.TryGetValue(eventType, out var subscriber)) return;

        subscriber.Remove(e => callback((T)e));

        if (Subscribers[eventType].Count == 0) Subscribers.Remove(eventType);
    }
}