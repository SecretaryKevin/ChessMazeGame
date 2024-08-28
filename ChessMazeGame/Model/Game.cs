using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model;

public class Game : IGame
{
    public ILevel CurrentLevel { get; }
    public void LoadLevel(ILevel aLevel)
    {
        throw new NotImplementedException();
    }

    public bool MakeMove(IPosition newPosition)
    {
        throw new NotImplementedException();
    }

    public bool IsGameOver { get; }
    public int GetMoveCount()
    {
        throw new NotImplementedException();
    }

    public void Undo()
    {
        throw new NotImplementedException();
    }

    public void Restart()
    {
        throw new NotImplementedException();
    }
}