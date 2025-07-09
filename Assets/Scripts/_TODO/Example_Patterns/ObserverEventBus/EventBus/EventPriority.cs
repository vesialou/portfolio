namespace Game.Features.Observer
{
    public enum EventPriority
    {
        Critical = 0,   // Analytics, crash reporting
        High = 1,       // Score, progression
        Normal = 2,     // UI animations, sound
        Low = 3         // Non-essential effects
    }
}