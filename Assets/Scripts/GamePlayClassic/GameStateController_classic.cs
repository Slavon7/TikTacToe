///-----------------------------------------------------------------
///   Class:          GameStateController
///   Description:    Handles the current state of the game and whos turn it is
///   Author:         VueCode
///   GitHub:         https://github.com/ivuecode/
///-----------------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class GameStateController_classic : MonoBehaviour
{
    [Header("TitleBar References")]
    public Image playerXIcon_classic;                                        // Reference to the playerX icon
    public Image playerOIcon_classic;                                        // Reference to the playerO icon
    public InputField player1InputField_classic;                             // Reference to P1 input field
    public InputField player2InputField_classic;                             // Refernece to P2 input field
    public Text winnerText_classic;                                          // Displays the winners name

    [Header("Misc References")]
    public GameObject endGameState_classic;                                  // Game footer container + winner text

    [Header("Asset References")]
    public Sprite tilePlayerO_classic;                                       // Sprite reference to O tile
    public Sprite tilePlayerX_classic;                                       // Sprite reference to X tile
    public Sprite tileEmpty_classic;                                         // Sprite reference to empty tile
    public Text[] tileList_classic;                                          // Gets a list of all the tiles in the scene

    [Header("GameState Settings")]
    public Color inactivePlayerColor_classic;                                // Color to display for the inactive player icon
    public Color activePlayerColor_classic;                                  // Color to display for the active player icon
    public string whoPlaysFirst_classic;                                     // Who plays first (X : 0) {NOTE! no checks are made to ensure this is either X or O}

    [Header("Private Variables")]
    private string playerTurn_classic;                                       // Internal tracking whos turn is it
    private string player1Name_classic;                                      // Player1 display name
    private string player2Name_classic;                                      // Player2 display name
    private int moveCount_classic;                                           // Internal move counter



    /// <summary>
    /// Start is called on the first active frame
    /// </summary>
    private void Start()
    {
        // Set the internal tracker of whos turn is first and setup UI icon feedback for whos turn it is
        playerTurn_classic = whoPlaysFirst_classic;
        if (playerTurn_classic == "X") playerOIcon_classic.color = inactivePlayerColor_classic;
        else playerXIcon_classic.color = inactivePlayerColor_classic;

        //Adds a listener to the name input fields and invokes a method when the value changes. This is a callback.
        player1InputField_classic.onValueChanged.AddListener(delegate { OnPlayer1NameChanged_classic(); });
        player2InputField_classic.onValueChanged.AddListener(delegate { OnPlayer2NameChanged_classic(); });

        // Set the default values to what tthe inputField text is
        player1Name_classic = player1InputField_classic.text;
        player2Name_classic = player2InputField_classic.text;
    }

    /// <summary>
    /// Called at the end of every turn to check for win conditions
    /// Hardcoded all possible win conditions (8)
    /// We just take position of tiles and check the neighbours (within a row)
    /// 
    /// Tiles are numbered 0..8 from left to right, row by row, example:
    /// [0][1][2]
    /// [3][4][5]
    /// [6][7][8]
    /// </summary>
    public void EndTurn_classic()
    {
        moveCount_classic++;
        if (tileList_classic[0].text == playerTurn_classic && tileList_classic[1].text == playerTurn_classic && tileList_classic[2].text == playerTurn_classic) GameOver_classic(playerTurn_classic);
        else if (tileList_classic[3].text == playerTurn_classic && tileList_classic[4].text == playerTurn_classic && tileList_classic[5].text == playerTurn_classic) GameOver_classic(playerTurn_classic);
        else if (tileList_classic[6].text == playerTurn_classic && tileList_classic[7].text == playerTurn_classic && tileList_classic[8].text == playerTurn_classic) GameOver_classic(playerTurn_classic);
        else if (tileList_classic[0].text == playerTurn_classic && tileList_classic[3].text == playerTurn_classic && tileList_classic[6].text == playerTurn_classic) GameOver_classic(playerTurn_classic);
        else if (tileList_classic[1].text == playerTurn_classic && tileList_classic[4].text == playerTurn_classic && tileList_classic[7].text == playerTurn_classic) GameOver_classic(playerTurn_classic);
        else if (tileList_classic[2].text == playerTurn_classic && tileList_classic[5].text == playerTurn_classic && tileList_classic[8].text == playerTurn_classic) GameOver_classic(playerTurn_classic);
        else if (tileList_classic[0].text == playerTurn_classic && tileList_classic[4].text == playerTurn_classic && tileList_classic[8].text == playerTurn_classic) GameOver_classic(playerTurn_classic);
        else if (tileList_classic[2].text == playerTurn_classic && tileList_classic[4].text == playerTurn_classic && tileList_classic[6].text == playerTurn_classic) GameOver_classic(playerTurn_classic);
        else if (moveCount_classic >= 9) GameOver_classic("D");
        else
            ChangeTurn_classic();
    }

    /// <summary>
    /// Changes the internal tracker for whos turn it is
    /// </summary>
    public void ChangeTurn_classic()
    {
        // This is called a Ternary operator which evaluates "X" and results in "O" or "X" based on truths
        // We then just change some ui feedback like colors.
        playerTurn_classic = (playerTurn_classic == "X") ? "O" : "X";
        if (playerTurn_classic == "X")
        {
            playerXIcon_classic.color = activePlayerColor_classic;
            playerOIcon_classic.color = inactivePlayerColor_classic;
        }
        else
        {
            playerXIcon_classic.color = inactivePlayerColor_classic;
            playerOIcon_classic.color = activePlayerColor_classic;
        }
    }

    /// <summary>
    /// Called when the game has found a win condition or draw
    /// </summary>
    /// <param name="winningPlayer_classic">X O D</param>
    private void GameOver_classic(string winningPlayer_classic)
    {
        switch (winningPlayer_classic)
        {
            case "D":
                winnerText_classic.text = "DRAW";
                break;
            case "X":
                winnerText_classic.text = player1Name_classic;
                break;
            case "O":
                winnerText_classic.text = player2Name_classic;
                break;
        }
        endGameState_classic.SetActive(true);
        ToggleButtonState_classic(false);
    }

    /// <summary>
    /// Restarts the game state
    /// </summary>
    public void RestartGame_classic()
    {
        // Reset some gamestate properties
        moveCount_classic = 0;
        playerTurn_classic = whoPlaysFirst_classic;
        ToggleButtonState_classic(true);
        endGameState_classic.SetActive(false);

        // Loop though all tiles and reset them
        for (int i = 0; i < tileList_classic.Length; i++)
        {
            tileList_classic[i].GetComponentInParent<TileController>().ResetTile_classic();
        }
    }

    /// <summary>
    /// Enables or disables all the buttons
    /// </summary>
    private void ToggleButtonState_classic(bool state_classic)
    {
        for (int i = 0; i < tileList_classic.Length; i++)
        {
            tileList_classic[i].GetComponentInParent<Button>().interactable = state_classic;
        }
    }

    /// <summary>
    /// Returns the current players turn (X / O)
    /// </summary>
    public string GetPlayersTurn_classic()
    {
        return playerTurn_classic;
    }

    /// <summary>
    /// Retruns the display sprite (X / 0)
    /// </summary>
    public Sprite GetPlayerSprite_classic()
    {
        if (playerTurn_classic == "X") return tilePlayerX_classic;
        else return tilePlayerO_classic;
    }

    /// <summary>
    /// Callback for when the P1_textfield is updated. We just update the string for Player1
    /// </summary>
    public void OnPlayer1NameChanged_classic()
    {
        player1Name_classic = player1InputField_classic.text;
    }

    /// <summary>
    /// Callback for when the P2_textfield is updated. We just update the string for Player2
    /// </summary>
    public void OnPlayer2NameChanged_classic()
    {
        player2Name_classic = player2InputField_classic.text;
    }
}