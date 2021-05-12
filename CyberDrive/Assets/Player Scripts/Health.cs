using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


namespace Player
{

    public class Health : MonoBehaviour
    {

        public PlayerMovementV2 playerMove;
        public Overdrive over;

        public int initHealth = 100;
        public int currHealth;
        bool isDamaged;
        bool isDash;
        bool isDead;

        // Start is called before the first frame update
        void Start()
        {
            GameObject thePlayer = GameObject.Find("Player");
            playerMove = thePlayer.GetComponent<PlayerMovementV2>();
            over = thePlayer.GetComponent<Overdrive>();

            currHealth = initHealth;

        }

        // Update is called once per frame
        void Update()
        {

            if (currHealth <= 0 && !isDead)
            {
                Death();
            }
        }

        void Death()
        {
            isDead = true;

            // Disable player movement and shooting
            playerMove.enabled = false;
        }
    }
}
