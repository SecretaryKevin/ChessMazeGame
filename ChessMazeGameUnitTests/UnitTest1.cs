using ChessMazeGame.InterfacesAndEnums;
using ChessMazeGame.Model;

namespace ChessMazeGameUnitTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly()
    {
        const int rows = 8;
        const int columns = 8;

        var board = new Board(rows, columns);

        Assert.AreEqual(rows, board.Rows);
        Assert.AreEqual(columns, board.Columns);
        Assert.IsNotNull(board.Cells);
        Assert.AreEqual(rows, board.Cells.GetLength(0));
        Assert.AreEqual(columns, board.Cells.GetLength(1));
    }

    [TestMethod]
    public void GetPieceAt_ShouldGetCorrectPiece()
    {
        // Arrange
        IPiece piece = new Piece(PieceType.Bishop, PieceColour.Black);
        const int rows = 8;
        const int columns = 8;
        var board = new Board(rows, columns);

        board.Cells[1, 1] = piece;

        // Inline implementation of IPosition
        IPosition position = new CustomPosition(1, 1);

        // Act
        var result = board.GetPieceAt(position);

        // Assert
        Assert.AreEqual(piece, result);
    }


    public class CustomPosition : IPosition
    {
        public CustomPosition(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }
    }
}