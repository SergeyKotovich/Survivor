using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;


public class MenuAnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _leaderBoard;
    [SerializeField] private MenuSettings _mainMenuSettings;
    [SerializeField] private MenuSettings _menuSettings;
    
    private void Awake()
    {
        ShowMainMenu();
    }

    [UsedImplicitly]
    public void ShowMainMenu()
    {
        Show(_mainMenu, _mainMenuSettings.AverageValue, _mainMenuSettings.XPositionToShow, _mainMenuSettings.Duration);
    }

    [UsedImplicitly]
    public void HideMainMenu()
    {
        _mainMenu.transform.DOMoveX(_mainMenuSettings.XPositionToHide, _mainMenuSettings.Duration);
    }

    [UsedImplicitly]
    public void ShowSettingMenu()
    {
        Show(_settingsMenu, _menuSettings.AverageValue, _menuSettings.XPositionToShow, _menuSettings.Duration);
    }

    [UsedImplicitly]
    public void HideSettingMenu()
    {
        Hide(_settingsMenu, _menuSettings.XPositionToHide, _menuSettings.Duration);
    }

    private void Show(GameObject menu, int averageValue, int xPositionToShow, float duration)
    {
        menu.transform.DOMoveX(averageValue, duration)
            .OnComplete(() => menu.transform.DOMoveX(xPositionToShow, duration));
    }

    private void Hide(GameObject menu, int endValue, float duration)
    {
        menu.transform.DOMoveX(endValue, duration);
    }
    //
    [UsedImplicitly]
    public void ShowLeaderboard()
    {
        Show(_leaderBoard, _mainMenuSettings.AverageValue, _mainMenuSettings.XPositionToShow, _mainMenuSettings.Duration);
    }

    [UsedImplicitly]
    public void HideLeaderboard()
    {
        _leaderBoard.transform.DOMoveX(_mainMenuSettings.XPositionToHide, _mainMenuSettings.Duration);
    }
}