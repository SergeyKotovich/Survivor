using System;
using UnityEngine;
using UnityEngine.Events;

public class ShopTriggerHandler : MonoBehaviour
{
    [SerializeField] private GameObject _improvementController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            _improvementController.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GlobalConstants.PLAYER_TAG))
        {
            _improvementController.SetActive(false);
        }
    }
}