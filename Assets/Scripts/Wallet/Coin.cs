using UnityEngine;

public class Coin : MonoBehaviour
{
    private const int _rotationSpeed = 50;
    [field: SerializeField] public int CountMoney { get; private set; }

    protected virtual void Update()
    {
        transform.Rotate(new Vector3(0, 0, 2) * Time.deltaTime * _rotationSpeed);
    }
}