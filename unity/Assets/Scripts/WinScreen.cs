using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI playerOneScore; 
    public TextMeshProUGUI playerTwoScore;
    public TextMeshProUGUI whoWon;

    public GameObject winScreen;


    private PlayerMovement playerOne;
    private PlayerMovement playerTwo;

    private void OnEnable()
    {
        GameManager.Instance.gameOverEvent += ShowDeathScore;
        winScreen.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.gameOverEvent -= ShowDeathScore;
        winScreen.SetActive(false);
    }

    void Start()
    {
       // playerOne = GameManager.Instance.playerOne.GetComponent<PlayerMovement>();
       // playerTwo = GameManager.Instance.playerTwo.GetComponent<PlayerMovement>();
    }

    
    void Update()
    {

        
    }



    private void ShowDeathScore()
    {

       // playerOneScore.text = playerOne.deathCount.ToString();
       // playerTwoScore.text = playerOne.deathCount.ToString();

        /*if (playerOne.wonGame == true)
        {
            whoWon.text = playerOne.name + "Has Won!";
        }*/
        
        
        winScreen.SetActive(true);

    }
    
    public void EnterMainMenu()
    {
        
        GameManager.Instance.MainMenu();
        
    }
}
