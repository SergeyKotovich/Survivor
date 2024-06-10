using System;
using UnityEngine;

public class HealthBarPositionController : MonoBehaviour
{
   [SerializeField] private Transform _healthBarRoot;
   [SerializeField] private RectTransform _healthBar;

   private void Awake()
   {
      
   }

 // private void Update()
 // {
 //    var positionInScreenSpace = Camera.main.WorldToScreenPoint(_healthBarRoot.position);
 //    _healthBar.position = positionInScreenSpace;
 // }
}