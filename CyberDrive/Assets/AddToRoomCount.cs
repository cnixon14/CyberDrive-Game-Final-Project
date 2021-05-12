using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToRoomCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RoomChecker.enemyCount++;
    }

    public static void RemoveCount()
    {
        RoomChecker.enemyCount--;
    }
}
