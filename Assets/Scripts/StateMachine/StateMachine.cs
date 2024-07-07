using RadgarGames.Interface;
using UnityEngine;

namespace RadgarGames.NavMesh.Follower
{
    public abstract class StateMachine : MonoBehaviour, IStateMachine
    {
        protected IState _currentState;

        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        protected virtual void Awake()
        {
            Debug.Log($"Initialize State Machine: {gameObject.name}");
        }

        protected virtual void Update()
        {
            _currentState?.Execute();
        }
    }
}