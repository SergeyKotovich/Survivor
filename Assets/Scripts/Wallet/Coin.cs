using UnityEngine;

public class Coin : MonoBehaviour
{
    [field:SerializeField] public int CountMoney { get; private set; }
    protected virtual void Update()
    {
        transform.Rotate(new Vector3(0,0,2));
    }

    public void DestroyCoin()
    {
        Destroy(gameObject);
    }
}