using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button playerButton;
}

[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameManager : MonoBehaviour
{
    public Text[] buttonTextList;
    public GameObject gameOverPlane;
    public Text gameOverText;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;

    [SerializeField] GameObject _restartButton;

    private string _playerSide;
    private int _moveCount;

    private void Awake()
    {
        SetGameManagerReferenceOnButton();
        
        gameOverPlane.SetActive(false);
        _restartButton.SetActive(false);
        _moveCount = 0;
    }

    void SetGameManagerReferenceOnButton()
    {
        for (int count = 0; count < buttonTextList.Length; count++)
        {
            buttonTextList[count].GetComponentInParent<GridSpaceBtn>().SetGameManagerReference(this);
        }
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
    }

    public string GetPlayerSide()
    {
        return _playerSide;
    }

    public void EndTurn()
    {
        _moveCount++;

        GameLogic();
        ChangeSides();
    }

    void ChangeSides()
    {
        _playerSide = (_playerSide == "X") ? "O" : "X";

        if (_playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;

        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        _restartButton.SetActive(true);

        if (winningPlayer == "draw" || winningPlayer == "Draw")
        {
            SetGameOverText("It's Draw");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins");
        }
    }

    void SetGameOverText(string value)
    {
        gameOverPlane.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        gameOverPlane.SetActive(false);
        _moveCount = 0;
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);

        for (int count = 0; count < buttonTextList.Length; count++)
        {
            buttonTextList[count].text = "";
        }
        _restartButton.SetActive(false);
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int count = 0; count < buttonTextList.Length; count++)
        {
            buttonTextList[count].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public void SetStartingSide(string startingSide)
    {
        _playerSide = startingSide;

        if (_playerSide == "x")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }

        StartGame();
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.playerButton.interactable = toggle;
        playerO.playerButton.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerO.panel.color = inactivePlayerColor.panelColor;

        playerX.text.color = inactivePlayerColor.textColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

    private void GameLogic()
    {
        // for rows
        if (buttonTextList[0].text == _playerSide && buttonTextList[1].text == _playerSide && buttonTextList[2].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (buttonTextList[3].text == _playerSide && buttonTextList[4].text == _playerSide && buttonTextList[5].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (buttonTextList[6].text == _playerSide && buttonTextList[7].text == _playerSide && buttonTextList[8].text == _playerSide)
        {
            GameOver(_playerSide);
        }

        // for columns
        else if (buttonTextList[0].text == _playerSide && buttonTextList[3].text == _playerSide && buttonTextList[6].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (buttonTextList[1].text == _playerSide && buttonTextList[4].text == _playerSide && buttonTextList[7].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (buttonTextList[2].text == _playerSide && buttonTextList[5].text == _playerSide && buttonTextList[8].text == _playerSide)
        {
            GameOver(_playerSide);
        }

        // for diagonals
        else if (buttonTextList[0].text == _playerSide && buttonTextList[4].text == _playerSide && buttonTextList[8].text == _playerSide)
        {
            GameOver(_playerSide);
        }
        else if (buttonTextList[2].text == _playerSide && buttonTextList[4].text == _playerSide && buttonTextList[6].text == _playerSide)
        {
            GameOver(_playerSide);
        }

        // for all boxes
        else if (_moveCount >= 9)
        {
            GameOver("Draw");
        }
    }
}
