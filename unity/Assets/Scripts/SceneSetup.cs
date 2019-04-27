using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public GameObject playerOne;

    public GameObject playerTwo;

    public LevelScript levelScript;
    // Start is called before the first frame update
    
    void Awake()
    {
        GameManager.Instance.RegisterPlayer(playerOne, playerTwo);
        GameManager.Instance.RegisterLevelScript(levelScript);

        playerOne.GetComponent<PlayerMovement>().Respawn();
        playerTwo.GetComponent<PlayerMovement>().Respawn();
    }

    private void Start()
    {
        GameManager.Instance.StartGame();
    }
}
