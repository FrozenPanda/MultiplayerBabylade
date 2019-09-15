using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public static PlayerController PlayerControllerScript;
    public static PlayerController PlayerControllerScript2;

    public Rigidbody rb;
    public Joystick joystick;

    public Text winnerText, LoseText;

    float maxSpeed = 15;

    [SerializeField]
    TMPro.TextMeshProUGUI playerNameText;

    public Image healthBar;
    public GameObject explosion;

    public float health = 100f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.name = photonView.Owner.NickName;
        playerNameText.text = photonView.Owner.NickName;
        
    }

    void Start()
    {
        BabyladeGameManager.BabyladeGameManagerScript.playerCount++;
        if (BabyladeGameManager.BabyladeGameManagerScript.playerCount == 1)
        {
            gameObject.tag = "First";
            PlayerControllerScript = this;
            //BabyladeGameManager.BabyladeGameManagerScript.player1NameText.text = photonView.Owner.NickName;
            
        }
        else if (BabyladeGameManager.BabyladeGameManagerScript.playerCount == 2)
        {
            gameObject.tag = "Second";
            PlayerControllerScript2 = this;
            //BabyladeGameManager.BabyladeGameManagerScript.player2NameText.text = photonView.Owner.NickName;
        }

        if (photonView.IsMine)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }

        healthBar.fillAmount = 100;

        
    }

    // Update is called once per frame
    void Update()
    {
        //set maximum speed
        if (rb.velocity.magnitude >  maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // transform.eulerAngles = new Vector3(rb.velocity.z , 0f , -rb.velocity.x);

        if (health <= 0)
        {
            Die();
        }

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) || joystick.Horizontal > 0.2f)
        {
            rb.AddForce(new Vector3(30f, 0f, 0f));
        }
        else if (Input.GetKey(KeyCode.A) || joystick.Horizontal < -0.2f)
        {
            rb.AddForce(new Vector3(-30f, 0f, 0f));
        }

        if (Input.GetKey(KeyCode.W) || joystick.Vertical > 0.2f)
        {
            rb.AddForce(new Vector3(0f, 0f, 30f));
        }
        else if (Input.GetKey(KeyCode.S) || joystick.Vertical < -0.2f)
        {
            rb.AddForce(new Vector3(0f, 0f, -30f));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Die();
        }
    }

    public void changeSpeeds(Vector3 speed)
    {
        rb.velocity = speed;
    }

    [PunRPC]
    public void takeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health / 100f;

    }

    
    public void Die()
    {
        
            if (gameObject.tag == "First")
            {

            BabyladeGameManager.BabyladeGameManagerScript.setVictoryText(GameObject.FindGameObjectWithTag("Second").name);
            }
            else
            {

            BabyladeGameManager.BabyladeGameManagerScript.setVictoryText(GameObject.FindGameObjectWithTag("First").name);
        }

        if (photonView.IsMine)
        {
            transform.localScale = new Vector3(0f, 0f, 0f);
            BabyladeGameManager.BabyladeGameManagerScript.LeaveRoom();
        }
    }

    
    public void enableVictoryTextForEach(string name)
    {
        BabyladeGameManager.BabyladeGameManagerScript.winnerText.gameObject.SetActive(true);
        BabyladeGameManager.BabyladeGameManagerScript.winnerText.text = "winner is: " + name;

    }

    public void exitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
