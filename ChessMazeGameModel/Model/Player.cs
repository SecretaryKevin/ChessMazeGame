using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model
{
    public class Player(IPosition currentPosition) : IPlayer
    {
        public IPosition CurrentPosition { get; set; } = currentPosition;
        public List<IPosition> MoveHistory { get; set; } = [];

        public bool CanMove(IPosition newPosition, IBoard? board)
        {
            if (CurrentPosition.Row == newPosition.Row && CurrentPosition.Column == newPosition.Column)
            {
                // The player cannot move to the position it is already on
                return false;
            }
            return board.IsValidPosition(newPosition) && board.IsMoveLegal(CurrentPosition, newPosition);
        }

        public void Move(IPosition newPosition, IBoard? board)
        {
            // Requests move then if confirmed work update player and history else throw exception
            if (CanMove(newPosition, board))
            {
                MoveHistory.Add(newPosition);
                CurrentPosition = newPosition;
            }
            else
            {
                throw new Exception("Move is not legal");
            }
        }

        public void UndoMove()
        {
            // Set the current position to the second to last move in the move history
            CurrentPosition = MoveHistory[^2];
            MoveHistory.RemoveAt(MoveHistory.Count - 1);
        }

        public List<IPosition> GetAvailableMoves(IBoard? board)
        {
            // Loops Through all locations on the board and checks if the player can move to that location
            // If the player can move to that location, it adds it to the list of available moves
            // Returns the list of available moves
            List<IPosition> availableMoves = [];
            for (int row = 0; row < board.Rows; row++)
            {
                for (int column = 0; column < board.Columns; column++)
                {
                    IPosition newPosition = new Position(row, column);
                    if (CanMove(newPosition, board))
                    {
                        availableMoves.Add(newPosition);
                    }
                }
            }
            return availableMoves;
        }
    }
}