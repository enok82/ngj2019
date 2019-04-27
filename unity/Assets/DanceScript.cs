using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceScript : MonoBehaviour
{
    private int danceNo;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        danceNo = Random.Range(0, 6);
        anim = GetComponent<Animator>();
        anim.SetInteger("Dance", danceNo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
