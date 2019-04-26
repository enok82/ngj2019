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
    public int playerTurnSpeed;

    private float wantedDirectionAngle;
    private bool dashing;
    private CharacterController playerController;

    void Awake()
    {
        wantedDirectionAngle = 0f;
        dashing = false;
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float turn = Input.GetAxis(playerRightAxis) - Input.GetAxis(playerLeftAxis);
        float walk = Input.GetAxis(playerLeftAxis) + Input.GetAxis(playerRightAxis);
        float dash = Mathf.Max(Input.GetAxis(playerLeftDashAxis), Input.GetAxis(playerRightDashAxis));

        Debug.Log(dash);

        Vector3 moveVector;

        Quaternion wantedDirectionQuaternion = Quaternion.Euler(0, wantedDirectionAngle, 0);

        if (!dashing)
        {
            if (dash > 0)
            {
                dashing = true;

                Invoke("DashCoolDown", playerDashTime);
            }

            wantedDirectionAngle += turn * playerTurnSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, wantedDirectionQuaternion, 1);

            moveVector = new Vector3(0, 0, walk) * playerWalkSpeed / 10;

        }
        else
        {
            moveVector = new Vector3(0, 0, -1) * playerDashSpeed / 10;
        }
        
        playerController.Move(wantedDirectionQuaternion * moveVector);
    }

    void DashCoolDown()
    {
        dashing = false;
    }
}
