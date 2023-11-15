using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerID : MonoBehaviour
{
    public string PlayerId { get; private set; }

    public PlayerID()
    {
        // Sử dụng Guid để tạo ID ngẫu nhiên
        PlayerId = Guid.NewGuid().ToString();
    }
}
