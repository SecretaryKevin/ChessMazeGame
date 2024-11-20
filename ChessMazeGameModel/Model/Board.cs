using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model;

public class Board : IBoard
{

    public int Rows { get; }
    public int Columns { get; }
    public IPiece[,] Cells { get; }

    public Board(int rows, int columns)
    {
        // cells equals a 2D array of size rows x columns of Piece objects
        Cells = new IPiece[rows, columns];
        Rows = rows;
        Columns = columns;

        // Initialize the board with empty pieces
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                Cells[row, column] = new Piece(PieceType.Empty);
            }
        }
    }

    public IPiece GetPieceAt(IPosition position)
    {
        return Cells[position.Row, position.Column];
    }

    public void PlacePiece(IPiece piece, IPosition position)
    {
        Cells[position.Row, position.Column] = piece;
    }

    public void RemovePiece(IPosition position)
    {
        Cells[position.Row, position.Column] = new Piece(PieceType.Empty);
    }

    public void MovePiece(IPosition from, IPosition to)
    {
        if (IsValidPosition(to))
        {
            try
            {
                if (IsMoveLegal(from, to))
                {
                    PlacePiece(GetPieceAt(from), to);
                    RemovePiece(from);
                }
                else
                {
                    throw new Exception("Move is not legal.");
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it, show a message to the user, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        else
        {
            throw new Exception("Invalid position.");
        }
    }

    public bool IsValidPosition(IPosition position)
    {
        // checks if the position exists in the 2d array of cell
        return position.Row >= 0 && position.Row < Rows && position.Column >= 0 && position.Column < Columns;
    }

    public bool IsMoveLegal(IPosition from, IPosition to)
    {
        IPiece fromPiece = GetPieceAt(from);
        IPiece toPiece = GetPieceAt(to);

        if (IsMoveToEmptySpace(toPiece))
        {
            return false;
        }

        if (IsMoveToSameColourPieceOnMultiColouredBoard(fromPiece, toPiece))
        {
            return false;
        }


        return IsMoveLegalBasedOnPieceType(fromPiece, from, to);
    }

    private bool IsMoveToEmptySpace(IPiece toPiece)
    {
        return toPiece.Type == PieceType.Empty;
    }

    private bool IsMoveToSameColourPieceOnMultiColouredBoard(IPiece fromPiece, IPiece toPiece)
    {
        return IsBoardMultiColoured() && fromPiece.Colour == toPiece.Colour;
    }

    private bool IsMoveLegalBasedOnPieceType(IPiece fromPiece, IPosition from, IPosition to)
    {
        switch (fromPiece.Type)
        {
            case PieceType.Bishop:
                // check if the move on a diangonal and is only 1 space
                return Math.Abs(from.Row - to.Row) == Math.Abs(from.Column - to.Column) ;
            case PieceType.King:
                return Math.Abs(from.Row - to.Row) <= 1 && Math.Abs(from.Column - to.Column) <= 1;

            case PieceType.Knight:
                return (Math.Abs(from.Row - to.Row) == 2 && Math.Abs(from.Column - to.Column) == 1) ||
                       (Math.Abs(from.Row - to.Row) == 1 && Math.Abs(from.Column - to.Column) == 2);

            case PieceType.Rook:
                // Check if the move is either vertical or horizontal and is only move 1 space
                return (from.Row == to.Row && Math.Abs(from.Column - to.Column) == 1) ||
                       (from.Column == to.Column && Math.Abs(from.Row - to.Row) == 1);

            case PieceType.Empty:
                throw new Exception("Piece type is empty, move is not legal.");

            default:
                throw new Exception("Piece type wasn't in switch case.");
        }
    }

    public bool IsBoardMultiColoured()
    {
        bool hasBlack = false;
        bool hasWhite = false;

        // Iterate through the board
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                IPiece piece = Cells[row, column];
                // Check if the piece is not null and check its color
                if (piece != null)
                {
                    if (piece.Colour == PieceColour.Black)
                    {
                        hasBlack = true;
                    }
                    else if (piece.Colour == PieceColour.White)
                    {
                        hasWhite = true;
                    }

                    // If both black and white are found, return true
                    if (hasBlack && hasWhite)
                    {
                        return true;
                    }
                }
            }
        }
        // If both colors are not found, return false
        return false;
    }
}