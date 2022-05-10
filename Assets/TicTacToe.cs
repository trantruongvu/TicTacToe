using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviour
{
    // Singleton
    public static TicTacToe Instance;

    public int blocksToBuild = 3;
    public int blocksToWin = 3;
    public Board board;

    // Result panel
    public Text txtResult;

    public int turnCount = 0;
    public bool isPlayer1Turn = true;
    public string currentPlayerCharacter = "x";

    private void Awake()
    {
        Instance = this;
    }

    public void NextTurn()
    {
        // Update number of turns
        turnCount++;

        // Check if current player won
        if (board.CheckForWinner())
        {
            if (isPlayer1Turn)
                EndGame(0); // Debug.Log("Player 1 won");
            else
                EndGame(1); // Debug.Log("Player 2 won");
        }

        if (turnCount >= blocksToBuild * blocksToBuild)
            EndGame(2); // Debug.Log("Draw");

        // Switch player character
        isPlayer1Turn = !isPlayer1Turn;
        if (isPlayer1Turn)
            currentPlayerCharacter = "x";
        else
            currentPlayerCharacter = "o";
    }

    public string GetCurrentPlayerCharacter()
    {
        return currentPlayerCharacter;
    }

    public void EndGame(int status)
    {
        switch (status)
        {
            case 0:
                txtResult.text = "Player 1 won";
                txtResult.transform.parent.gameObject.SetActive(true);
                break;
            case 1:
                txtResult.text = "Player 2 won";
                txtResult.transform.parent.gameObject.SetActive(true);
                break;
            case 2:
                txtResult.text = "DRAW";
                txtResult.transform.parent.gameObject.SetActive(true);
                break;

        }
    }
}