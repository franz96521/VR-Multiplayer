using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    public void GoHome() {
        VirtualWorldManager.Instance.LeaveRoomAndLoadHomeScene();
    }
}
