using System;

public class GameStateManager : Singleton<GameStateManager> {
	    
		private enum GameState {
        Start,
        Memorize,
        Countdown,
        Choose,
        Result,
        Restart
    }
	
	private GameState _currentGameState = GameState.Start;
	
	public System.Action OnStart;
	
	public System.Action OnMemorize;
	
	public System.Action OnCountdown;
	
	public System.Action OnChoose;
	
	public System.Action OnResult;
	
	public System.Action OnRestart;
	
	
	 /// <summary>
    /// Called whenever the Action Button is clicked
    /// </summary>
    public void ActionClick() {
        
        // Depending on the State in the Current Game State...
        switch (_currentGameState) {

            // ...if the state is on Start...
            case GameState.Start:
				
				// ...then Change the state to Memorize.
                _currentGameState = GameState.Memorize;
				
				// ...and Run the OnStart Action...
				OnStart.Run();

                break;

            // ...if the state is on Memorize...
            case GameState.Memorize:
                
                // ...Then change the gameState to Restart...
                _currentGameState = GameState.Restart;
				
				// ...and Run the OnMemorize Action.
				OnMemorize.Run();
				
                break;

            // ...if the state is on Restart...
            case GameState.Restart:
                
                // ...then Change the state to Start...
                _currentGameState = GameState.Start;
				
				// ...and Run the OnRestart Action
				OnRestart.Run();

                break;

            // ...if the state is on Countdown...
            case GameState.Countdown:
                
				// ...then change the state to Choose
				_currentGameState = GameState.Choose;
				
				// ...and Run the OnCountdown Action.
				OnChoose.Run();
				
                break;

            // ...if the state is on Choose...
            case GameState.Choose:
                
				// ...then change the state to Choose
				_currentGameState = GameState.Result;
				
				// ...and Run the OnResult Action.
				OnResult.Run();
				
                break;

            // ...if the state is on Result...
            case GameState.Result:
                
				// ...then change the state to Choose
				_currentGameState = GameState.Start;
				
				// ...and Run the OnStart Action.
				OnStart.Run();
				
                break;

            // ...if the game state is non of the enums...
            default:
                // ...then Throw an Argument out of range exception.
                throw new ArgumentOutOfRangeException();
        }
    }
}
