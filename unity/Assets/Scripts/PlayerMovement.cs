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
    public float playerPushedDragCoefficient;

    public float startDirectionAngle;

    private float wantedDirectionAngle;

    public PlayerMovement otherPlayer;

    public GameManager gameManager;

    void Awake()
    {
        playerPushedMovement = new Vector3(0, 0, 0);
        wantedDirectionAngle = startDirectionAngle;
        dashing = false;
        dashOnCoolDown = false;
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
            moveVector = new Vector3(0, 0, -1) * playerDashSpeed;
        }

        GetComponent<Rigidbody>().AddForce((wantedDirectionQuaternion * moveVector));
    }

    void DashCoolDown()
    {
        dashOnCoolDown = false;
    }

    void DashTime()
    {
        dashing = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(tag + " boom " + other.tag);

        switch (other.tag)
        {
            case "Player":
                if (dashing)
                {
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                    otherPlayer.playerPushedMovement = transform.forward * playerDashSpeed;

                    dashing = false;
                }
                break;
            case "Walkable":
                gameManager.SteppedOnWalkableTile(other, this);
                break;
            case "NotWalkable":
                gameManager.SteppedOnNonWalkableTile(other, this);
                break;
            default:
                break;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
