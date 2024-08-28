using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model;

public class Level(IBoard board, IPosition startPosition, IPosition endPosition, IPlayer player, bool isCompleted) : ILevel
{
    public IBoard Board { get; } = board;
    public IPosition StartPosition { get; } = startPosition;
    public IPosition EndPosition { get; } = endPosition;
    public IPlayer Player { get; } = player;
    public bool IsCompleted { get; } = isCompleted;
}
