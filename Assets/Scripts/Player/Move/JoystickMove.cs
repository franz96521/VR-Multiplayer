using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class JoystickMove : MonoBehaviour
{
    public float speed = 1.0f;
    public List<XRController> controllers;
    public GameObject head = null;

    [SerializeField]
    TeleportationProvider teleportationProvider;
    public GameObject MainVRPlayer;
    public GameObject XRRigGameobject;
    private void OnEnable()
    {
        teleportationProvider.endLocomotion += OnEndLocomotion;
    }
    private void OnDisable()
    {
        teleportationProvider.endLocomotion -= OnEndLocomotion;
    }
    private void OnEndLocomotion(LocomotionSystem locomotionSystem)
    {
        MainVRPlayer.transform.position = MainVRPlayer.transform.TransformPoint(XRRigGameobject.transform.localPosition);
        XRRigGameobject.transform.localPosition = Vector3.zero;
    }
    
    void Update()
    {
        foreach (XRController xRController in controllers)
        {       
            if (xRController.inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 positionVector))
            {
                if (positionVector.magnitude > 0.15f)
                {
                    Move(positionVector);
                }
            }
        }
    }


    private void Move(Vector2 positionVector)
    {
        // Apply the touch position to the head's forward Vector
        Vector3 direction = new Vector3(positionVector.x, 0, positionVector.y);
        Vector3 headRotation = new Vector3(0, head.transform.eulerAngles.y, 0);
        // Rotate the input direction by the horizontal head rotation
        direction = Quaternion.Euler(headRotation) * direction;
        // Apply speed and move
        Vector3 movement = direction * speed;
        MainVRPlayer.transform.position += (Vector3.ProjectOnPlane(Time.deltaTime * movement, Vector3.up));
    }
}
