using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model
{
    public class Level(IBoard? board, IPosition startPosition, IPosition endPosition, IPlayer player, bool isCompleted) : ILevel
    {
        public IBoard? Board { get; } = board;
        public IPosition StartPosition { get; } = startPosition;
        public IPosition EndPosition { get; } = endPosition;
        public IPlayer Player { get; } = player;
        public bool IsCompleted { get; private set; } = isCompleted;

        public bool MovePlayer(IPosition newPosition)
        {
            try
            {
                Player.Move(newPosition, Board);
                if (newPosition.Row == EndPosition.Row && newPosition.Column == EndPosition.Column)
                {
                    IsCompleted = true;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetMoveCount()
        {
            return Player.MoveHistory.Count;
        }

        public void Undo()
        {
            if (Player.MoveHistory.Count > 0)
            {
                // If the player has only made one move, then the player should be moved back to the start position
                if (Player.MoveHistory.Count == 1)
                {
                    Player.CurrentPosition = StartPosition;
                    Player.MoveHistory.Clear(); // Clear the move history
                }
                else
                {
                    Player.UndoMove();
                }
            }
            else
            {
                throw new Exception("No moves to undo");
            }
        }

        public List<IPosition> GetAvailableMoves()
        {
            return Player.GetAvailableMoves(Board);
        }

        public List<IPosition> GetMoveHistory()
        {
            return Player.MoveHistory;
        }

        public IPiece GetPieceAt(IPosition position)
        {
            return Board.GetPieceAt(position);
        }

        public IPosition GetPlayerPosition()
        {
            return Player.CurrentPosition;
        }
    }
}