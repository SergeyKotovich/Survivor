using UnityEngine;

[CreateAssetMenu(menuName = "Create MenuSettings", fileName = "MenuSettings", order = 0)]
public class MenuSettings : ScriptableObject
{
    [field: SerializeField] public int XPositionToHide  { get; private set; }
    [field: SerializeField] public int AverageValue { get; private set; }
    [field: SerializeField] public int XPositionToShow { get; private set; }
    [field: SerializeField] public float Duration { get; private set; }
}