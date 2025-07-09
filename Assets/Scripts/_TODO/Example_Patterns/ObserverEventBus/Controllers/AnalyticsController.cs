using System.Collections.Generic;

namespace Game.Features.Observer
{
    public class AnalyticsController : IEventHandler
    {
        private readonly AnalyticsModel _analyticsModel;

        public AnalyticsController(AnalyticsModel analyticsModel)
        {
            _analyticsModel = analyticsModel;
        }

        public void Handle(IGameEvent e)
        {
            switch (e)
            {
                case SpecialGemMatchedEvent sge:
                    Handle(sge);
                    break;
                case ScoreChangedEvent sce:
                    Handle(sce);
                    break;
                case QuestProgressEvent qpe:
                    Handle(qpe);
                    break;
            }
        }

        private void Handle(SpecialGemMatchedEvent gameEvent)
        {
            // collect data
            var eventData = new Dictionary<string, object>();

            _analyticsModel.TrackEvent("special_gem_matched", eventData);
        }

        private void Handle(ScoreChangedEvent gameEvent)
        {
            // collect data
            var eventData = new Dictionary<string, object>();

            _analyticsModel.TrackEvent("score_changed", eventData);
        }

        private void Handle(QuestProgressEvent gameEvent)
        {
            // collect data
            var eventData = new Dictionary<string, object>();

            _analyticsModel.TrackEvent("quest_progress", eventData);

            if (gameEvent.IsCompleted)
            {
                // collect data
                _analyticsModel.TrackEvent("quest_completed", eventData);
            }
        }
    }
}