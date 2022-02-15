using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Imports da Photon 2
using Photon.Pun;
using Photon.Realtime;

public class NertworkController : MonoBehaviourPunCallbacks
{
    bool isConected = false;
   

    public override void OnConnected()
    {
        Debug.Log("Conexão Feita com Sucesso");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Não Estou no Master, Hora de entrar no Lobby!");
        isConected = true;

    }

    IEnumerator ReturnPing(float time)
    {
        yield return new WaitForSeconds(time);

        if(PhotonNetwork.CloudRegion != "")
        {
            Debug.Log("Server: " + PhotonNetwork.CloudRegion);
            Debug.Log("Ping: " + PhotonNetwork.GetPing());
            Debug.Log("-----------------------------");
        }
       
       
        StartCoroutine(ReturnPing(time));
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Estou No Lobby");
        Debug.Log("Temos " + PhotonNetwork.CountOfRooms + " disponiveis");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        Debug.Log("Não tem nenhuma room disponível");
        string roomName = ("sala_" + Random.Range(0, 9999));
        Debug.Log("Criar e entrar na room "+ roomName);
       PhotonNetwork.CreateRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("AAAAAAAAAAAAAAEEEEEEEEE estou na room!");
        Debug.Log("Temos "+PhotonNetwork.CurrentRoom.PlayerCount+" Jogadores Disponiveis");
    }

    public void ButtonClick()
    {
        PhotonNetwork.ConnectUsingSettings();
        //StartCoroutine(ReturnPing(1f));
    }

    public void EnterInLobby()
    {
        if (isConected)
        {
            PhotonNetwork.JoinLobby();
        }
    }
}
