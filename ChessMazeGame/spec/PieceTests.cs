using ChessMazeGame.InterfacesAndEnums;
using ChessMazeGame.Model;
using Xunit;

namespace ChessMazeGame.spec
{
    public class PieceTests
    {
        [Fact]
        public void Constructor_ShouldSetPieceType()
        {
            const PieceType expectedType = PieceType.Knight;
            var piece = new Piece(expectedType);
            Assert.Equal(expectedType, piece.Type);
        }
    }
}