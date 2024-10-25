using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ChessMazeGame.InterfacesAndEnums;
using ChessMazeGame.Model;

namespace ChessMazeGameWPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Board? _board;
        private Game _model;
        private int _moveCount;
        private Level _currentLevel;
        private string _movesHistory;

        public ViewModel()
        {
            _model = new Game();
            LoadData();
            MoveCount = 0; // Initialize move count
        }

        public Level CurrentLevel
        {
            get => _currentLevel;
            set
            {
                _currentLevel = value;
                OnPropertyChanged();
            }
        }
        public string MovesHistory
        {
            get => _movesHistory;
            set
            {
                _movesHistory = value;
                OnPropertyChanged();
            }
        }
        public int MoveCount
        {
            get => _moveCount;
            set
            {
                _moveCount = value;
                OnPropertyChanged();
            }
        }

        public void LoadData()
        {
            // creates levels then passes them to the model
            // Create the board
            _board = new Board(8, 8);
            // Row 0
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(0, 0));
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(0, 1));
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(0, 2));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(0, 3));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(0, 4));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(0, 5));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(0, 6));
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(0, 7));

            // Row 1
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(1, 0));
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(1, 1));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(1, 2));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(1, 3));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(1, 5));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(1, 7));

            // Row 2
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 0));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(2, 1));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(2, 2));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(2, 3));
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(2, 4));
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 5));
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(2, 6));

            // Row 3
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(3, 0));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(3, 1));
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(3, 2));
            _board.PlacePiece(new Piece(PieceType.King, PieceColour.Black), new Position(3, 3));
            _board.PlacePiece(new Piece(PieceType.King, PieceColour.Black), new Position(3, 4));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(3, 5));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(3, 6));
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(3, 7));

            // Row 4
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(4, 0));
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(4, 1));
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(4, 2));
            _board.PlacePiece(new Piece(PieceType.King, PieceColour.Black), new Position(4, 3));
            _board.PlacePiece(new Piece(PieceType.King, PieceColour.Black), new Position(4, 4));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(4, 6));

            // Row 5
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(5, 0));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(5, 1));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(5, 2));
            _board.PlacePiece(new Piece(PieceType.Rook, PieceColour.Black), new Position(5, 4));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(5, 7));

            // Row 6
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(6, 0));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(6, 1));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(6, 2));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(6, 3));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(6, 4));
            _board.PlacePiece(new Piece(PieceType.Bishop, PieceColour.Black), new Position(6, 6));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(6, 7));

            // Row 7
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(7, 0));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(7, 3));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(7, 4));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(7, 5));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(7, 6));
            _board.PlacePiece(new Piece(PieceType.Knight, PieceColour.Black), new Position(7, 7));

            CurrentLevel = new Level(_board, new Position(0, 0), new Position(7, 7),
                new Player(new Position(0, 0)), false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public IPosition GetPlayerPosition()
        {
            return CurrentLevel.GetPlayerPosition();
        }

        public List<IPosition> GetAvailableMoves()
        {
            return CurrentLevel.GetAvailableMoves();
        }

        public void MovePlayer(IPosition newPosition)
        {
            CurrentLevel.MovePlayer(newPosition);
            MoveCount = CurrentLevel.GetMoveCount(); // Update move count
            // if the player has reached the goal give them a popup
            if (CurrentLevel.IsCompleted)
            {
                MessageBox.Show($"Congratulations! You have reached the goal! using {MoveCount} moves.");
                // move to the next level
                if (_model.GetCurrentLevelNumber() < _model.GetLevelCount())
                    _model.SetCurrentLevel(_model.GetCurrentLevelNumber() + 1);
                else
                {
                    MessageBox.Show("You have completed all levels!");
                    _model.AllLevels.Clear();
                    LoadData();
                    NavigateToMainMenu();
                }
            }
        }

        private void NavigateToMainMenu()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.MainFrame.Navigate(new MainMenu());
        }

        public void UndoMove()
        {
            if (CurrentLevel.GetMoveCount() == 0)
            {
                MessageBox.Show("No moves to undo");
                return;
            }
            CurrentLevel.Undo();
            MoveCount = CurrentLevel.GetMoveCount(); // Update move count

            // Remove the last move from the move history
            if (!string.IsNullOrEmpty(MovesHistory))
            {
                var moves = MovesHistory.Trim().Split('\n').ToList();
                if (moves.Any())
                {
                    moves.RemoveAt(moves.Count - 1);
                    MovesHistory = string.Join("\n", moves) + "\n";
                }
            }
        }
    }
}