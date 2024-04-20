using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField input_Create;
    public TMP_InputField input_Join;
    public GameObject lobbyUI;
    public GameObject startButton;
    public TextMeshProUGUI playerCountText;

    private bool isMaster = false;

    void Start()
    {
        startButton.SetActive(false);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(input_Create.text, new RoomOptions() { MaxPlayers = 2, IsVisible = true, IsOpen = true }, TypedLobby.Default, null);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(input_Join.text);
    }

    public void JoinRoomInList(string RoomName)
    {
        PhotonNetwork.JoinRoom(RoomName);
    }

    public override void OnJoinedRoom()
    {
        lobbyUI.SetActive(false);
        isMaster = PhotonNetwork.IsMasterClient;
        if (isMaster)
        {
            startButton.SetActive(true);
        }
        UpdatePlayerCount();
    }

    public void StartGame()
    {
        if (isMaster)
        {
            photonView.RPC("RPC_LoadGameScene", RpcTarget.All);
        }
    }

    [PunRPC]
    private void RPC_LoadGameScene()
    {
        PhotonNetwork.LoadLevel("OnlineGame");
    }

    private void UpdatePlayerCount()
    {
        if (playerCountText != null)
        {
            playerCountText.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + "/2";
        }
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        UpdatePlayerCount();
    }
}
