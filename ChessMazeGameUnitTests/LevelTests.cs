using ChessMazeGame.InterfacesAndEnums;
using ChessMazeGame.Model;
namespace ChessMazeGameUnitTests;

[TestClass]
public class LevelTests
{
    [TestMethod]
    public void Level_ShouldInitializeCorrectly()
    {
        IBoard board = new Board(8, 8);
        IPosition startPosition = new Position(0, 0);
        IPosition endPosition = new Position(7, 7);
        IPlayer player = new Player(new Position(0,0));
        bool isCompleted = false;

        ILevel level = new Level(board, startPosition, endPosition, player, isCompleted);

        Assert.AreEqual(board, level.Board);
        Assert.AreEqual(startPosition, level.StartPosition);
        Assert.AreEqual(endPosition, level.EndPosition);
        Assert.AreEqual(player, level.Player);
        Assert.AreEqual(isCompleted, level.IsCompleted);
    }

    [TestMethod]
    public void Undo_ShouldThrowExceptionIfNoMovesToUndo()
    {
        IBoard board = new Board(8, 8);
        IPosition startPosition = new Position(0, 0);
        IPosition endPosition = new Position(7, 7);
        IPlayer player = new Player(new Position(0,0));
        bool isCompleted = false;

        ILevel level = new Level(board, startPosition, endPosition, player, isCompleted);

        Assert.ThrowsException<Exception>(() => level.Undo());
    }

    [TestMethod]
    public void Undo_ShouldUndoLastMove()
    {
        IBoard board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(0, 0));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));

        IPosition startPosition = new Position(0, 0);
        IPosition endPosition = new Position(7, 7);
        IPlayer player = new Player(new Position(0,0));
        bool isCompleted = false;

        ILevel level = new Level(board, startPosition, endPosition, player, isCompleted);

        player.Move(new Position(1, 1), board);
        level.MovePlayer(new Position(1, 1));
        player.Move(new Position(2, 2), board);
        level.Undo();

        Assert.AreEqual(1, player.CurrentPosition.Row);
        Assert.AreEqual(1, player.CurrentPosition.Column);
    }

    [TestMethod]
    public void GetMoveCount_ShouldReturnCorrectMoveCount()
    {
        IBoard board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(0, 0));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));

        IPosition startPosition = new Position(0, 0);
        IPosition endPosition = new Position(7, 7);
        IPlayer player = new Player(new Position(0,0));
        bool isCompleted = false;

        ILevel level = new Level(board, startPosition, endPosition, player, isCompleted);
        ;
        level.MovePlayer(new Position(1, 1));
        level.MovePlayer(new Position(2, 2));

        Assert.AreEqual(2, level.GetMoveCount());
    }
}