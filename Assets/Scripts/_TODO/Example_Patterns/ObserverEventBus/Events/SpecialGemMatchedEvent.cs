using System;

namespace Game.Features.Observer
{
    public class SpecialGemMatchedEvent : IGameEvent
    {
        public GemType GemType { get; }
        public int ComboMultiplier { get; }
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public SpecialGemMatchedEvent(GemType gemType, int comboMultiplier)
        {
            GemType = gemType;
            ComboMultiplier = comboMultiplier;
        }
    }
}