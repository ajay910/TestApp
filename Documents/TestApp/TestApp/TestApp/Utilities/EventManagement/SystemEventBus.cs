namespace Aglive.Business.Infrastructure.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SystemEventBus : IEventBus
    {
        private readonly ILog _log;

        //private readonly EventRecordFactory _eventRecordFactory;
        //private readonly ISerializer _serializer;
        private readonly object _publishLock = new object();

        public SystemEventBus(ILog log)
        {
            _log = log;
        }

        private readonly Dictionary<Type, List<object>> _standardSubscriptions = new Dictionary<Type, List<object>>();
        //private readonly Dictionary<Type, List<object>> _eventNotificationSubscriptions = new Dictionary<Type, List<object>>();

        //public void Subscribe<T>(Action<EventNotification<T>> handler)
        //{
        //    try
        //    {
        //        if (_eventNotificationSubscriptions.ContainsKey(typeof(T)) == false)
        //        {
        //            _eventNotificationSubscriptions.Add(typeof(T), new List<object>());
        //        }

        //        _eventNotificationSubscriptions[typeof(T)].Add(handler);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.Error(ex.Message, ex);
        //        throw;
        //    }
        //}

        public void Subscribe<T>(Action<T> handler)
        {
            try
            {
                if (_standardSubscriptions.ContainsKey(typeof(T)) == false)
                {
                    _standardSubscriptions.Add(typeof(T), new List<object>());
                }

                _standardSubscriptions[typeof(T)].Add(handler);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw;
            }
        }

        //public void Subscribe<T>(Action<EventNotification<T>> handler, int cursorPosition)
        //{
        //    try
        //    {
        //        List<EventStreamRecord> missedEvents;
        //        lock (_publishLock)
        //        {
        //            //Replay
        //            missedEvents = _eventRepository.GetRecentEventsForMessageType(cursorPosition,
        //                typeof(T).AssemblyQualifiedName);
        //            Subscribe(handler);
        //        }

        //        Task.Run(() =>
        //        {
        //            lock (handler)
        //            {
        //                foreach (var eventStreamRecord in missedEvents)
        //                {
        //                    handler(new EventNotification<T>(eventStreamRecord.ID,
        //                        (T)_serializer.Deserialize(eventStreamRecord.EventType,
        //                            eventStreamRecord.EventContent)));
        //                }
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.Error(ex.Message, ex);
        //        throw;
        //    }

        //}

        //public void Unsubscribe<T>(Action<EventNotification<T>> handler)
        //{
        //    try
        //    {
        //        if (_eventNotificationSubscriptions.ContainsKey(typeof(T)))
        //        {
        //            _eventNotificationSubscriptions[typeof(T)].Remove(handler);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogManager.Error(ex.Message, ex);
        //        throw;
        //    }
        //}

        public void Unsubscribe<T>(Action<T> handler)
        {
            try
            {
                if (_standardSubscriptions.ContainsKey(typeof(T)))
                {
                    _standardSubscriptions[typeof(T)].Remove(handler);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw;
            }
        }

        public void Publish<T>(T @event)
        {
            Publish(typeof(T), @event);
        }

        public void Publish(Type eventType, object @event)
        {
            //_log.Info($"Publishing message {eventType.FullName}");

            try
            {
                lock (_publishLock)
                {
                    //var eventRecord = _eventRepository.SaveEvent(_eventRecordFactory.Create(@event));

                    if (_standardSubscriptions.ContainsKey(eventType))
                        foreach (var handler in _standardSubscriptions[eventType])
                        {
                            Task.Run(() => { ((Delegate)handler).DynamicInvoke(@event); });
                            //LogManager.Debug($"Event Manager handler registered for {eventType.Name}: {((Delegate)handler).Target}");
                        }

                    //if (_eventNotificationSubscriptions.ContainsKey(eventType))
                    //{
                    //    var genericType = typeof(EventNotification<>).MakeGenericType(eventType);
                    //    var eventNotificationInstance = Activator.CreateInstance(genericType, eventRecord.ID, @event);

                    //    foreach (var handler in _eventNotificationSubscriptions[eventType])
                    //    {
                    //        Task.Run(() => { ((Delegate)handler).DynamicInvoke(eventNotificationInstance); });
                    //    }
                    //}

                    //DebugCommands.LogEventManagerSubscriptions(eventType, _standardSubscriptions, _eventNotificationSubscriptions);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw;
            }
        }
    }
}