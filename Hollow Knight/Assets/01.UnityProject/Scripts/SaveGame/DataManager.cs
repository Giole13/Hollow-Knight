using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;




// 저장하는 방법
// 1. 저장할 데이터 존재
// 2. 데이터를 제이슨으로 변환
// 3. 제이슨을 외부에 저장

// 불러오는 방법
// 1. 외부에 저장된 제이슨을 가져옴
// 2. 제이슨을 데이터형태로 변환
// 3. 불러온 데이터를 사용

// 슬릇별로 다르게 저장



public class PlayerData
{
    // 지역 이름, 돈
    public string areaName;
    public int coin;

    // 플레이어 게임 오브젝트,
    // 현재 소속 레벨 오브젝트
    public GameObject playerObj;
    public GameObject levelObj;
    public Vector3 playerPos;
}


public class DataManager : GioleSingletone<DataManager>
{
    public PlayerData nowPlayer = new PlayerData();

    // 경로 변수
    public string path;
    string fileName = "save";
    public int nowSlot;



    public override void Awake()
    {
        path = Application.persistentDataPath + "/" + fileName;

    }



    // 데이터 저장하기
    public void SaveData()
    {
        // Json 파일로 변환 후 저장
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    // 데이터 불러오기
    public void LoadData()
    {
        // Json 파일로 불러온 후 Class로 변환
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    // 데이터 초기화
    public void DataNewInit()
    {
        // 다른 데이터를 불러오기 위해서 가상의 값으로 초기화
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
    
    // 데이터 삭제
    public void RemoveData()
    {
        // 해당 경로의 해당 파일을 삭제
        File.Delete(path + nowSlot.ToString());
    }
}
