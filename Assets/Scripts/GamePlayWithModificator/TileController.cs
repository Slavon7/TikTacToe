using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    [Header("Component References")]
    public GameStateController gameController;
    public GameStateController_classic gameController_classic;   
    public Button interactiveButton;
    public Text internalText;

    public void UpdateTile()
    {
        gameController.AddClickedButton(interactiveButton);
        internalText.text = gameController.GetPlayersTurn();
        interactiveButton.image.sprite = gameController.GetPlayerSprite();
        interactiveButton.interactable = false;
        gameController.EndTurn();
    
        // Активируем аудиосорс
        if (gameController.audioSource != null)
        {
            gameController.audioSource.Play();
        }
    }

    public void ResetTile()
    {
        internalText.text = "";
        interactiveButton.image.sprite = gameController.tileEmpty;
        SetAlpha(1f);
    }

        // Метод для установки альфа-канала
    public void SetAlpha(float alpha)
    {
        Color currentColor = interactiveButton.image.color;
        currentColor.a = alpha;
        interactiveButton.image.color = currentColor;
    }

    public void UpdateTile_classic()
    {
        internalText.text = gameController_classic.GetPlayersTurn_classic();
        interactiveButton.image.sprite = gameController_classic.GetPlayerSprite_classic();
        interactiveButton.interactable = false;
        gameController_classic.EndTurn_classic();
    }

    public void ResetTile_classic()
    {
        internalText.text = "";
        interactiveButton.image.sprite = gameController_classic.tileEmpty_classic;
    }
}
