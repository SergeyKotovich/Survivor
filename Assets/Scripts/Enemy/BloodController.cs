using UnityEngine;

public class BloodController : MonoBehaviour
{
   [SerializeField] private ParticleSystem _particleSystem;

   public void ShowBlood()
   {
      _particleSystem.Play();
   }
}
