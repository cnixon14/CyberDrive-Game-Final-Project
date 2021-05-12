using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{

    public class Overdrive : MonoBehaviour
    {

        public PlayerMovementV2 move;

        public int overTier = 0;
        public float overCounter = 0.0f;
        public bool isOvercharge = false;

        


        // Start is called before the first frame update
        void Start()
        {
            GameObject thePlayer = GameObject.Find("Player");
            move = thePlayer.GetComponent<PlayerMovementV2>();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
