using UnityEngine;


[CreateAssetMenu(menuName = "Create ShopConfig", fileName = "ShopConfig", order = 0)]
public class ShopConfig : ScriptableObject
{
    [field: SerializeField] public int PriceImprovement { get; private set; }
    [field: SerializeField] public int PriceHeal { get; private set; }
}