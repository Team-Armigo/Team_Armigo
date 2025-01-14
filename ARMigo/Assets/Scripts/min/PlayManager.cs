using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayManager : MonoBehaviourPunCallbacks
{

    public TMP_Text roomName;
    public TMP_Text maxNum;
    
    public TMP_Text msglist ;

    string msg = "";

    //string RoomName = PhotonNetwork.CurrentRoom.Name;
    public Button back;

    int count = 0;

    //string name = tmpName.text();


    // Start is called before the first frame update
    void Start()
    {
        Invoke("CheckPlayerCount", 0.5f);
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerCount();
    }
    public void RoomData()
    {
        //playerNum = PhotonNetwork.maxNum;
        //RoomName = PhotonNetwork.roomNameText.text;
    }

    public void LeaveRoom()
    {
        // 방 떠나기.
        PhotonNetwork.LeaveRoom();
        Debug.Log("LeaveRoom();실행되었따.");

    }
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom()함수가 불러졌다.");
        if(SceneManager.GetActiveScene().name == "RoomMain")
        {
            SceneManager.LoadScene("RoomList");
            Debug.Log("LoadScene실행되었따.");
            return;
        }
    }



    public override void OnCreatedRoom()
    {
        Debug.Log("03.방 생성 완료-play");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("04.방 입장 완료-play");
        if(PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.LoadLevel("Level_1");
            Debug.Log("IF");
        }
    }

    public void ListSceneGo()
    {
        SceneManager.LoadScene("RoomList");
    }

    void CheckPlayerCount()
    {
        int currPlayer = PhotonNetwork.PlayerList.Length;
        int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;
        maxNum.text = string.Format("[{0}/{1}]", currPlayer,maxPlayer);

        roomName.text = PhotonNetwork.CurrentRoom.Name;
        

    }


}
