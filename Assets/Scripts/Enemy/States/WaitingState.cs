using Cysharp.Threading.Tasks;
using UnityEngine;

public class WaitingState : MonoBehaviour, IState
{
    [SerializeField] private EnemyAnimationController _animationController;
    
    private StateMachine _stateMachine;
    
    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _animationController.ShowIdle();
    }
    
}