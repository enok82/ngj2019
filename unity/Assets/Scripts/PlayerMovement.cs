using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string playerLeftAxis;
    public string playerRightAxis;
    public string playerLeftDashAxis;
    public string playerRightDashAxis;

    public string playerHorizontalAxis;
    public string playerVerticalAxis;

    public int playerWalkSpeed;
    public int playerDashSpeed;
    public float playerDashTime;
    public float playerDashCoolDownTime;
    public int playerTurnSpeed;

    public bool dashing;
    public bool dashCoolingDown;
    public Vector3 playerPushedMovement;
    public float playerPushedDragCoefficient;

    public float startDirectionAngle;

    public int deathCount;
    public bool hasWon;

    public GameObject deathSpray;

    public GameObject endGameConfetti;

    private float wantedDirectionAngle;

    public PlayerMovement otherPlayer;
    
    public Transform spawnHelper;

    private Animator anim;

    private Rigidbody playerRigidbody;

    void Awake()
    {
        deathCount = 0;
        hasWon = false;
        playerPushedMovement = new Vector3(0, 0, 0);
        wantedDirectionAngle = startDirectionAngle;
        dashing = false;
        dashCoolingDown = false;
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float turn = 0;
        float walk = 0;

        float dash = Mathf.Max(Input.GetAxis(playerLeftDashAxis), Input.GetAxis(playerRightDashAxis));
        
        switch (GameManager.Instance.playerControllerScheme)
        {
            case "A":
                turn = Input.GetAxis(playerRightAxis) - Input.GetAxis(playerLeftAxis);
                walk = -(Input.GetAxis(playerLeftAxis) + Input.GetAxis(playerRightAxis));

                break;
            case "B":
                turn = Input.GetAxis(playerHorizontalAxis);
                walk = -2 * Input.GetAxis(playerVerticalAxis);

                break;
            default:
                Debug.Log("Controller scheme not found");
                break;
        }
        
        anim.SetFloat("Speed", Mathf.Abs(walk));

        Vector3 moveVector;

        Quaternion wantedDirectionQuaternion = Quaternion.Euler(0, wantedDirectionAngle, 0);

        if (!dashing)
        {
            if (dash > 0 && !dashCoolingDown)
            {
                StartDashing();
            }

            wantedDirectionAngle += turn * playerTurnSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, wantedDirectionQuaternion, 1);

            moveVector = (new Vector3(0, 0, walk) * playerWalkSpeed) + playerPushedMovement;

            if (playerPushedMovement.magnitude > 0)
            {
                playerPushedMovement -= playerPushedMovement * playerPushedDragCoefficient;

                if (playerPushedMovement.magnitude < 0.1)
                {
                    playerPushedMovement = new Vector3(0, 0, 0);
                }
            }
        }
        else
        {
            moveVector = new Vector3(0, 0, 1) * playerDashSpeed;
        }

        playerRigidbody.AddForce((wantedDirectionQuaternion * moveVector));
    }

    void DashCoolDown()
    {
        dashCoolingDown = false;
    }

    void DashTime()
    {
        StopDashing();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        switch (other.tag)
        {
            case "Player":
                if (dashing)
                {
                    playerRigidbody.velocity = new Vector3(0, 0, 0);

                    otherPlayer.playerPushedMovement = transform.forward * playerDashSpeed;

                    StopDashing();
                }

                break;
            case "walkable":
                GameManager.Instance.SteppedOnWalkableTile(other, this);

                break;
            case "notWalkable":
                GameManager.Instance.SteppedOnNonWalkableTile(other, this);

                break;
            case "finishTile":
                GameManager.Instance.SteppedOnFinishTile(other, this);

                break;
            default:
                break;
        }
    }

    public void Die()
    {
        deathCount++;

        if (deathSpray)
        {
            Instantiate(deathSpray, transform.position, transform.rotation); // Expect auto destroy
        }

        Respawn();
    }

    public void Win()
    {
        hasWon = true;
    }

    public void Restart()
    {
        if (endGameConfetti)
        {
            Instantiate(endGameConfetti, transform.position, transform.rotation); // Expect auto destroy
        }
        
        Respawn();
    }

    public void Respawn()
    {
        if(spawnHelper)
        {
            if (deathSpray)
            {
                Instantiate(deathSpray, spawnHelper.position, spawnHelper.rotation); // Expect auto destroy
            }
            if(!playerRigidbody)
            {
                Debug.Log("Issues with RigidBody");
            }
            wantedDirectionAngle = 0;

            playerRigidbody.position = spawnHelper.position;
            playerRigidbody.velocity = new Vector3(0, 0, 0);
            StopDashing();
        }
    }

    void StopDashing()
    {
        anim.SetBool("Push", false);
        anim.SetBool("NotPush", true);
        dashing = false;
    }

    void StartDashing()
    {
        anim.SetBool("Push", true);
        anim.SetBool("NotPush", false);
        dashing = true;
        dashCoolingDown = true;

        Invoke("DashTime", playerDashTime);
        Invoke("DashCoolDown", playerDashCoolDownTime);
    }
}
