using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
[System.Serializable]
public class VRBodyPart
{
    public GameObject part;
    [HideInInspector]
    public Transform generalTransform;
    //position
    [HideInInspector]
    public float m_Distance;
    [HideInInspector]
    public Vector3 m_Direction;
    [HideInInspector]
    public Vector3 m_NetworkPosition;
    [HideInInspector]
    public Vector3 m_StoredPosition;
    //Rotation
    [HideInInspector]
    public Quaternion m_NetworkRotation;
    [HideInInspector]
    public float m_Angle;

    public void initialize()
    {
        this.generalTransform = this.part.transform;
        this.m_StoredPosition=generalTransform.position;
        this.m_NetworkPosition = Vector3.zero;
        this.m_NetworkRotation = Quaternion.identity;
    }
    public void moveMine() {
        this.generalTransform.position = Vector3.MoveTowards(generalTransform.position, this.m_NetworkPosition, this.m_Distance * (1.0f / PhotonNetwork.SerializationRate));
        this.generalTransform.rotation = Quaternion.RotateTowards(generalTransform.rotation, this.m_NetworkRotation, this.m_Angle * (1.0f / PhotonNetwork.SerializationRate));
    }
    public void updateMyPositionData() {
        this.m_Direction = generalTransform.position - this.m_StoredPosition;
        this.m_StoredPosition = generalTransform.position;
    }  
}
public class MultiplayerVRSynchronization : MonoBehaviour, IPunObservable
{
    private PhotonView m_PhotonView;
    bool m_firstTake = false;

    public List<VRBodyPart> bodyParts;
    public void Update()
    {
        if (!this.m_PhotonView.IsMine)
        {
            foreach (VRBodyPart part in bodyParts)
            {
                //send position
                part.moveMine();
            }
        }
    }
    public void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
        foreach (VRBodyPart part in bodyParts) {
            part.initialize();
        }
    }
    void OnEnable()
    {
        m_firstTake = true;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // my data
        if (stream.IsWriting)
        {
            foreach (VRBodyPart part in bodyParts)
            {
                //send position
                part.updateMyPositionData();
                stream.SendNext(part.generalTransform.position);
                stream.SendNext(part.m_Direction);
                //send rotation
                stream.SendNext(part.generalTransform.rotation);
            }
        }
        //others data 
        else {
            foreach (VRBodyPart part in bodyParts)
            {
                //position data
                part.m_NetworkPosition = (Vector3)stream.ReceiveNext();
                part.m_Direction= (Vector3)stream.ReceiveNext();
                if (m_firstTake)
                {
                    part.generalTransform.position = part.m_NetworkPosition;
                    part.m_Distance = 0f;
                }
                else {
                    float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                    part.m_NetworkPosition += part.m_Direction * lag;
                    part.m_Distance = Vector3.Distance(part.generalTransform.position, part.m_NetworkPosition);
                }
                //rotation data
                part.m_NetworkRotation = (Quaternion)stream.ReceiveNext();
                if (m_firstTake)
                {
                    part.m_Angle = 0f;
                    part.generalTransform.rotation = part.m_NetworkRotation;
                }
                else
                {
                    part.m_Angle = Quaternion.Angle(part.generalTransform.rotation, part.m_NetworkRotation);
                }
            }
        }
    }
}