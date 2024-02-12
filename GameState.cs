using MoonActive.Scripts;

public class GameState
{
private int[][] _game;
private PlayerType _playerO, _playerX;
private bool _turn;
private int _numOfMarks;

public GameState(int rows, int colummns)
    {
        _game = new int[rows][colummns];
        _playerO = new PlayerType(true);
        _playerX = new PlayerType(false);
        _numOfMarks = 0;
    }

}