using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;




// �����ϴ� ���
// 1. ������ ������ ����
// 2. �����͸� ���̽����� ��ȯ
// 3. ���̽��� �ܺο� ����

// �ҷ����� ���
// 1. �ܺο� ����� ���̽��� ������
// 2. ���̽��� ���������·� ��ȯ
// 3. �ҷ��� �����͸� ���

// �������� �ٸ��� ����



public class PlayerData
{
    // ���� �̸�, ��
    public string areaName;
    public int coin;

    // �÷��̾� ���� ������Ʈ,
    // ���� �Ҽ� ���� ������Ʈ
    public GameObject playerObj;
    public GameObject levelObj;
    public Vector3 playerPos;
}


public class DataManager : GioleSingletone<DataManager>
{
    public PlayerData nowPlayer = new PlayerData();

    // ��� ����
    public string path;
    string fileName = "save";
    public int nowSlot;



    public override void Awake()
    {
        path = Application.persistentDataPath + "/" + fileName;

    }



    // ������ �����ϱ�
    public void SaveData()
    {
        // Json ���Ϸ� ��ȯ �� ����
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    // ������ �ҷ�����
    public void LoadData()
    {
        // Json ���Ϸ� �ҷ��� �� Class�� ��ȯ
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    // ������ �ʱ�ȭ
    public void DataNewInit()
    {
        // �ٸ� �����͸� �ҷ����� ���ؼ� ������ ������ �ʱ�ȭ
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
    
    // ������ ����
    public void RemoveData()
    {
        // �ش� ����� �ش� ������ ����
        File.Delete(path + nowSlot.ToString());
    }
}
