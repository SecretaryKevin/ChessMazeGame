using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model;

public class Position(int row, int column) : IPosition
{
    public int Row { get; } = row;
    public int Column { get; } = column;
}