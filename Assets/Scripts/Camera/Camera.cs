using System;
using UnityEngine;
using VContainer;

public class Camera : MonoBehaviour
{
   [SerializeField] private Vector3 _offset;

   private IMovable _movable;

   [Inject]
   public void Construct(IMovable movable)
   {
      _movable = movable;
   }

   private void LateUpdate()
   {
      transform.position = _movable.Position+_offset;
   }
}
