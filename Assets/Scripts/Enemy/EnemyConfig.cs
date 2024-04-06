using UnityEngine;

[CreateAssetMenu(menuName = "Create EnemyConfig", fileName = "EnemyConfig", order = 0)]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public Enemy EnemyPrefab { get; private set; }
    
    [field: SerializeField] public float Speed { get; private set; }
    
    [field: SerializeField] public int Damage { get; private set; }
    
    [field: SerializeField] public int Health { get; private set; }
    
}