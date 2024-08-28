using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model;

public class Board : IBoard
{
    public Board(int rows, int columns)
    {
        // cells equals a 2D array of size rows x columns of Piece objects
        Cells = new IPiece[rows, columns];
        Rows = rows;
        Columns = columns;
    }

    public int Rows { get; }
    public int Columns { get; }
    public IPiece[,] Cells { get; }

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
            if (IsMoveLegal(from, to))
            {
                RemovePiece(from);
                PlacePiece(GetPieceAt(from), to);
            }
            else
            {
                throw new Exception("Move is not legal");
            }
        }
        else
        {
            throw new Exception("Invalid Position");
        }
    }

    public bool IsValidPosition(IPosition position)
    {
        // checks if the position exists in the 2d array of cell
         return position.Row >= 0 && position.Row < Rows &&
            position.Column >= 0 && position.Column < Columns;
    }

    public bool IsMoveLegal(IPosition from, IPosition to)
    {
        throw new NotImplementedException();
    }
}