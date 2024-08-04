using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    [SerializeField] private Slider _progressBar;


    public void ShowLoading(float progress)
    {
        _progressBar.gameObject.SetActive(true);
        _progressBar.value = progress / _progressBar.maxValue;
    }
}