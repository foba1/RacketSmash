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
        connectText.text = "���� ��...";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        connectText.text = "������ �Ϸ�Ǿ����ϴ�.";
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        connectText.text = "���ӿ� �����Ͽ����ϴ�. �������� �õ��մϴ�.";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            connectText.text = "���ӿ� ���� ��...";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinedRoom()
    {
        connectText.text = "���ӿ� �����Ͽ����ϴ�.";
        PhotonNetwork.LoadLevel("VRInteractionTestScene");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectText.text = "���ӿ� ���� ��...";
        PhotonNetwork.CreateRoom("test", new RoomOptions { MaxPlayers = 2 });
    }
}
