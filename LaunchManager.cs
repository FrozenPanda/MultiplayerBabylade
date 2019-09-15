using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    public GameObject EnterGamePanel;
    public GameObject ConnectionStatusPanel;

    public GameObject LobbyPanel;

    // Start is called before the first frame update
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        EnterGamePanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        LobbyPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PhotonNetwork.CountOfPlayersOnMaster);
    }



    public override void OnConnected()
    {
        Debug.Log("Connected to internet");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.NickName + "Connected server");
        LobbyPanel.SetActive(true);
        ConnectionStatusPanel.SetActive(false);
        
        
    }


    //eğer oda bulunamadıysa
    //PhotonNetwork.CreateRoom(oda adı) ile yeni oda açıyoruz..
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //int roomNumber = Random.Range(1, 1000);
        Debug.Log("random room could not find");
        //PhotonNetwork.CreateRoom("room" + roomNumber);
        //Debug.Log("yeni oda oluşturuldu oda ismi room"+ roomNumber);
        CreateAndJoinRoom();
    }

    //herhangi bir odaya katılındığı zaman
    public override void OnJoinedRoom()
    {
        Debug.Log("this massage sent by onjoinedroom");
        PhotonNetwork.LoadLevel("GameScene");
        
    }


    //başka bir oyuncu açık olan odaya girdiği zaman..
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName  +": joined to room");
        Debug.Log("diğer oyuncu geldi");
    }


    //set button to connect server
    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            EnterGamePanel.SetActive(false);
            ConnectionStatusPanel.SetActive(true);
            PhotonNetwork.ConnectUsingSettings();
            
        }
        else
        {
            Debug.Log("You are already connected to server");
        }
    }

    public void JoinRandomRoom()
    {
        Debug.Log("trying to connect random room");
        PhotonNetwork.JoinRandomRoom();
    }


    private void CreateAndJoinRoom()
    {

        string randomRoomNAME = "Room" + Random.Range(0, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = (byte)20;


        PhotonNetwork.CreateRoom(randomRoomNAME, roomOptions);

        Debug.Log("yeni oda oluşturuldu oda ismi" + randomRoomNAME);
    }
}
