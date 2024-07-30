using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class MenuAnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _leaderBoard;

    private void Awake()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        _mainMenu.transform.DOMoveX(550, 0.3f)
            .OnComplete(() => _mainMenu.transform.DOMoveX(400, 0.3f));
    }

    [UsedImplicitly]
    public void HideMainMenu()
    {
        _mainMenu.transform.DOMoveX(-350, 0.3f);
    }

    public void ShowSettingMenu()
    {
        _settingsMenu.transform.DOMoveX(550,0.3f)
            .OnComplete(() => _mainMenu.transform.DOMoveX(400, 0.3f));
    }
}