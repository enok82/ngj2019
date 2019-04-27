using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions.Must;

public class ScoreUI : MonoBehaviour
{

    public TextMeshProUGUI playerOneDeaths;
    public TextMeshProUGUI playerTwoDeaths;
    public TextMeshProUGUI countDownTimer;
    private float countDowntime;

  


    private PlayerMovement playerOne;
    private PlayerMovement playerTwo;


    private void Start()
    {
        playerOne = GameManager.Instance.playerOne.GetComponent<PlayerMovement>();
        playerTwo = GameManager.Instance.playerTwo.GetComponent<PlayerMovement>();

       countDowntime = GameManager.Instance.countDownTime;
       countDownTimer.enabled = true;



    }

    void Update()
    {
        countDowntime -= Time.deltaTime;
        countDownTimer.text = countDowntime.ToString("#.0");
        if (countDowntime < 0 )
        {
            countDownTimer.enabled = false;
        }
  
        playerOneDeaths.text = playerOne.deathCount.ToString();
        
        
        playerTwoDeaths.text = playerTwo.deathCount.ToString(); 

    }
}
