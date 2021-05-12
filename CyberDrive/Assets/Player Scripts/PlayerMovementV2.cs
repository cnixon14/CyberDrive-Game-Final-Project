using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;


/* Credit to Alexey Shmakov, PrzemyslawNowaczyk, WiggleWizard, Maskim Ruts, scnoobi, 
 * and Aaron Ware on Github for providing translation of Quake 3 Source code for
 * reference and serving as a baseboard for the code.
 */

/* Original Quake 3: Arena source code
 * 
 */

struct Commands
{
    public float xMove;
    public float zMove;
    public float yMove;
}

namespace Player
{

    public class PlayerMovementV2 : MonoBehaviour
    {
        public CharacterController controller;
        public Overdrive over;

        public float grav = 15.00f;
        public float groundFric = 6;
        public float runSpeed = 10f;                 // Player movement speed
        public float runAcceleration = 10f;         // Acceleration on ground
        public float runDecceleration = 10f;
        public float airAcceleration = 2f;
        public float airDecceleration = 2f;
        public float airControl = 0.5f;
        public float airStompSpeed = 50f;
        public float jumpSpeed = 10.0f;
        public float dashSpeed = 50.0f;
        public float dashAccel = 100f;
        public int extraJump = 2;
        public bool isDash;
        public bool isDashReady;
        public bool isStomp;
        public float dashCool = 2.5f;
        private float playerFriction = 0.0f;
        


        private Vector3 playerVelocity = Vector3.zero;
        private Vector3 moveDirectionNorm = Vector3.zero;


        private Commands cmd;


        // Start is called before the first frame update
        void Start()
        {
            GameObject thePlayer = GameObject.Find("Player");
            controller = thePlayer.GetComponent<CharacterController>();
            over = thePlayer.GetComponent<Overdrive>();
        }

        // Update is called once per frame
        void Update()
        {

            if (controller.isGrounded)
            {
                GroundMovement();
                switch (over.overTier)
                {
                    case 1:
                        extraJump = 1;
                        break;
                    case 2:
                        extraJump = 2;
                        break;
                    case 3:
                        extraJump = 2;
                        break;
                    default:
                        extraJump = 0;
                        break;
                }
                isStomp = false;
            }
            else if (!controller.isGrounded)
            {
                AerialMovement();
            }

            controller.Move(playerVelocity * Time.deltaTime);
        }

        private void SetMoveDir()
        {
            cmd.xMove = Input.GetAxisRaw("Horizontal");
            cmd.zMove = Input.GetAxisRaw("Vertical");
        }

        private void GroundMovement()
        {
            Vector3 inputDir;

            SetFriction(1.0f);

            SetMoveDir();

            inputDir = new Vector3(cmd.xMove, 0, cmd.zMove);
            inputDir = transform.TransformDirection(inputDir);
            inputDir.Normalize();
            moveDirectionNorm = inputDir;

            var inputSpeed = inputDir.magnitude;

            inputSpeed *= runSpeed;

            Accelerate(inputDir, inputSpeed, runAcceleration);

            playerVelocity.y -= grav * Time.deltaTime;

            
            inputSpeed *= runSpeed;

            Accelerate(inputDir, inputSpeed, runAcceleration);

            playerVelocity.y -= grav * Time.deltaTime;

            if (Input.GetButtonDown("Dash") && isDashReady == true)
            {
                inputSpeed *= dashSpeed;
                Accelerate(inputDir, inputSpeed, runAcceleration);

                isDashReady = false;
                dashCool -= Time.deltaTime;
                if (dashCool <= 0)
                {
                    isDashReady = true;
                    dashCool = 2.5f;
                }

            }

            if (Input.GetButtonDown("Jump"))
            {
                playerVelocity.y = jumpSpeed;
            }

        }

        private void AerialMovement()
        {
            Vector3 inputDir;
            float inputVel = airAcceleration;
            float accel;

            SetMoveDir();

            if (!isStomp)
            {
                inputDir = new Vector3(cmd.xMove, 0, cmd.zMove);
                inputDir = transform.TransformDirection(inputDir);

                float inputSpeed = inputDir.magnitude;
                inputSpeed *= runSpeed;

                inputDir.Normalize();
                moveDirectionNorm = inputDir;

                float inputSpeed2 = inputSpeed;
                if (Vector3.Dot(playerVelocity, inputDir) < 0)
                {
                    accel = airDecceleration;
                }
                else
                {
                    accel = airAcceleration;
                }

                Accelerate(inputDir, inputSpeed, accel);

                if (Input.GetButtonDown("Jump") && extraJump != 0)
                {
                    playerVelocity.y = jumpSpeed;
                    extraJump--;
                }

                playerVelocity.y -= grav * Time.deltaTime;
            }

            if (Input.GetButtonDown("AirStomp"))
            {
                playerVelocity.x = 0;
                playerVelocity.z = 0;
                playerVelocity.y -= airStompSpeed;
                isStomp = true;
            }


        }


        private void SetFriction(float x)
        {
            Vector3 vec = playerVelocity;
            float speed;
            float newSpeed;
            float control;
            float drop;

            vec.y = 0.0f;
            speed = vec.magnitude;
            drop = 0.0f;

            if (controller.isGrounded)
            {
                control = speed < runDecceleration ? runDecceleration : speed;
                drop = control * groundFric * Time.deltaTime * x;
            }

            newSpeed = speed - drop;
            playerFriction = newSpeed;
            if (newSpeed < 0)
                newSpeed = 0;
            if (speed > 0)
                newSpeed /= speed;

            playerVelocity.x *= newSpeed;
            playerVelocity.z *= newSpeed;
        }

        private void Accelerate(Vector3 moveDir, float moveSpeed, float accel)
        {
            float addSpeed;
            float accelSpeed;
            float currentSpeed;

            currentSpeed = Vector3.Dot(playerVelocity, moveDir);
            addSpeed = moveSpeed - currentSpeed;
            if (addSpeed <= 0)
                return;
            accelSpeed = accel * Time.deltaTime * moveSpeed;
            if (accelSpeed > addSpeed)
                accelSpeed = addSpeed;

            playerVelocity.x += accelSpeed * moveDir.x;
            playerVelocity.z += accelSpeed * moveDir.z;
        }
    }

}
