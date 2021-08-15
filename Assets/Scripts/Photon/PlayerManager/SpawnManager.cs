using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject GenericVRPlayerPrefab;
    public GameObject Spawner;
    
    int num = 0;
    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Debug.Log("avatar creado "+num);
            num += 1;
            PhotonNetwork.Instantiate(GenericVRPlayerPrefab.name, Spawner.transform.position , Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
