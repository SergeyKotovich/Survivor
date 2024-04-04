using UnityEngine;
using VContainer;

public class FollowPlayer : MonoBehaviour
{
   [SerializeField] private Vector3 _offset;
   private int _currentIndex;

   private IMovable _movable;

   [Inject]
   public void Construct(IMovable movable)
   {
      _movable = movable;
   }
   private void LateUpdate()
   {
      transform.localPosition = _movable.Position + _offset;
   }
}

