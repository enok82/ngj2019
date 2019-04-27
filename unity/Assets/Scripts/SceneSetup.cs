using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public GameObject playerOne;

    public GameObject playerTwo;
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.RegisterPlayer(playerOne, playerTwo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
