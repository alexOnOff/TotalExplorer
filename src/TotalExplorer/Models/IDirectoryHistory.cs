namespace TotalExplorer.Models;

internal interface IDirectoryHistory
{
    bool CanMoveBack { get; }
    bool CanMoveForward { get; }

    public string Current {  get; }
    void MoveBack();
    void MoveForward();

    void Add(string node);
    void Remove(string node);
}