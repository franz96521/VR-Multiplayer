using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    TextMeshProUGUI OcupancyRatefor_Park;
    [SerializeField]
    TextMeshProUGUI OcupancyRatefor_Village;
    private string mapTypeName;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinLobby();
        }
        else {
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    void Update()
    {

    }

    #region UI Callback Methods
    public void joinRanomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void OnEnterRoomButtonClicked_School()
    {
        mapTypeName = GameMultiplayerConstants.MAP_TYPE_VALUE_SCHOOL;
        ExitGames.Client.Photon.Hashtable ExpectedcustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { GameMultiplayerConstants.MAP_TYPE_KEY, GameMultiplayerConstants.MAP_TYPE_VALUE_SCHOOL } };
        PhotonNetwork.JoinRandomRoom(ExpectedcustomRoomProperties, 0);
    }
    public void OnEnterRoomButtonClicked_Outdoor()
    {
        mapTypeName = GameMultiplayerConstants.MAP_TYPE_VALUE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable ExpectedcustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { GameMultiplayerConstants.MAP_TYPE_KEY, GameMultiplayerConstants.MAP_TYPE_VALUE_OUTDOOR } };
        PhotonNetwork.JoinRandomRoom(ExpectedcustomRoomProperties, 0);
       
    }
    public void OnEnterRoomButtonClicked_Park()
    {
        mapTypeName = GameMultiplayerConstants.MAP_TYPE_VALUE_PARK;
        ExitGames.Client.Photon.Hashtable ExpectedcustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { GameMultiplayerConstants.MAP_TYPE_KEY, GameMultiplayerConstants.MAP_TYPE_VALUE_OUTDOOR } };
        PhotonNetwork.JoinRandomRoom(ExpectedcustomRoomProperties, 0);

    }
    public void OnEnterRoomButtonClicked_Village()
    {
        mapTypeName = GameMultiplayerConstants.MAP_TYPE_VALUE_VILLAGE;
        ExitGames.Client.Photon.Hashtable ExpectedcustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { GameMultiplayerConstants.MAP_TYPE_KEY, GameMultiplayerConstants.MAP_TYPE_VALUE_OUTDOOR } };
        PhotonNetwork.JoinRandomRoom(ExpectedcustomRoomProperties, 0);

    }

    #endregion

    #region Photon Callback Methods
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("fali to join random" + message + "create and Joing");
        CreateAndJoingRoom();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("conect to Master");
        PhotonNetwork.JoinLobby();
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("room create" + PhotonNetwork.CurrentRoom.Name);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("the localPlayer " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " player count " + PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(GameMultiplayerConstants.MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(GameMultiplayerConstants.MAP_TYPE_KEY, out mapType))
            {
                if (mapType.ToString() == GameMultiplayerConstants.MAP_TYPE_VALUE_SCHOOL)
                {
                    PhotonNetwork.LoadLevel(GameMultiplayerConstants.MAP_TYPE_VALUE_SCHOOL);
                }
                else if (mapType.ToString() == GameMultiplayerConstants.MAP_TYPE_VALUE_OUTDOOR)
                {
                    PhotonNetwork.LoadLevel(GameMultiplayerConstants.MAP_TYPE_VALUE_OUTDOOR);
                }
                else if (mapType.ToString() == GameMultiplayerConstants.MAP_TYPE_VALUE_VILLAGE)
                {
                    PhotonNetwork.LoadLevel(GameMultiplayerConstants.MAP_TYPE_VALUE_VILLAGE);
                }
                else if (mapType.ToString() == GameMultiplayerConstants.MAP_TYPE_VALUE_PARK)
                {
                    PhotonNetwork.LoadLevel(GameMultiplayerConstants.MAP_TYPE_VALUE_PARK);
                }
            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        // Debug.Log("new player "+newPlayer.NickName +" count "+ PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("join  loby");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //Debug.Log("Room Update " + roomList.Count);
        if (roomList.Count == 0)
        {
            OcupancyRatefor_Park.text = 0 + "/" + 20;
            OcupancyRatefor_Village.text = 0 + "/" + 20;
        }
        
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);

            if (room.Name.Contains(GameMultiplayerConstants.MAP_TYPE_VALUE_VILLAGE))
            {
                OcupancyRatefor_Village.text = room.PlayerCount + "/" + 20;
                Debug.Log("school count " + room.PlayerCount);
            }
            else if (room.Name.Contains(GameMultiplayerConstants.MAP_TYPE_VALUE_PARK))
            {
                Debug.Log("outdoot count " + room.PlayerCount);
                OcupancyRatefor_Park.text = room.PlayerCount + "/" + 20;

            }

        }
    }
    #endregion

    #region Private Metods
    void CreateAndJoingRoom()
    {
        string ranomName = "Room_" + mapTypeName +"_"+ Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();

        roomOptions.MaxPlayers = 20;
        string[] roomPropertiesInLoby = { GameMultiplayerConstants.MAP_TYPE_KEY };
        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { GameMultiplayerConstants.MAP_TYPE_KEY, mapTypeName } };
        roomOptions.CustomRoomPropertiesForLobby = roomPropertiesInLoby;
        roomOptions.CustomRoomProperties = customRoomProperties;
        PhotonNetwork.CreateRoom(ranomName, roomOptions);
    }
    #endregion

}
