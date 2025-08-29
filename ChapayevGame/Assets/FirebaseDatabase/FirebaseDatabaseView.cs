using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseDatabaseView : View
{
    [SerializeField] private List<TopRecord> topRecords = new List<TopRecord>();

    public void DisplayUsersRecords(List<UserData> users)
    {
        for (int i = 0; i < users.Count; i++)
        {
            topRecords[i].SetData(users[i].Nickname, users[i].Record);
        }
    }
}

[Serializable]
public class TopRecord
{
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private TextMeshProUGUI textRecord;

    public void SetData(string nickname, int record)
    {
        textNickname.text = nickname;
        textRecord.text = record.ToString();
    }
}
