using System;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
   [SerializeField] private Light _light;

   private void Update()
   {
      _light.range -= 0.0001f;
   }

   public void AddFuel(float fuel)
   {
      _light.range += fuel;
   }
}
