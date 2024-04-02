using System;
using UnityEngine;

[Serializable]
public class SpawnParameters
{
    [field: SerializeField] public Transform Point {get; private set;}
    [field: SerializeField] public int CountEnemy {get; private set;}
    [field: SerializeField] public EnemyConfig EnemyConfig {get; private set;}
    [field: SerializeField] public int Radius {get; private set;}
}