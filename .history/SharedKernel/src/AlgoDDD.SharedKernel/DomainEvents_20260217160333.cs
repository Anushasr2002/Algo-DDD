using System;
using System.Collections.Generic;
using AlgoDDD.SharedKernel.Domain.BaseClasses;


namespace AlgoDDD.SharedKernel
{
    /// <summary>
    /// Dispatcher for raising and handling domain events.
    /// Allows registering handlers, raising events, and clearing handlers.
    /// </summary>
    public static class DomainEvents
    {
        private static readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public static void Raise<T>(T domainEvent) where T : DomainEvent
        {
            if (domainEvent == null)
                throw new ArgumentNullException(nameof(domainEvent));

            if (_handlers.TryGetValue(typeof(T), out var handlers))
            {
                foreach (var handler in handlers)
                {
                    ((Action<T>)handler)(domainEvent);
                }
            }

            Console.WriteLine($"Event raised: {domainEvent.GetType().Name}");
        }

        public static void Register<T>(Action<T> handler) where T : DomainEvent
        {
            var eventType = typeof(T);
            if (!_handlers.ContainsKey(eventType))
            {
                _handlers[eventType] = new List<Delegate>();
            }
            _handlers[eventType].Add(handler);
        }

        public static void ClearHandlers()
        {
            _handlers.Clear();
        }
    }
}
