using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{

    public TextMeshProUGUI playerOneDeaths;
    public TextMeshProUGUI playerTwoDeaths;

    private PlayerMovement playerOne;
    private PlayerMovement playerTwo;


    private void Start()
    {
        playerOne = GameManager.Instance.playerOne.GetComponent<PlayerMovement>();
        playerTwo = GameManager.Instance.playerTwo.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        playerOneDeaths.text = playerOne.deathCount.ToString();
        playerTwoDeaths.text = playerTwo.deathCount.ToString(); 

    }
}
