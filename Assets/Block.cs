using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public Button button;
    public Image image;
    public Text text;

    public void Click()
    {
        // Can't interact 2nd time
        button.interactable = false;
        
        // Update player character
        text.text = TicTacToe.Instance.GetCurrentPlayerCharacter();
        
        // End one turn -> Next
        TicTacToe.Instance.NextTurn();
    }
}
