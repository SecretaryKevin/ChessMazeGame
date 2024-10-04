namespace ChessMazeGame.InterfacesAndEnums;

/// <summary>
/// Represents a chess piece with a specific type.
/// </summary>
public interface IPiece
{
    /// <summary>
    /// Gets the type of the chess piece.
    /// </summary>
    PieceType Type { get; }
    /// <summary>
    /// Gets the colour of the chess peice
    /// </summary>
    PieceColour Colour { get; }
}