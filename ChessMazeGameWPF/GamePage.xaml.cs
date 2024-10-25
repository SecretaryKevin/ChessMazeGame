using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGameWPF
{
    public partial class GamePage : Page
    {
        private const int CellSize = 50;
        private ViewModel _viewModel;
        private Rectangle _selectedSquare;
        private Brush _originalFill;

        public GamePage()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            DrawBoard();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the MainMenu
            if (NavigationService != null) NavigationService.Navigate(new MainMenu());
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.CurrentLevel))
            {
                DrawBoard();
            }
        }

        private void DrawBoard()
        {
            GameCanvas.Children.Clear();

            var level = _viewModel.CurrentLevel;
            if (level == null)
            {
                throw new Exception("No Levels Found");
            }

            if (level.Board == null)
            {
                throw new Exception("No Board Found");
            }

            for (int row = 0; row < level.Board.Rows; row++)
            {
                for (int column = 0; column < level.Board.Columns; column++)
                {
                    DrawSquare(row, column, level.Board.Cells[row, column]);
                }
            }
        }

        private void DrawSquare(int row, int column, IPiece piece)
        {
            Rectangle square = new Rectangle
            {
                Width = CellSize,
                Height = CellSize,
                Fill = (row + column) % 2 == 0 ? Brushes.LightGray : Brushes.DarkGray,
                Stroke = Brushes.Black,
                Tag = new { Row = row, Column = column } // Store row and column in Tag
            };
            Canvas.SetLeft(square, column * CellSize);
            Canvas.SetTop(square, row * CellSize);
            GameCanvas.Children.Add(square);

            if (piece.Type != PieceType.Empty)
            {
                PlacePieceImage(piece, column, row, square);
            }

            // fill the player square red if the player is on it
            IPosition playerPosition = _viewModel.GetPlayerPosition();
            if (playerPosition.Row == row && playerPosition.Column == column)
            {
                square.Fill = Brushes.Red;
            }

            // if square is the goal square, fill it green
            if (row == _viewModel.CurrentLevel.EndPosition.Row && column == _viewModel.CurrentLevel.EndPosition.Column)
            {
                square.Fill = Brushes.Green;
            }
        }

        private void PlacePieceImage(IPiece piece, int column, int row, Rectangle square)
        {
            // load the image of the piece
            string pieceImageName = piece.Colour.ToString() + '-' + piece.Type + ".png";
            Image pieceImage = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/ChessMazeGameWPF;component/Images/" + pieceImageName)),
                Width = CellSize,
                Height = CellSize
            };
            IPosition playerPostion = _viewModel.GetPlayerPosition();
            if (playerPostion.Row == row && playerPostion.Column == column)
            {
                // add event handler for player piece
                pieceImage.MouseLeftButtonDown += (sender, e) => player_MouseLeftButtonDown(sender, e, square);
            } else
            {
                // add event handler for non-player piece
                pieceImage.MouseLeftButtonDown += (sender, e) => nonPlayer_MouseLeftButtonDown(sender, e, square);
            }
            Canvas.SetLeft(pieceImage, column * CellSize);
            Canvas.SetTop(pieceImage, row * CellSize);
            GameCanvas.Children.Add(pieceImage);
        }

        private void nonPlayer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, Rectangle square)
        {
            if (_selectedSquare == null) return;

            var availableMoves = _viewModel.GetAvailableMoves();
            var position = (dynamic)square.Tag;
            var move = availableMoves.FirstOrDefault(m => m.Row == position.Row && m.Column == position.Column);
            if (move != null)
            {
                _viewModel.MovePlayer(move);
                if (_selectedSquare != null)
                {
                    _selectedSquare.Fill = _originalFill;
                }
                DrawBoard();

                // Update MovesHistory
                _viewModel.MovesHistory += $"Moved to ({move.Row}, {move.Column})\n";
            }
        }

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.UndoMove();
            DrawBoard();
        }

        private void player_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, Rectangle square)
        {
            var position = (dynamic)square.Tag;
            var player = _viewModel.GetPlayerPosition();

            // check if player is selected
            if (player.Row == position.Row && player.Column == position.Column)
            {
                // revert the fill color of the previously selected square if it exists
                if (_selectedSquare != null)
                {
                    _selectedSquare.Fill = _originalFill;
                }

                // store the original fill color of the new selected square
                _selectedSquare = square;
                _originalFill = _selectedSquare.Fill;
                _selectedSquare.Fill = Brushes.LightYellow;
            }

            // get available moves for the selected piece
            var availableMoves = _viewModel.GetAvailableMoves();
            // set the available move squares light blue
            foreach (var move in availableMoves)
            {
                var squareToHighlight = GameCanvas.Children.OfType<Rectangle>().FirstOrDefault(s => ((dynamic)s.Tag).Row == move.Row && ((dynamic)s.Tag).Column == move.Column);
                if (squareToHighlight != null)
                {
                    squareToHighlight.Fill = Brushes.LightBlue;
                }
            }
        }
    }
}