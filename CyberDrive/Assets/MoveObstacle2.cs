using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle2 : MonoBehaviour
{
    public GameObject self;

    // Update is called once per frame
    void Update()
    {
        if (RoomChecker2.enemyCount == 0)
        {
            Destroy(self);
        }
    }
}
