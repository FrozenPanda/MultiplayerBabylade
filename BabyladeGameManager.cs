using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BabyladeGameManager : MonoBehaviourPunCallbacks
{   
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    GameObject spark;

    public Transform[] spawnPoints;

    public static BabyladeGameManager BabyladeGameManagerScript;
    public int playerCount;

    public Vector3 firstSpeed, secondSpeed;

    public Text winnerText;

    //public Text player1NameText, player2NameText;
    //public Slider player1HealthBar, player2HealthBar;
    // Start is called before the first frame update
    private void Awake()
    {
        BabyladeGameManagerScript = this;
        playerCount = 0;
    }
    void Start()
    {
        //player1HealthBar.value = 100f;
        //player2HealthBar.value = 100f;
        winnerText.gameObject.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {

            if (playerPrefab != null)
            {
                int randomPoint = Random.Range(-20, 20);

                PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[playerCount].transform.position, Quaternion.identity);
                
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    
    public void sparkCreate(Vector3 a,Vector3 b, float firstSpeed ,float secondSpeed)
    {

        PhotonNetwork.Instantiate(spark.name , (a + b) / 2, Quaternion.identity);
        if (firstSpeed > secondSpeed)
        {
            PlayerController.PlayerControllerScript2.takeDamage(firstSpeed - secondSpeed);
        }
        else
        {
            PlayerController.PlayerControllerScript.takeDamage(secondSpeed - firstSpeed);
        }
    }

    public void reverseSpeedAfterCollision(Vector3 first , Vector3 second)
    {
        firstSpeed = first;
        secondSpeed = second;
        PlayerController.PlayerControllerScript.changeSpeeds(second);
        PlayerController.PlayerControllerScript2.changeSpeeds(first);

    }

    [PunRPC]
    public void setVictoryText(string name)
    {
        winnerText.gameObject.SetActive(true);
        
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

    }


}
