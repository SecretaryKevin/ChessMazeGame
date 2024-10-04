using ChessMazeGame.InterfacesAndEnums;
using ChessMazeGame.Model;

namespace ChessMazeGameUnitTests;

[TestClass]
public class PieceTest
{
    [TestMethod]
    public void PieceConstructor_ShouldInitializeCorrectly()
    {
        var piece = new Piece(PieceType.Bishop, PieceColour.Black);
        Assert.AreEqual(piece.Type, PieceType.Bishop);
        Assert.AreEqual(piece.Colour, PieceColour.Black);
    }
}