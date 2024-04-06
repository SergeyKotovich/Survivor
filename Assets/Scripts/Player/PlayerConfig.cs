using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerConfig", fileName = "PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public int Speed { get; private set; }
    
    [field: SerializeField] public float Damage { get; private set; }
    
    [field: SerializeField] public int Health { get; private set; }
}