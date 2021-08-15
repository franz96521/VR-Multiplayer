using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LoginManager))]
public class LoginmanagerEitorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LoginManager loginManager = (LoginManager)target;
        EditorGUILayout.HelpBox("this script is for conecting to photon servers",MessageType.Info);
        if (GUILayout.Button("Connect Anonimousli")) {
            loginManager.ConnectToPhotonServerUsingSettings();
        }
    }
}
