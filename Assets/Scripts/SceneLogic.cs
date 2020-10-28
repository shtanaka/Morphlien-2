using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogic : MonoBehaviour
{
    [SerializeField] private Ship gameShip;
    [SerializeField] private UIManager mainMenu;
    
    private bool isGUIDisplayed;
    public bool HasGameStarted { get; private set; } = false;

    void Start()
    {
        gameShip.IsShipReadyToPlay = false;
    }

    void Update()
    {
        ShowGUI();
        CloseGUI();
    }

    void ShowGUI()
    {
        if (gameShip.IsShipStartAnimationFinished && !isGUIDisplayed)
        {
            isGUIDisplayed = true;
            mainMenu.gameObject.SetActive(true);
        }
    }

    void CloseGUI()
    {
        if (mainMenu.IsGUIClosed)
        {
            Destroy(mainMenu);
            gameShip.IsShipReadyToPlay = true;
        }
    }

    public void StartGame()
    {
        mainMenu.StartCloseGUI();
    }
}
