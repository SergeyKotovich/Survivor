using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private static readonly int _running = Animator.StringToHash("running");
    private static readonly int _idle = Animator.StringToHash("idle");
    


    public void PlayRunningAnimation()
    {
        _animator.SetBool(_running, true);
    }
    
    public void StopRunningAnimation()
    {
        _animator.SetBool(_running, false);
    }

    public void PlayIdleAnimation()
    {
        _animator.SetBool(_idle, true);
    }

    public void StopIdleAnimation()
    {
        _animator.SetBool(_idle,false);
    }

    
    
}