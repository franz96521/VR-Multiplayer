using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLeftHandUI : MonoBehaviour
{
    public GameObject HandController;
    public GameObject LefthandUI;
    public Vector3 rotationOffset;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        MovekeftHandUI();
    }
    private void  MovekeftHandUI() { 
        LefthandUI.transform.position= HandController.transform.position;       
        LefthandUI.transform.rotation = HandController.transform.rotation* Quaternion.Euler(rotationOffset);
    }
}
