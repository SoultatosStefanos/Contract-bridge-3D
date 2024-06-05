using System;

namespace Events
{
    public interface IEventBus
    {
        void Post<T>(T evt);

        void On<T>(Action<T> callback);

        void Off<T>(Action<T> callback);
    }
}