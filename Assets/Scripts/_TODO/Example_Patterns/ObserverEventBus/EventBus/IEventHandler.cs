namespace Game.Features.Observer
{
    public interface IEventHandler
    {
        void Handle(IGameEvent e);
    }
}