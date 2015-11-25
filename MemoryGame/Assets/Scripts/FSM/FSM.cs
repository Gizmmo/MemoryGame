using System;
using UnityEngine;
using System.Collections.Generic;

public class Fsm<TEnum, T> where T : IFsmState {

    #region Private Methods
    private readonly Dictionary<TEnum, T> _states = new Dictionary<TEnum, T>();
	private Dictionary<TEnum, Dictionary<TEnum, Action>> _transitions = new Dictionary<TEnum, Dictionary<TEnum, Action>>();
    #endregion

    #region Auto Properties
    public T CurrentState { get; private set; }

    public TEnum CurrentStateName { get; private set; }

    #endregion

    #region State Methods
    public void AddState(TEnum key, T state) {
        _states.Add(key, state);
		_transitions.Add(key, new Dictionary<TEnum, Action>());
    }

    public void RemoveState(TEnum key) {
        _states.Remove(key);
    }

    public void SetCurrentState(TEnum key) {
        //If the fsm does not contain this key
        if (!_states.ContainsKey(key)) {
            Debug.LogError("State Machine Does not contian the key: " + key);
            return;
        }

        //If this is the first state added
        if (CurrentState != null) {
            CurrentState.OnExit();
	        if (_transitions[CurrentStateName].ContainsKey(key)) {
		        _transitions[CurrentStateName][key]();
	        }
        }

        CurrentState = _states[key];
        CurrentStateName = key;
        CurrentState.OnEntry();
    }

    public void SetCurrentStateIf(TEnum key, Func<bool> checkFunc) {
        if (checkFunc()) {
            SetCurrentState(key);
        }
    }

	public void SetCurrentStateIf(TEnum key, bool checkBool) {
		if(checkBool) {
			SetCurrentState(key);
		}
	}

    #endregion

	#region Transition Methods

	public void AddTransition(TEnum currentState, TEnum destinationState, Action actionToPerform) {
		var cStateTrans = _transitions[currentState];
		if (cStateTrans == null) {
			Debug.LogError("Current State for transition not found");
			return;
		}

		cStateTrans[destinationState] = cStateTrans.ContainsKey(destinationState) ? cStateTrans[destinationState] += actionToPerform : actionToPerform;
	}
	#endregion
}
