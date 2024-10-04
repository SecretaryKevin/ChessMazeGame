using ChessMazeGame.InterfacesAndEnums;

namespace ChessMazeGame.Model;

public class Game : IGame
{
    public ILevel? CurrentLevel { get; private set; }
    public List<ILevel?> AllLevels { get; } = [];
    private bool _isGameOver;
    private DateTime _startTime;
    private DateTime _endTime;

    public void LoadLevel(ILevel aLevel)
    {
        AllLevels.Add(aLevel);
        if (AllLevels.Count == 1)
        {
            CurrentLevel = aLevel;
        }
    }

    public void SetCurrentLevel(int levelNumber)
    {
        if (levelNumber > AllLevels.Count)
        {
            throw new Exception("levelNumber index out of bounds");
        }
        CurrentLevel = AllLevels[levelNumber - 1];
        _isGameOver = false;
        _startTime = DateTime.Now;
    }

    public bool MakeMove(IPosition newPosition)
    {
        bool moveResult = CurrentLevel.MovePlayer(newPosition);
        CheckGameOver();
        return moveResult;
    }

    public bool IsGameOver => _isGameOver;

    public int GetMoveCount()
    {
        return CurrentLevel.GetMoveCount();
    }

    public void Undo()
    {
        CurrentLevel.Undo();
        _isGameOver = false;
    }

    public void Restart()
    {
        CurrentLevel = AllLevels.Find(level => level.Equals(CurrentLevel));
        _isGameOver = false;
        _startTime = DateTime.Now;
    }

    private void CheckGameOver()
    {
        if (CurrentLevel.IsCompleted)
        {
            _isGameOver = true;
            _endTime = DateTime.Now;
        }
    }

    public TimeSpan GetElapsedTime()
    {
        if (!_isGameOver)
        {
            throw new Exception("Game has not ended yet");
        }
        return _endTime - _startTime;
    }

    public List<IPosition> GetAvailableMoves()
    {
        return CurrentLevel.GetAvailableMoves();
    }

    public List<IPosition> GetMoveHistory()
    {
        return CurrentLevel.GetMoveHistory();
    }
}