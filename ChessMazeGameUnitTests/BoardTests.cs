using ChessMazeGame.InterfacesAndEnums;
using ChessMazeGame.Model;

namespace ChessMazeGameUnitTests;


public class Position(int row, int column) : IPosition
{
    public int Row { get; } = row;
    public int Column { get; } = column;
}

[TestClass]
public class BoardTests
{
    private Board board;

    [TestInitialize]
    public void TestInitialize()
    {
        board = new Board(8, 8);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly()
    {
        const int rows = 8;
        const int columns = 8;

        Assert.AreEqual(rows, board.Rows);
        Assert.AreEqual(columns, board.Columns);
        Assert.IsNotNull(board.Cells);
        Assert.AreEqual(rows, board.Cells.GetLength(0));
        Assert.AreEqual(columns, board.Cells.GetLength(1));
    }

    [TestMethod]
    public void GetPieceAt_ShouldGetCorrectPiece()
    {
        IPiece piece = new Piece(PieceType.Bishop, PieceColour.Black);
        board.Cells[1, 1] = piece;
        IPosition position = new Position(1, 1);
        var result = board.GetPieceAt(position);
        Assert.AreEqual(piece, result);
    }

    [TestMethod]
    public void PlacePiece_ShouldPlacePieceOnBoard()
    {
        IPiece piece = new Piece(PieceType.Bishop, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));

        Assert.AreEqual(piece, board.GetPieceAt(new Position(1, 1)));
    }

    [TestMethod]
    public void RemovePiece_ShouldRemovePieceOnBoard()
    {
        IPiece piece = new Piece(PieceType.Bishop, PieceColour.Black);
        IPiece expectedPiece = new Piece(PieceType.Empty);
        board.PlacePiece(piece, new Position(1, 1));

        board.RemovePiece(new Position(1, 1));

        Assert.AreEqual(expectedPiece.Type, board.GetPieceAt(new Position(1, 1)).Type);
    }

    [TestMethod]
    public void MovePiece_ShouldMoveBishop()
    {
        IPiece piece = new Piece(PieceType.Bishop, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(2, 2));

        board.MovePiece(new Position(1, 1), new Position(2, 2));

        Assert.AreEqual(PieceType.Bishop, board.GetPieceAt(new Position(2, 2)).Type);
        Assert.AreEqual(PieceType.Empty, board.GetPieceAt(new Position(1, 1)).Type);
    }

    [TestMethod]
    public void MovePiece_ShouldNotMoveBishop()
    {
        IPiece piece = new Piece(PieceType.Bishop, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(2, 3));

        Assert.ThrowsException<Exception>(() => board.MovePiece(new Position(1, 1), new Position(2, 3)));
    }

    [TestMethod]
    public void MovePiece_ShouldMoveKing()
    {
        IPiece piece = new Piece(PieceType.King, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(1, 2));

        board.MovePiece(new Position(1, 1), new Position(1, 2));

        Assert.AreEqual(PieceType.King, board.GetPieceAt(new Position(1, 2)).Type);
        Assert.AreEqual(PieceType.Empty, board.GetPieceAt(new Position(1, 1)).Type);
    }

    [TestMethod]
    public void MovePiece_ShouldNotMoveKing()
    {
        IPiece piece = new Piece(PieceType.King, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(1, 3));

        Assert.ThrowsException<Exception>(() => board.MovePiece(new Position(1, 1), new Position(1, 3)));
    }

    [TestMethod]
    public void MovePiece_ShouldMoveKnight()
    {
        IPiece piece = new Piece(PieceType.Knight, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(3, 2));

        board.MovePiece(new Position(1, 1), new Position(3, 2));

        Assert.AreEqual(PieceType.Knight, board.GetPieceAt(new Position(3, 2)).Type);
        Assert.AreEqual(PieceType.Empty, board.GetPieceAt(new Position(1, 1)).Type);
    }

    [TestMethod]
    public void MovePiece_ShouldNotMoveKnight()
    {
        IPiece piece = new Piece(PieceType.Knight, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(3, 3));

        Assert.ThrowsException<Exception>(() => board.MovePiece(new Position(1, 1), new Position(3, 3)));
    }

    [TestMethod]
    public void MovePiece_ShouldMoveRook()
    {
        IPiece piece = new Piece(PieceType.Rook, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(1, 2));

        board.MovePiece(new Position(1, 1), new Position(1, 2));

        Assert.AreEqual(PieceType.Rook, board.GetPieceAt(new Position(1, 2)).Type);
        Assert.AreEqual(PieceType.Empty, board.GetPieceAt(new Position(1, 1)).Type);
    }

    [TestMethod]
    public void MovePiece_ShouldNotMoveRook()
    {
        IPiece piece = new Piece(PieceType.Rook, PieceColour.Black);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(2, 3));

        Assert.ThrowsException<Exception>(() => board.MovePiece(new Position(1, 1), new Position(1, 3)));
    }

    [TestMethod]
    public void MovePiece_ShouldNotMoveEmptyPiece()
    {
        IPiece piece = new Piece(PieceType.Empty);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(2, 3));

        Assert.ThrowsException<Exception>(() => board.MovePiece(new Position(1, 1), new Position(1, 3)));
    }

    [TestMethod]
    public void MovePiece_ShouldNotMoveToSameColour()
    {
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King, PieceColour.White), new Position(4, 4));
        board.PlacePiece(new Piece(PieceType.King, PieceColour.Black), new Position(2, 2));

        Assert.ThrowsException<Exception>(() => board.MovePiece(new Position(1, 1), new Position(2, 2)));
    }

    [TestMethod]
    public void MovePiece_CantMoveEmptyPiece()
    {
        IPiece piece = new Piece(PieceType.Empty);
        board.PlacePiece(piece, new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King), new Position(2, 3));

        Assert.ThrowsException<Exception>(() => board.MovePiece(new Position(1, 1), new Position(1, 3)));
    }

    [TestMethod]
    public void IsBoardMultiColoured_ShouldReturnTrue()
    {
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King, PieceColour.White), new Position(4, 4));

        Assert.IsTrue(board.IsBoardMultiColoured());
    }

    [TestMethod]
    public void IsBoardMultiColoured_ShouldReturnFalse()
    {
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
        board.PlacePiece(new Piece(PieceType.King, PieceColour.Black), new Position(4, 4));

        Assert.IsFalse(board.IsBoardMultiColoured());
    }

    [TestMethod]
    public void TestBoardInitialization()
    {
        int rows = 8;
        int columns = 8;
        Board board = new Board(rows, columns);

        Assert.AreEqual(rows, board.Rows);
        Assert.AreEqual(columns, board.Columns);
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Assert.AreEqual(PieceType.Empty, board.Cells[row, column].Type);
            }
        }
    }

    [TestMethod]
    public void TestPlacePiece()
    {
        Board board = new Board(8, 8);
        IPiece piece = new Piece(PieceType.Knight, PieceColour.White);
        IPosition position = new Position(0, 0);

        board.PlacePiece(piece, position);

        Assert.AreEqual(piece, board.GetPieceAt(position));
    }

    [TestMethod]
    public void TestRemovePiece()
    {
        Board board = new Board(8, 8);
        IPosition position = new Position(0, 0);
        board.PlacePiece(new Piece(PieceType.Knight, PieceColour.White), position);

        board.RemovePiece(position);

        Assert.AreEqual(PieceType.Empty, board.GetPieceAt(position).Type);
    }
    

    [TestMethod]
    public void TestInvalidMoveThrowsException()
    {
        Board board = new Board(8, 8);
        IPiece piece = new Piece(PieceType.Knight, PieceColour.White);
        IPosition from = new Position(0, 0);
        IPosition to = new Position(3, 3);

        board.PlacePiece(piece, from);

        Assert.ThrowsException<Exception>(() => board.MovePiece(from, to));
    }

    [TestMethod]
    public void TestIsBoardMultiColoured()
    {
        Board board = new Board(8, 8);
        board.PlacePiece(new Piece(PieceType.Knight, PieceColour.White), new Position(0, 0));
        board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));

        Assert.IsTrue(board.IsBoardMultiColoured());
    }
}