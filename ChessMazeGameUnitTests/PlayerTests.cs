using ChessMazeGame.InterfacesAndEnums;
using ChessMazeGame.Model;

namespace ChessMazeGameUnitTests;

[TestClass]
public class PlayerTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly()
    {
        IPlayer player = new Player(new Position(1,1));
        Assert.AreEqual(0, player.MoveHistory.Count);
    }

    [TestMethod]
    public void AddMoveToHistory_ShouldAddMoveToHistory()
    {
        IPlayer player = new Player(new Position(1,1));
        IBoard board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        player.Move(new Position(2,2), board);
        Assert.AreEqual(1, player.MoveHistory.Count);
    }

    [TestMethod]
    public void Move_ShouldMovePiece()
    {
        IPlayer player = new Player(new Position(1,1));
        IBoard board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        player.Move(new Position(2,2), board);
        Assert.AreEqual(2, player.CurrentPosition.Row);
        Assert.AreEqual(2, player.CurrentPosition.Column);
    }

    [TestMethod]
    public void Move_ShouldThrowExceptionIfPieceCannotMove()
    {
        IPlayer player = new Player(new Position(1,1));
        IBoard board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        Assert.ThrowsException<Exception>(() => player.Move(new Position(1,1), board));
    }

}