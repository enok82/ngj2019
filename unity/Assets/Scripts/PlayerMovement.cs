using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string playerLeftAxis;
    public string playerRightAxis;
    public string playerLeftDashAxis;
    public string playerRightDashAxis;

    public int playerWalkSpeed;
    public int playerDashSpeed;
    public float playerDashTime;
    public float playerDashCoolDownTime;
    public int playerTurnSpeed;

    public bool dashing;
    public bool dashOnCoolDown;
    public Vector3 playerPushedMovement;

    public float startDirectionAngle;

    private float wantedDirectionAngle;
    private CharacterController playerController;

    public PlayerMovement otherPlayer;
    
    void Awake()
    {
        playerPushedMovement = new Vector3(0, 0, 0);
        wantedDirectionAngle = startDirectionAngle;
        dashing = false;
        dashOnCoolDown = false;
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float turn = Input.GetAxis(playerRightAxis) - Input.GetAxis(playerLeftAxis);
        float walk = Input.GetAxis(playerLeftAxis) + Input.GetAxis(playerRightAxis);
        float dash = Mathf.Max(Input.GetAxis(playerLeftDashAxis), Input.GetAxis(playerRightDashAxis));
        
        Vector3 moveVector;

        Quaternion wantedDirectionQuaternion = Quaternion.Euler(0, wantedDirectionAngle, 0);

        if (!dashing)
        {
            if (dash > 0 && !dashOnCoolDown)
            {
                dashing = true;
                dashOnCoolDown = true;

                Invoke("DashTime", playerDashTime);
                Invoke("DashCoolDown", playerDashCoolDownTime);
            }

            wantedDirectionAngle += turn * playerTurnSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, wantedDirectionQuaternion, 1);

            moveVector = (new Vector3(0, 0, walk) * playerWalkSpeed / 10) + playerPushedMovement;

            if(playerPushedMovement.magnitude > 0)
            {
                playerPushedMovement -= playerPushedMovement.normalized / 10;

                if(playerPushedMovement.magnitude < 0.1)
                {
                    playerPushedMovement = new Vector3(0, 0, 0);
                }
            }
        }
        else
        {
            moveVector = new Vector3(0, 0, -1) * playerDashSpeed / 10;
        }
        
        playerController.Move(wantedDirectionQuaternion * moveVector);
    }

    void DashCoolDown()
    {
        dashOnCoolDown = false;
    }

    void DashTime()
    {
        dashing = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (dashing && (hit.collider.tag == "Player"))
        {
            otherPlayer.playerPushedMovement = -transform.forward;

            dashing = false;
        }
    }
}
