namespace Game.Features.ChainOfResponsibility
{
    public abstract class LevelAdaptationHandler
    {
        protected LevelAdaptationHandler nextHandler;

        public void SetNext(LevelAdaptationHandler handler)
        {
            nextHandler = handler;
        }

        public virtual void Handle(ref LevelData level)
        {
            ProcessLevel(ref level);
            nextHandler?.Handle(ref level);
        }

        protected abstract void ProcessLevel(ref LevelData level);
    }
}
