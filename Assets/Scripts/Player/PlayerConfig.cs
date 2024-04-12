using UnityEngine;

[CreateAssetMenu(menuName = "Create PlayerConfig", fileName = "PlayerConfig", order = 0)]
public class PlayerConfig : ScriptableObject
{
    [field: SerializeField] public float RunningSpeed { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float AttackImprovementStep { get; private set; }
    [field: SerializeField] public float HealthImprovementStep { get; private set; }
    [field: SerializeField] public float DamageImprovementStep { get; private set; }
    [field: SerializeField] public float RunningSpeedImprovementStep { get; private set; }
    
    
    
}