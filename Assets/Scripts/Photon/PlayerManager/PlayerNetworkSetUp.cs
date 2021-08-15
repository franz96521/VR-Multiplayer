using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerNetworkSetUp : MonoBehaviourPunCallbacks
{
    public GameObject LocalXRRigGameObject;
    public GameObject ControllersScripts;
    public GameObject MovementScript;
    public GameObject MainPlayer;
    void Start()
    {
        if (photonView.IsMine)
        {
            LocalXRRigGameObject.SetActive(true);
            ControllersScripts.SetActive(true);
            MovementScript.SetActive(true);
            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
            if (teleportationAreas.Length > 0)
            {
                foreach (var item in teleportationAreas)
                {
                    item.teleportationProvider = MovementScript.GetComponent<TeleportationProvider>();
                }
            }
        }
        else
        {
            LocalXRRigGameObject.SetActive(false);
            ControllersScripts.SetActive(false);
            MovementScript.SetActive(false);
            SetLayerRecursively(MainPlayer, 9);

        }
    }

  
    void Update()
    {

    }
    void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
