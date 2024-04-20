using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TileController_classicOnline : MonoBehaviourPunCallbacks
{
    [Header("Component References")]
    public GameStateController_classicOnline gameController_classicOnline; 
    public Button interactiveButton_classicOnline;
    public Text internalText_classicOnline;

    private void Start()
    {
        // Disable the button if it's not owned by the local player
        if (!photonView.IsMine)
        {
            interactiveButton_classicOnline.interactable = false;
        }
    }

    // Called when the tile is clicked
    public void OnTileClicked()
    {
        // Ensure that the tile is owned by the local player
        if (!photonView.IsMine)
            return;

        // Update the tile text and disable the button
        internalText_classicOnline.text = GetLocalPlayerSymbol();
        interactiveButton_classicOnline.interactable = false;

        // Inform all clients about the tile update
        photonView.RPC("UpdateTileRPC", RpcTarget.AllBuffered, internalText_classicOnline.text);
    }

    // RPC method to update the tile text for all clients
    [PunRPC]
    private void UpdateTileRPC(string symbol)
    {
        internalText_classicOnline.text = symbol;
    }

    // Helper method to get the local player's symbol (X or O)
    private string GetLocalPlayerSymbol()
    {
        if (photonView.Owner.ActorNumber % 2 == 0)
            return "O";
        else
            return "X";
    }

        public void ResetTile_classicOnline()
    {
        internalText_classicOnline.text = "";
        interactiveButton_classicOnline.image.sprite = gameController_classicOnline.tileEmpty_classicOnline;
    }
}
