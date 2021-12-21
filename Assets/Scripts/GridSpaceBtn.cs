using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSpaceBtn : MonoBehaviour
{
    public Button gridButton;
    public Text gridBtnText;
    public string playerSide;

    private GameManager _gameManager;

    public void SetGameManagerReference(GameManager gameController)
    {
        _gameManager = gameController;
    }

    public void SetGridSpace()
    {
        gridBtnText.text = _gameManager.GetPlayerSide();
        gridButton.interactable = false;
        _gameManager.EndTurn();
    }
}
