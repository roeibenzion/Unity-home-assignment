using MoonActive.Scripts;
using System;
using System.Collections.Generic;

public class GameBoardLogic
{
    private GameView _gameView;
    private UserActionEvents _userActionEvents;

    /*
    1 is O, 2 is X.
    */
    private int [][] _game;

    private PlayerType _playerO, playerX; 
    private bool _turn;

    private int _numOfMarks;

    private GameState _gameState;
    
    public GameBoardLogic(GameView gameView, UserActionEvents userActionEvents)
    {
        /*
         * Constructor
         */
        _gameView = gameView;
        _userActionEvents = userActionEvents;
    }

    public void Initialize(int columns, int rows)
    {
        /*
         * Initialization logic will be called once per session
         */

         //Create a new game state
        _game = new int[columns][rows];
        _playerO = new PlayerType(true);
        _playerX = new PlayerType(false);
        _turn = true;
        _numOfMarks = 0;
        _gameState = new GameState(_game, _playerO, _playerX, _turn, _numOfMarks);
        //Initialize the game view
        _gameView.Initialize(columns, rows);
    }
    
    public void DeInitialize()
    {
        /*
         * DeInitialization logic will be called once per session, at disposal
         */
    }

    /*
     * Event handlers
     */
    public void startGameClicked()
    {
        /*
        * Start game logic
        */
        _userActionEvents.statGameClicked();
        //using  _currentTurnText.text = player == PlayerType.PlayerO ? "Player 0 Turn" : "Player X Turn";
        Random rnd = new Random();
        //Throw a coin to decide starter
        bool whoStarts = (rnd.Next(0, 2) == 1) ? true : false;
        this._turn = whoStarts;
        if (whoStarts)
        {
            _gameView.StartGame(_playerO);
        }
        else
        {
            _gameView.StartGame(_playerX);
        }
    }

    public void TileClicked(int x, int y)
    {
        /*
        * Tile clicked logic
        */
        this.game[x][y] = (_turn) ? 1 : 2;
        _gameView.SetTileSign(this._playerType, new BoardTilePosition(x, y));
        _numOfMarks++;

        //check for a winner
        val = IsWinnerExists();
        if (val == 1)
        {
            _gameView.GameWon(_playerO);
        }
        else if (val == 2)
        {
            _gameView.GameWon(_playerX);
        }
        else if (val == 3)
        {
            _gameView.GameTie();
        }
        else
        {
            //change the turn
            _turn = !_turn;
            if (_turn)
            {
                _gameView.ChangeTurn(_playerO);
            }
            else
            {
                _gameView.ChangeTurn(_playerX);
            }
        }
        
    }

    public void SaveStateClicked(StorageType storageType)
    {
        // Determine the storage type and save the game state accordingly
        storageType.SaveGameState(_gameState);
    }

    public void LoadStateClicked(StorageType storageType)
    {
    // Load game state from the specified storage type
    GameState savedGameState = storageType.LoadGameState();

    if (savedGameState != null)
        {
            // Load the saved game state
            this._game = savedGameState.Game;
            this._playerO = savedGameState.PlayerO;
            this._playerX = savedGameState.PlayerX;
            this._turn = savedGameState.Turn;
            this._numOfMarks = savedGameState.NumOfMarks;

            // Update the view state based on the loaded game state
            SetupInitialViewState(_game.GetLength(0), _game.GetLength(1));
        }
    }
    private int IsWinnerExists(int[,] matrix)
    {
        /*
        Returns 1 if player O wins, 2 if player X wins, 3 if tie, 0 if no winner yet.
        */
        int size = matrix.GetLength(0);

        // Check rows and columns for a winner
        for (int i = 0; i < size; i++)
        {
            // Check rows
            if (matrix[i, 0] != 0 && matrix[i, 0] == matrix[i, 1] && matrix[i, 0] == matrix[i, 2])
                return matrix[i, 0];

            // Check columns
            if (matrix[0, i] != 0 && matrix[0, i] == matrix[1, i] && matrix[0, i] == matrix[2, i])
                return matrix[0, i];
        }

        // Check diagonals for a winner
        if (matrix[0, 0] != 0 && matrix[0, 0] == matrix[1, 1] && matrix[0, 0] == matrix[2, 2])
            return matrix[0, 0];

        if (matrix[0, 2] != 0 && matrix[0, 2] == matrix[1, 1] && matrix[0, 2] == matrix[2, 0])
            return matrix[0, 2];

        //check for a tie
        if (IsFull()) 
        // switch to (IsTie()) when finishing the data structure 
        {
            return 3;
        }
        // No winner found
        return 0;
    }

    private bool IsFull()
    {
        return _numOfMarks == _game.Length * _game[0].Length;
    }
}

private void SetupInitialViewState(int rows, int columns)
{
    for (int row = 0; row < rows; row++)
    {
        for (int column = 0; column < columns; column++)
        {
            var tilePosition = new BoardTilePosition(row, column);
            int tileState = _game[row][column];

            // Determine player type based on tile state
            PlayerType playerType = tileState == 1 ? _playerO : (tileState == 2 ? _playerX : null);

            // Set tile sign using SetTileSign function
            SetTileSign(playerType, tilePosition);
        }
    }
}
