namespace Game.Features.CommandMemento
{

    public interface IUndoableCommand : ICommand
    {
        string Label { get; }
        void Undo();
        bool CanExecute();
    }
}