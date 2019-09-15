using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI playerNameText;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            transform.GetComponent<PlayerController>().enabled = true;
        }
        else
        {
            transform.GetComponent<PlayerController>().enabled = false;
        }
        SetPlayerUI();
    }

    public void SetPlayerUI()
    {
        if (playerNameText!= null)
        {
            playerNameText.text = photonView.Owner.NickName;
        }
    }
}
