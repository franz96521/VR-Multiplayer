using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        RoomManager roomManager = (RoomManager)target;
        EditorGUILayout.HelpBox("this script is for creating an joing rooms ", MessageType.Info);
        /*if (GUILayout.Button("joing  to ranom room"))
        {
            roomManager.joinRanomRoom();
        }*/
        if (GUILayout.Button("joing village Room")) {
            roomManager.OnEnterRoomButtonClicked_Village();
        }
        if (GUILayout.Button("joing park Room"))
        {
            roomManager.OnEnterRoomButtonClicked_Park(); ;
        }
    }
}

