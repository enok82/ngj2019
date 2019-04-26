using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string playerLeftAxis;
    public string playerRightAxis;
    public string playerLeftDashAxis;
    public string playerRightDashAxis;

    public int playerRunSpeed;
    public int playerDashSpeed;
    public int playerDashDistance;
    public int playerTurnSpeed;

    private float wantedDirectionAngle;

    void Awake()
    {
        playerLeftAxis = "";
        playerRightAxis = "";
        playerLeftDashAxis = "";
        playerRightDashAxis = "";
        wantedDirectionAngle = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float turn = Input.GetAxis(playerLeftAxis) - Input.GetAxis(playerRightAxis);
        float walk = Input.GetAxis(playerRightAxis) + Input.GetAxis(playerRightAxis);
        float dash = Mathf.Max(Input.GetAxis(playerLeftDashAxis), Input.GetAxis(playerRightDashAxis));

        wantedDirectionAngle += turn * playerTurnSpeed;

        Quaternion wantedDirectionQuaternion = Quaternion.Euler(0, wantedDirectionAngle, 0);

        Vector3 moveVector = new Vector3(0, 0, walk);
    }
}
