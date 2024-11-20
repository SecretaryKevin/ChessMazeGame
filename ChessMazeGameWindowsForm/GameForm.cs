using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGameWindowsForm;

public partial class GameForm : Form
{
    private readonly ViewModel _viewModel;
    private  int _cellSize;
    private Panel _selectedSquare;
    private Color _originalFill;
    public GameForm()
    {
        InitializeComponent();
        _viewModel = new ViewModel();
        _cellSize = 75;
        _viewModel.PropertyChanged += ViewModel_PropertyChanged!;
        _viewModel.PropertyChanged += ViewModel_MoveHistoryChanged!;
        _viewModel.PropertyChanged += ViewModel_MoveCountChanged!;
        Resize += GameForm_Resize!;
        MinimumSize = new Size(800, 600);
        DrawBoard();
    }

    public sealed override Size MinimumSize
    {
        get { return base.MinimumSize; }
        set { base.MinimumSize = value; }
    }

    private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.CurrentLevel))
        {
            DrawBoard();
        }
    }

    private void GameForm_Resize(object sender, EventArgs e)
    {
        // Calculate the available space for the board
        int margin = 20;
        int availableWidth = ClientSize.Width - (moveHistoryTextBox.Width + margin);
        int availableHeight = ClientSize.Height - (margin * 2);

        // Calculate new cell size based on the available space
        int boardSize = Math.Min(availableWidth, availableHeight);
        _cellSize = boardSize / Math.Max(_viewModel.CurrentLevel.Board.Rows, _viewModel.CurrentLevel.Board.Columns);
        DrawBoard();
    }

    private void ViewModel_MoveCountChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.MoveCount))
        {
            moveCountLabel.Text = $"Move Count: {_viewModel.MoveCount}";
        }
    }

    private void ViewModel_MoveHistoryChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ViewModel.MovesHistory))
        {
            moveHistoryTextBox.Text = _viewModel.MovesHistory;
        }
    }

    private void DrawBoard()
    {
        BoardPanel.Controls.Clear();

        var level = _viewModel.CurrentLevel;

        // Check if level is loaded correctly
        if (level == null)
        {
            throw new Exception("No Levels Found");
        }
        if (level.Board == null)
        {
            throw new Exception("No Board Found");
        }

        // Calculate the size of the board panel dynamically
        int boardWidth = level.Board.Columns * _cellSize;
        int boardHeight = level.Board.Rows * _cellSize;

        // Set the size of the board panel
        BoardPanel.Width = boardWidth;
        BoardPanel.Height = boardHeight;

        // Center the BoardPanel within the GameForm
        BoardPanel.Location = new Point(
            (ClientSize.Width - boardWidth) / 2,
            (ClientSize.Height - boardHeight) / 2
        );

        for (int row = 0; row < level.Board.Rows; row++)
        {
            for (int column = 0; column < level.Board.Columns; column++)
            {
                DrawSquare(row, column, level.Board.Cells[row, column]);
            }
        }
        moveHistoryTextBox.Text = _viewModel.MovesHistory;
    }

    private void DrawSquare(int row, int column, IPiece piece)
    {
        Panel square = new Panel
        {
           Width = _cellSize,
           Height = _cellSize,
           BackColor = (row + column) % 2 == 0 ? Color.LightGray  : Color.DarkGray,
           BorderStyle = BorderStyle.FixedSingle,
           Tag = new {Row = row, Column = column}
        };

        square.Location = new Point(column * _cellSize, row * _cellSize);
        BoardPanel.Controls.Add(square);


        // Add piece to square
        if (piece.Type != PieceType.Empty)
        {
            PlacePieceImage(piece, column, row, square);
        }

        // set colour of blank squares
        if (piece != null)
        {
            square.BackColor = piece.Colour == PieceColour.Black ? Color.LightGray : Color.DarkGray;


        //Highlight square if player or goal
        IPosition playerPosition = _viewModel.GetPlayerPosition();
        if (playerPosition.Row == row && playerPosition.Column == column)
        {
            square.BackColor = Color.Red;
        }
        if (row == _viewModel.CurrentLevel.EndPosition.Row && column == _viewModel.CurrentLevel.EndPosition.Column)
        {
            square.BackColor = Color.Green;
        }

        }
    }

    private void PlacePieceImage(IPiece piece, int column, int row, Panel square)
    {
        string pieceImageName = piece.Colour.ToString() + '-' + piece.Type + ".png";
        PictureBox pieceImage = new PictureBox
        {
            Image = Image.FromFile("../../../Images/" + pieceImageName),
            Width = _cellSize,
            Height = _cellSize,
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        pieceImage.Location = new Point(0, 0);
        square.Controls.Add(pieceImage);

        var playerPosition = _viewModel.GetPlayerPosition();
        pieceImage.MouseClick += (sender, e) =>
        {
            if (playerPosition.Row == row && playerPosition.Column == column)
            {
                player_MouseClick(sender, e, square);
            }
            else
            {
                nonPlayer_MouseClick(sender, e, square);
            }
        };
    }

    private void UndoButton_Click(object sender, EventArgs e)
    {
        _viewModel.UndoMove();
        DrawBoard();
    }

    private void nonPlayer_MouseClick(object sender, MouseEventArgs e, Panel square)
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
                _selectedSquare.BackColor = _originalFill;
            }
            DrawBoard();

            // Update MovesHistory
            _viewModel.MovesHistory += $"Moved to ({move.Row}, {move.Column})\n";
        }
    }

    private void BackButton_Click(object sender, EventArgs e)
    {
        _viewModel.NavigateToMainMenu();
    }

    private void player_MouseClick(object sender, MouseEventArgs e, Panel square)
    {
        var position = (dynamic)square.Tag;
        var player = _viewModel.GetPlayerPosition();

        // Check if player is selected
        if (player.Row == position.Row && player.Column == position.Column)
        {
            // Revert the fill color of the previously selected square if it exists
            if (_selectedSquare != null)
            {
                _selectedSquare.BackColor = _originalFill;
            }

            // Store the original fill color of the new selected square
            _selectedSquare = square;
            _originalFill = _selectedSquare.BackColor;
            _selectedSquare.BackColor = Color.LightYellow;
        }

        // Highlight available move squares
        var availableMoves = _viewModel.GetAvailableMoves();
        foreach (var move in availableMoves)
        {
            var squareToHighlight = BoardPanel.Controls
                .OfType<Panel>()
                .FirstOrDefault(s => ((dynamic)s.Tag).Row == move.Row && ((dynamic)s.Tag).Column == move.Column);
            if (squareToHighlight != null)
            {
                squareToHighlight.BackColor = Color.LightBlue;
            }
        }
    }
}