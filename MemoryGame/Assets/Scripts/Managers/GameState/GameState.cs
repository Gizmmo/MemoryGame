public class ProgressState {
	
	public enum GameState {
        Start,
        Memorize,
        Countdown,
        Choose,
        Result,
        Restart
    }
	
	public System.Action OnStart;
	
	public System.Action OnMemorize;
	
	public System.Action OnCountdown;
	
	public System.Action OnChoose;
	
	public System.Action OnResult;
	
	public System.Action OnRestart;
	
	
	private Fsm<GameState, ConcreteState> _currentState = new Fsm<GameState, ConcreteState>();

	public ProgressState() {
		_currentState.AddState(GameState.Start, new StartState(this));
		_currentState.AddState(GameState.Memorize, new MemorizeState(this));
		_currentState.AddState(GameState.Countdown, new CountdownState(this));
		_currentState.AddState(GameState.Choose, new ChooseState(this));
		_currentState.AddState(GameState.Result, new ResultState(this));
		_currentState.AddState(GameState.Restart, new RestartState(this));
		_currentState.SetCurrentState(GameState.Start);
	}
	
	public void ActionTrigger() {
		_currentState.CurrentState.ActionTrigger();
	}

    public abstract class ConcreteState : IFsmState {
		
		protected ProgressState BaseProgressState;
		
		protected ConcreteState(ProgressState baseProgressState) {
			BaseProgressState = baseProgressState;
		}
		
        public virtual void OnEntry() {}

        public virtual void OnExit() {}
		
		public virtual void ActionTrigger() {}
		
		protected void ChangeState(GameState nextState) {
			BaseProgressState._currentState.SetCurrentState(nextState);
		}
    }
	
	public class StartState : ConcreteState {
	
		public StartState(ProgressState baseProgressState) : base(baseProgressState) {}
		
		public override string ToString() {
			return "Start State";
		}
		
		public override void OnEntry() {
			BaseProgressState.OnStart.Run();
		}
		
		public override void ActionTrigger() {
			ChangeState(GameState.Memorize);
		}
		
	}
	
	public class MemorizeState : ConcreteState {
	
		public MemorizeState(ProgressState baseProgressState) : base(baseProgressState) {}
		
		public override string ToString() {
			return "Memorize State";
		}
		
		public override void OnEntry() {
			BaseProgressState.OnMemorize.Run();
		}
		
		public override void ActionTrigger() {
			ChangeState(GameState.Restart);
		}
	}
	
	public class CountdownState : ConcreteState {
	
		public CountdownState(ProgressState baseProgressState) : base(baseProgressState) {}
		
		public override string ToString() {
			return "Countdown State";
		}
		
		public override void OnEntry() {
			BaseProgressState.OnCountdown.Run();
		}
		
		public override void ActionTrigger() {
			ChangeState(GameState.Choose);
		}
	}
	
	public class ChooseState : ConcreteState {
	
		public ChooseState(ProgressState baseProgressState) : base(baseProgressState) {}
		
		public override string ToString() {
			return "Choose State";
		}
		
		public override void OnEntry() {
			BaseProgressState.OnChoose.Run();
		}
		
		public override void ActionTrigger() {
			ChangeState(GameState.Result);
		}
	}
	
	public class ResultState : ConcreteState {
	
		public ResultState(ProgressState baseProgressState) : base(baseProgressState) {}
		
		public override string ToString() {
			return "Result State";
		}
		
		public override void OnEntry() {
			BaseProgressState.OnResult.Run();
		}
		
		public override void ActionTrigger() {
			ChangeState(GameState.Restart);
		}
	}
	
	public class RestartState : ConcreteState {
	
		public RestartState(ProgressState baseProgressState) : base(baseProgressState) {}
		
		public override string ToString() {
			return "Restart State";
		}
		
		public override void OnEntry() {
			BaseProgressState.OnRestart.Run();
		}
		
		public override void ActionTrigger() {
			ChangeState(GameState.Start);
		}
	}
}
