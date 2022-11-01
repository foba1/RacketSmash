using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public Text connectText;

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        connectText.text = "접속 중...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        connectText.text = "접속이 완료되었습니다.";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectText.text = "접속에 실패하였습니다. 재접속을 시도합니다.";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            connectText.text = "게임에 참가 중...";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        connectText.text = "게임에 참가하였습니다.";
        PhotonNetwork.LoadLevel("VRInteractionTestScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectText.text = "게임에 참가 중...";
        PhotonNetwork.CreateRoom("test", new RoomOptions { MaxPlayers = 2 });
    }
}
