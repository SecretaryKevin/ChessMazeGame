namespace ChessMazeGame.InterfacesAndEnums;

/// <summary>
/// Represents a level in the Chess Maze game.
/// </summary>
public interface ILevel
{
    /// <summary>
    /// Gets the game board for this level.
    /// </summary>
    IBoard? Board { get; }

    /// <summary>
    /// Gets the start position for this level.
    /// </summary>
    IPosition StartPosition { get; }

    /// <summary>
    /// Gets the end position for this level.
    /// </summary>
    IPosition EndPosition { get; }

    /// <summary>
    /// Gets the player for this level.
    /// </summary>
    IPlayer Player { get; }

    /// <summary>
    /// Determines if the level is completed.
    /// </summary>
    bool IsCompleted { get; }

    /// <summary>
    /// Moves the player to a new position.
    /// <para name="newPosition">The new location of the player</para>
    /// </summary>
    bool MovePlayer(IPosition newPosition);

    /// <summary>
    /// Gets the lenght of MoveHistory in player
    /// </summary>
    int GetMoveCount();

    /// <summary>
    /// pops the last move from the player's MoveHistory and sets the player's current position to the last move
    /// </summary>
    void Undo();

    /// <summary>
    /// Gets the available moves for the player
    /// </summary>
    List<IPosition> GetAvailableMoves();

    /// <summary>
    /// Gets the move history of the player
    /// </summary>
    List<IPosition> GetMoveHistory();

    /// <summary>
    /// Get the piece at the given position
    /// <para name="position">The position of the piece</para>
    /// </summary>
    IPiece GetPieceAt(IPosition position);
}
