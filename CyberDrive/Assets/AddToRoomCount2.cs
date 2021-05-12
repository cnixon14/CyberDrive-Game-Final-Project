using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToRoomCount2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RoomChecker2.enemyCount++;
    }

    public static void RemoveCount()
    {
        RoomChecker2.enemyCount--;
    }
}
