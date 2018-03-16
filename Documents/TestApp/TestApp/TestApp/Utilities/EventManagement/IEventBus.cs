namespace Aglive.Business.Infrastructure.Utilities
{
    using System;

    public interface IEventBus
    {
        //void Subscribe<T>(Action<EventNotification<T>> handler);
        void Subscribe<T>(Action<T> handler);

        //void Subscribe<T>(Action<EventNotification<T>> handler, int cursorPosition);
        //void Unsubscribe<T>(Action<EventNotification<T>> handler);
        void Unsubscribe<T>(Action<T> handler);

        void Publish<T>(T @event);

        void Publish(Type eventType, object @event);
    }
}