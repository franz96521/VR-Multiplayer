using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRkeyboardSettings : MonoBehaviour
{
    public GameObject body;
    public GameObject Keyboard;
    public Vector3 positionOffset;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MovekeftHandUI();
    }
    private void MovekeftHandUI()
    {
        Keyboard.transform.position = body.transform.position+positionOffset;
        
    }
}
