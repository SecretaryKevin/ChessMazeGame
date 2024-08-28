using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model
{
    /// <summary>
    ///   Represents a chess piece with a specific type and Colour.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="colour"></param>
    public class Piece(PieceType type, PieceColour colour = PieceColour.None) : IPiece
    {
        public PieceType Type { get; } = type;
        public PieceColour Colour { get; } = colour;
    }
}