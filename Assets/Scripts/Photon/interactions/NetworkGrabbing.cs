using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkGrabbing : MonoBehaviourPunCallbacks,IPunOwnershipCallbacks
{
    PhotonView m_photonView;
    Rigidbody rb;
    public bool isBeingHeld = false;
    private void Awake()
    {
        m_photonView = GetComponent<PhotonView>();
       // Debug.Log("Getting componetn " + m_photonView.ToString());
    }

    void Start()
    {
        
        //m_photonView = GetComponent<PhotonView>();
        
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
            gameObject.layer = 0;
        }
        else {
            rb.isKinematic = false;
            gameObject.layer = 8;
            rb.detectCollisions = true;
        }
    }
    void TransferOwnerShip() {
        m_photonView.RequestOwnership();
    }
    public void OnSelectedEnter() {
        
        //Debug.Log(" selected componetn " + m_photonView.ToString());
        isBeingHeld = true;
        if (m_photonView.Owner != PhotonNetwork.LocalPlayer) {
            TransferOwnerShip();
        }        
        
        m_photonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);
    }
    public void OnSelectedExit() {
        isBeingHeld = false;
        //Debug.Log("Exit");
        m_photonView.RPC("StopNetworkGrabbing", RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        if (targetView != m_photonView) {
            return;
        }
       //Debug.Log("ownership Recuest" + targetView.name + " from" + requestingPlayer.NickName);
        m_photonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        //Debug.Log("transfer is complete owner = " + targetView.Owner.NickName);
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        throw new System.NotImplementedException();
    }
    [PunRPC]
    public void StartNetworkGrabbing() {
        isBeingHeld = true;

    }
    [PunRPC]
    public void StopNetworkGrabbing()
    {
        isBeingHeld = false;
    }
}
