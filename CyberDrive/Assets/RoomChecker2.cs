using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker2 : MonoBehaviour
{
    public static int enemyCount;

    private void Start()
    {
        //CheckRoom();
        print(enemyCount);
    }

    public void CheckRoom()
    {
        Collider[] collidersHit = Physics.OverlapSphere(gameObject.transform.position, 300f);
        enemyCount = collidersHit.Length;
    }
}
