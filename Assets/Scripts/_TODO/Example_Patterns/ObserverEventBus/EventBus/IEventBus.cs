using System;

namespace Game.Features.Observer
{
    public interface IEventBus
    {
        void Subscribe(Type eventType, IEventHandler handler, EventPriority priority);
        void Unsubscribe(Type eventType, IEventHandler handler);
        void Publish(IGameEvent gameEvent);
        void Clear();
    }
}