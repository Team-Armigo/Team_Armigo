using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PtManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "v1.0";
    private string userId = "mineo";

    public TMP_InputField userIdText;
    public TMP_InputField roomNameText;
    public TMP_Dropdown maxNum;
    public TMP_InputField passWord;

/*
    //룸 목록 저장하기 위한 딕셔너리 자료형 
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();
    
  
    // 룸을 표시할 프리팹 
    public GameObject roomPrefab;
    // Room 프리팹이 차일드화 시킬 부모 객체 
    public Transform scrollContent;
    */

    private void Awake()
    {
        // 방장 혼자 씬 로딩하면 , 나머지 사람들 자동으로 싱크 
        PhotonNetwork.AutomaticallySyncScene = true;
        // 게임 버전 지정 
        PhotonNetwork.GameVersion = gameVersion;
        // 서버 접속 
        PhotonNetwork.ConnectUsingSettings();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("00.포톤 매니저 시작 ");
        userId = PlayerPrefs.GetString("USER_ID", $"USER_{Random.Range(0, 100):00}");
        userIdText.text = userId;
        PhotonNetwork.NickName = userId;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("01. 포톤 서버에 접속 ");

        // 로비에 접속
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("02. 로비에 접속 ");
    }
/*
    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        Debug.Log("랜덤 룸 접속 실패 ");

        // 룸 속성 설정
        RoomOptions ro = new RoomOptions();
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.MaxPlayers = 10;

        roomNameText.text = $"Room_{Random.Range(1, 100):000}";

        // 룸을 생성 -> 자동 입장
        PhotonNetwork.CreateRoom("room_1", ro);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("03.방 생성 완료 ");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("04.방 입장 완료 ");
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Level_1");
        }
    }*/
/*
    public override void OnCreateRoomFailed()
    {
        Debug.Log("05.방 입장 실패");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        GameObject tempRoom = null;
        foreach(var room in roomList)
        {
            // 룸이 삭제된 경우
            if (room.RemovedFromList == true)
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            // 룸 정보가 갱신 (변경 ) 된 경우 
            else
            {
                // 룸이 처음 생성된 경우
                if (roomDict.ContainsKey(room.Name) == false)
                {
                    GameObject _room = Instantiate(roomPrefab, scrollContent);
                    _room.GetComponent<RoomData>().RoomInfo = room;
                    roomDict.Add(room.Name, _room);
                }
                // 룸 정보를 갱신하는 경우
                else
                {
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomData>().RoomInfo = room;
                }
            }
        }
    }
    
    #region UI_BUTTON_CALLBACK
    // Random 버튼 클릭
    public void OnRandomBtn()
    {
        //ID 인풋필드가 비어있으면
        if (string.IsNullOrEmpty(userIdText.text))
        {
            //랜덤 아이디 부여
            userId = $"USER_{Random.Range(0, 100):00}";
            userIdText.text = userId;
        }

        PlayerPrefs.SetString("USER_ID", userIdText.text);
        PhotonNetwork.NickName = userIdText.text;
        PhotonNetwork.JoinRandomRoom();
    }
*/
    //Room 버튼 클릭 시 (룸 생성 )
    public void OnClick_CreateRoom()
    {
        Debug.Log("코드.");
        byte maxPlayers = byte.Parse(maxNum.options[maxNum.value].text); // 드롭다운에서 값 얻어오기.
        
        RoomOptions ro = new RoomOptions();
        //ro.RoomName = roomNameText;
        ro.MaxPlayers = maxPlayers;
        ro.IsOpen = true;
        ro.IsVisible = true;
        ro.PublishUserId = true; // 유저아이디 불러오기 허락
        Debug.Log(" 작동.");

        // 인풋필드가 비어있으면
        if(string.IsNullOrEmpty(roomNameText.text))
        {
            //랜덤 룸 이름 부여
            roomNameText.text = $"ROOM_{Random.Range(1, 100):000}";
        }

        PhotonNetwork.CreateRoom(roomNameText.text, ro);
        Debug.Log("코드가 작동은 되었다.-photon");
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    //#endregion
}
