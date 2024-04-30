using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class KateNpcController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _firstMessage;
    [SerializeField] private TextMeshProUGUI _secondMessage;
    [SerializeField] private int _delay = 5000;

    public async void ShowMessages()
    {
        gameObject.SetActive(true);
        _firstMessage.gameObject.SetActive(true);
        await UniTask.Delay(_delay);
        _firstMessage.gameObject.SetActive(false);
        _secondMessage.gameObject.SetActive(true);
        await UniTask.Delay(_delay);
        _secondMessage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
