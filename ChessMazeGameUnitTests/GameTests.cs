using ChessMazeGame.InterfacesAndEnums;
using ChessMazeGame.Model;

namespace ChessMazeGameUnitTests;

[TestClass]
public class GameTests
{
    private IGame game;

    [TestInitialize]
    public void TestInitialize()
    {
        game = new Game();
        game.LoadLevel(new Level(new Board(8, 8), new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false));

    }

    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly()
    {
        Assert.IsNotNull(game);
        Assert.IsNotNull(game.CurrentLevel);
        Assert.IsNotNull(game.AllLevels);
        Assert.AreEqual(1, game.AllLevels.Count);
    }

    [TestMethod]
    public void LoadLevel_ShouldAddLevelToAllLevels()
    {
        ILevel? level = new Level(new Board(8, 8), new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        Assert.AreEqual(2, game.AllLevels.Count);
    }

    [TestMethod]
    public void SetCurrentLevel_ShouldSetCurrentLevel()
    {
        ILevel? level = new Level(new Board(8, 8), new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(2);
        Assert.AreEqual(level, game.CurrentLevel);
    }

    [TestMethod]
    public void SetCurrentLevel_ShouldThrowExceptionIfLevelNumberIsOutOfBounds()
    {
        Assert.ThrowsException<Exception>(() => game.SetCurrentLevel(2));
    }

    [TestMethod]
    public void Restart_ShouldSetCurrentLevelToCurrentLevel()
    {
        ILevel level = new Level(new Board(8, 8), new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(2);
        game.MakeMove(new Position(2, 2));
        game.Restart();
        Assert.AreEqual(level, game.CurrentLevel);
    }

    [TestMethod]
    public void getMoveCount_ShouldReturnCorrectMoveCount()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        game.MakeMove(new Position(2, 2));
        Assert.AreEqual(1, game.GetMoveCount());
    }

    [TestMethod]
    public void Undo_ShouldThrowExceptionIfNoMoves()
    {
        Assert.ThrowsException<Exception>(() => game.Undo());
    }

    [TestMethod]
    public void Undo_ShouldUndoMove()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(3, 3));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        game.MakeMove(new Position(2, 2));
        game.Undo();
        Assert.AreEqual(1, game.GetMoveCount());
    }

    [TestMethod]
    public void GetElapsedTime_ShouldReturnATime()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(3, 3));
        ILevel level = new Level(board, new Position(1, 1), new Position(3, 3), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        game.MakeMove(new Position(2, 2));
        game.MakeMove(new Position(3, 3));
        TimeSpan time = game.GetElapsedTime();
        Assert.IsTrue(time.TotalMilliseconds > 0);
    }

    [TestMethod]
    public void GetElapsedTime_ShouldThrowExceptionIfGameIsNotFinished()
    {
        Assert.ThrowsException<Exception>(() => game.GetElapsedTime());
    }

    [TestMethod]
    public void GetAvailableMoves_ShouldReturnCorrectMoves()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        List<IPosition> availableMoves = game.GetAvailableMoves();
        Assert.AreEqual(1, availableMoves.Count);
    }

    [TestMethod]
    public void GetAvailableMoves_ShouldReturnNoMoves()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(5, 6));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(5,6)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        List<IPosition> availableMoves = game.GetAvailableMoves();
        Assert.AreEqual(0, availableMoves.Count);
    }

    [TestMethod]
    public void CheckGameOver_ShouldSetIsGameOverToFalse()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        Assert.AreEqual(false, game.IsGameOver);
    }

    [TestMethod]
    public void CheckGameOver_ShouldSetIsGameOverToTrue()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), true);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        game.MakeMove(new Position(2, 2));
        Assert.AreEqual(true, game.IsGameOver);
    }

    [TestMethod]
    public void GetMoveHistory_ShouldReturnCorrectMoveHistory()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        game.MakeMove(new Position(2, 2));
        List<IPosition> moveHistory = game.GetMoveHistory();
        Assert.AreEqual(1, moveHistory.Count);
    }

    [TestMethod]
    public void GetMoveHistory_ShouldReturnEmptyMoveHistory()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        List<IPosition> moveHistory = game.GetMoveHistory();
        Assert.AreEqual(0, moveHistory.Count);
    }

    [TestMethod]
    public void MakeMove_ShouldMakeGameOverAfterMove()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        game.MakeMove(new Position(2, 2));
        Assert.AreEqual(true, game.IsGameOver);
    }

    [TestMethod]
    public void MakeMove_ShouldNotMakeGameOverAfterMove()
    {
        IBoard? board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 2));
        ILevel level = new Level(board, new Position(1, 1), new Position(2, 2), new Player(new Position(1,1)), false);
        game.LoadLevel(level);
        game.SetCurrentLevel(game.AllLevels.Count);
        game.MakeMove(new Position(1, 2));
        Assert.AreEqual(false, game.IsGameOver);
    }
}