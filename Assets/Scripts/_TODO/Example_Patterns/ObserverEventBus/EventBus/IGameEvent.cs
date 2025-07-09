using System;

namespace Game.Features.Observer
{
    public interface IGameEvent
    {
        DateTime Timestamp { get; }
    }
}