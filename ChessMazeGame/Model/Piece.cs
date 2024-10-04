using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model
{
    public class Piece(PieceType type, PieceColour colour = PieceColour.None) : IPiece
    {
        public PieceType Type { get; } = type;
        public PieceColour Colour { get; } = colour;
    }
}