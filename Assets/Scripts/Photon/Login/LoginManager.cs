using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class LoginManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField PlayerName_inputfield;
   
    #region UNITY Methods
    void Start()
    {
        
    }    
    void Update()
    {

    }
    #endregion
    
    #region Photon Callback Methods
    public override void OnConnected()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("conected to master with name: "+PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("HomeScene");
    }
    #endregion
   
    #region UI Callback Methods
    public void ConnectToPhotonServerUsingSettings()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public void ConnectedToPhotonServerUsingName()
    {
        if (PlayerName_inputfield != null)
        {
            PhotonNetwork.NickName = PlayerName_inputfield.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    #endregion
}
