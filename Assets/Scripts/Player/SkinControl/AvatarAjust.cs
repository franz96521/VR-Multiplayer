using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform rigTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    public void Map()
    {
        rigTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        rigTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class AvatarAjust : MonoBehaviour
{
    public GameObject rigthHandScale;
    public GameObject rigthControllerScale;

    public VRMap head;
    public VRMap leftHand;
    public VRMap rightHand;
    public Transform headConstraint;
    public Vector3 headBodyOffest;

    // Start is called before the first frame update
    void Start()
    {
        AjustHead();
    }
    
    // Update is called once per frame
    void Update()
    {
        AjustMove();
    }
    private void AjustMove() {
        transform.position = headConstraint.position + headBodyOffest;
        transform.forward = Vector3.ProjectOnPlane(headConstraint.up, Vector3.up).normalized;
        head.Map();
        leftHand.Map();
        rightHand.Map();
    }
    private void AjustHead()
    {
        headBodyOffest = transform.position - headConstraint.position;
    }

    public void AjustSkinScale()
    {
        float scale = rigthControllerScale.transform.position.x / rigthHandScale.transform.position.x;
        float scale2 = rigthHandScale.transform.position.x / rigthControllerScale.transform.position.x;
        Debug.Log("scale = "+scale + "  " + scale2);
        transform.localScale *= scale2;
        AjustHead();
    }
}
