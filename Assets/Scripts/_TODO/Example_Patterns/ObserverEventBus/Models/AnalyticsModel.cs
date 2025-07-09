using System.Collections.Generic;

namespace Game.Features.Observer
{
    public class AnalyticsModel
    {
        private readonly Dictionary<string, int> _eventCounts = new();

        public void TrackEvent(string eventName, Dictionary<string, object> parameters)
        {
            // TODO: Send to analytics service 
            _eventCounts[eventName] = _eventCounts.GetValueOrDefault(eventName, 0) + 1;
        }
    }
}