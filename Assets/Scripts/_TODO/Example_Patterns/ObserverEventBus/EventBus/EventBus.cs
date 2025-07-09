using System;
using System.Collections.Generic;

namespace Game.Features.Observer
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, HandlerGroup> _handlers = new();
        private readonly Queue<IGameEvent> _eventQueue = new();
        private bool _isProcessing = false;

        public void Subscribe(Type eventType, IEventHandler handler, EventPriority priority)
        {
            if (!_handlers.TryGetValue(eventType, out var group))
            {
                group = _handlers[eventType] = new HandlerGroup();
            }

            group.Add(handler, priority);
        }

        public void Unsubscribe(Type eventType, IEventHandler handler)
        {
            if (_handlers.ContainsKey(eventType))
            {
                _handlers[eventType].Remove(handler);
                if (_handlers[eventType].IsEmpty())
                {
                    _handlers.Remove(eventType);
                }
            }
        }

        public void Publish(IGameEvent gameEvent)
        {
            if (_isProcessing)
            {
                _eventQueue.Enqueue(gameEvent);
                return;
            }

            ProcessEvent(gameEvent);
            ProcessQueuedEvents();
        }

        public void Clear()
        {
            _handlers.Clear();
            _eventQueue.Clear();
        }

        private void ProcessEvent(IGameEvent gameEvent)
        {
            _isProcessing = true;

            try
            {
                var type = gameEvent.GetType();
                if (_handlers.TryGetValue(type, out var group))
                {
                    group.ExecuteAll(gameEvent);
                }
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private void ProcessQueuedEvents()
        {
            while (_eventQueue.Count > 0)
            {
                var next = _eventQueue.Dequeue();
                ProcessEvent(next);
            }
        }

        private sealed class HandlerGroup
        {
            private const int catagoryLength = 4;
            private readonly List<IEventHandler>[] _handlersByPriority = new List<IEventHandler>[catagoryLength];
            private readonly int[] _counts = new int[catagoryLength];

            public bool IsEmpty()
            {
                for(var i = 0; i < catagoryLength; i++)
                {
                    if (_counts[i] != 0)
                    {
                        return false; // still has active handlers
                    }
                }

                return true;
            }

            public void Add(IEventHandler handler, EventPriority priority)
            {
                var index = (int)priority;
                _handlersByPriority[index] ??= new();
                _handlersByPriority[index].Add(handler);
            }

            public void Remove(IEventHandler handler)
            {
                for (var i = 0; i < catagoryLength; i++)
                {
                    var list = _handlersByPriority[i];

                    if (list != null && list.Remove(handler))
                    {
                        _counts[i]--;
                        if (_counts[i] == 0)
                        {
                            _handlersByPriority[i] = null;
                        }

                        return;
                    }
                }
            }

            public void ExecuteAll(IGameEvent gameEvent)
            {
                for (var i = 0; i < catagoryLength; i++)
                {
                    var list = _handlersByPriority[i];
                    if (list == null)
                    {
                        continue;
                    }

                    for (int j = 0; j < list.Count; j++)
                    {
                        IEventHandler handler = list[j];
                        handler.Handle(gameEvent);
                    }
                }
            }
        }
    }
}