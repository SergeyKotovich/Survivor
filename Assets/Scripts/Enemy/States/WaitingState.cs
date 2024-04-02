using Cysharp.Threading.Tasks;
using UnityEngine;

public class WaitingState : MonoBehaviour, IState
{
    private StateMachine _stateMachine;
    

    public void Initialize(StateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        Waiting();
        
    }

    private async UniTask Waiting()
    {
        await UniTask.Delay(1);
        _stateMachine.Enter<MoveToTargetState>();
    }
}