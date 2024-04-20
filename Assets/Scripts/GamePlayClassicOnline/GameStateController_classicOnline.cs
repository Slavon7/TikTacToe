using UnityEngine;
using UnityEngine.UI;

public class GameStateController_classicOnline : MonoBehaviour
{
    [Header("TitleBar References")]
    public Image playerXIcon_classicOnline;                                        // Reference to the playerX icon
    public Image playerOIcon_classicOnline;                                        // Reference to the playerO icon
    public InputField player1InputField_classicOnline;                             // Reference to P1 input field
    public InputField player2InputField_classicOnline;                             // Refernece to P2 input field
    public Text winnerText_classicOnline;                                          // Displays the winners name

    [Header("Misc References")]
    public GameObject endGameState_classicOnline;                                  // Game footer container + winner text

    [Header("Asset References")]
    public Sprite tilePlayerO_classicOnline;                                       // Sprite reference to O tile
    public Sprite tilePlayerX_classicOnline;                                       // Sprite reference to X tile
    public Sprite tileEmpty_classicOnline;                                         // Sprite reference to empty tile
    public Text[] tileList_classicOnline;                                          // Gets a list of all the tiles in the scene

    [Header("GameState Settings")]
    public Color inactivePlayerColor_classicOnline;                                // Color to display for the inactive player icon
    public Color activePlayerColor_classicOnline;                                  // Color to display for the active player icon
    public string whoPlaysFirst_classicOnline;                                     // Who plays first (X : 0) {NOTE! no checks are made to ensure this is either X or O}

    [Header("Private Variables")]
    private string playerTurn_classicOnline;                                       // Internal tracking whos turn is it
    private string player1Name_classicOnline;                                      // Player1 display name
    private string player2Name_classicOnline;                                      // Player2 display name
    private int moveCount_classicOnline;                                           // Internal move counter



    /// <summary>
    /// Start is called on the first active frame
    /// </summary>
    private void Start()
    {
        // Set the internal tracker of whos turn is first and setup UI icon feedback for whos turn it is
        playerTurn_classicOnline = whoPlaysFirst_classicOnline;
        if (playerTurn_classicOnline == "X") playerOIcon_classicOnline.color = inactivePlayerColor_classicOnline;
        else playerXIcon_classicOnline.color = inactivePlayerColor_classicOnline;

        //Adds a listener to the name input fields and invokes a method when the value changes. This is a callback.
        player1InputField_classicOnline.onValueChanged.AddListener(delegate { OnPlayer1NameChanged_classicOnline(); });
        player2InputField_classicOnline.onValueChanged.AddListener(delegate { OnPlayer2NameChanged_classicOnline(); });

        // Set the default values to what tthe inputField text is
        player1Name_classicOnline = player1InputField_classicOnline.text;
        player2Name_classicOnline = player2InputField_classicOnline.text;
    }

    public void EndTurn_classicOnline()
    {
        moveCount_classicOnline++;
        if (tileList_classicOnline[0].text == playerTurn_classicOnline && tileList_classicOnline[1].text == playerTurn_classicOnline && tileList_classicOnline[2].text == playerTurn_classicOnline) GameOver_classicOnline(playerTurn_classicOnline);
        else if (tileList_classicOnline[3].text == playerTurn_classicOnline && tileList_classicOnline[4].text == playerTurn_classicOnline && tileList_classicOnline[5].text == playerTurn_classicOnline) GameOver_classicOnline(playerTurn_classicOnline);
        else if (tileList_classicOnline[6].text == playerTurn_classicOnline && tileList_classicOnline[7].text == playerTurn_classicOnline && tileList_classicOnline[8].text == playerTurn_classicOnline) GameOver_classicOnline(playerTurn_classicOnline);
        else if (tileList_classicOnline[0].text == playerTurn_classicOnline && tileList_classicOnline[3].text == playerTurn_classicOnline && tileList_classicOnline[6].text == playerTurn_classicOnline) GameOver_classicOnline(playerTurn_classicOnline);
        else if (tileList_classicOnline[1].text == playerTurn_classicOnline && tileList_classicOnline[4].text == playerTurn_classicOnline && tileList_classicOnline[7].text == playerTurn_classicOnline) GameOver_classicOnline(playerTurn_classicOnline);
        else if (tileList_classicOnline[2].text == playerTurn_classicOnline && tileList_classicOnline[5].text == playerTurn_classicOnline && tileList_classicOnline[8].text == playerTurn_classicOnline) GameOver_classicOnline(playerTurn_classicOnline);
        else if (tileList_classicOnline[0].text == playerTurn_classicOnline && tileList_classicOnline[4].text == playerTurn_classicOnline && tileList_classicOnline[8].text == playerTurn_classicOnline) GameOver_classicOnline(playerTurn_classicOnline);
        else if (tileList_classicOnline[2].text == playerTurn_classicOnline && tileList_classicOnline[4].text == playerTurn_classicOnline && tileList_classicOnline[6].text == playerTurn_classicOnline) GameOver_classicOnline(playerTurn_classicOnline);
        else if (moveCount_classicOnline >= 9) GameOver_classicOnline("D");
        else
            ChangeTurn_classicOnline();
    }

    /// <summary>
    /// Changes the internal tracker for whos turn it is
    /// </summary>
    public void ChangeTurn_classicOnline()
    {
        // This is called a Ternary operator which evaluates "X" and results in "O" or "X" based on truths
        // We then just change some ui feedback like colors.
        playerTurn_classicOnline = (playerTurn_classicOnline == "X") ? "O" : "X";
        if (playerTurn_classicOnline == "X")
        {
            playerXIcon_classicOnline.color = activePlayerColor_classicOnline;
            playerOIcon_classicOnline.color = inactivePlayerColor_classicOnline;
        }
        else
        {
            playerXIcon_classicOnline.color = inactivePlayerColor_classicOnline;
            playerOIcon_classicOnline.color = activePlayerColor_classicOnline;
        }
    }

    /// <summary>
    /// Called when the game has found a win condition or draw
    /// </summary>
    /// <param name="winningPlayer_classicOnline">X O D</param>
    private void GameOver_classicOnline(string winningPlayer_classicOnline)
    {
        switch (winningPlayer_classicOnline)
        {
            case "D":
                winnerText_classicOnline.text = "DRAW";
                break;
            case "X":
                winnerText_classicOnline.text = player1Name_classicOnline;
                break;
            case "O":
                winnerText_classicOnline.text = player2Name_classicOnline;
                break;
        }
        endGameState_classicOnline.SetActive(true);
        ToggleButtonState_classicOnline(false);
    }

    /// <summary>
    /// Restarts the game state
    /// </summary>
    public void RestartGame_classicOnline()
    {
        // Reset some gamestate properties
        moveCount_classicOnline = 0;
        playerTurn_classicOnline = whoPlaysFirst_classicOnline;
        ToggleButtonState_classicOnline(true);
        endGameState_classicOnline.SetActive(false);

        // Loop though all tiles and reset them
        for (int i = 0; i < tileList_classicOnline.Length; i++)
        {
            tileList_classicOnline[i].GetComponentInParent<TileController_classicOnline>().ResetTile_classicOnline();
        }
    }

    /// <summary>
    /// Enables or disables all the buttons
    /// </summary>
    private void ToggleButtonState_classicOnline(bool state_classicOnline)
    {
        for (int i = 0; i < tileList_classicOnline.Length; i++)
        {
            tileList_classicOnline[i].GetComponentInParent<Button>().interactable = state_classicOnline;
        }
    }

    /// <summary>
    /// Returns the current players turn (X / O)
    /// </summary>
    public string GetPlayersTurn_classicOnline()
    {
        return playerTurn_classicOnline;
    }

    /// <summary>
    /// Retruns the display sprite (X / 0)
    /// </summary>
    public Sprite GetPlayerSprite_classicOnline()
    {
        if (playerTurn_classicOnline == "X") return tilePlayerX_classicOnline;
        else return tilePlayerO_classicOnline;
    }

    /// <summary>
    /// Callback for when the P1_textfield is updated. We just update the string for Player1
    /// </summary>
    public void OnPlayer1NameChanged_classicOnline()
    {
        player1Name_classicOnline = player1InputField_classicOnline.text;
    }

    /// <summary>
    /// Callback for when the P2_textfield is updated. We just update the string for Player2
    /// </summary>
    public void OnPlayer2NameChanged_classicOnline()
    {
        player2Name_classicOnline = player2InputField_classicOnline.text;
    }
}