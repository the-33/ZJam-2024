using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    public SpriteRenderer head;
    public Sprite[] heads;
    public int chillness;

    // Start is called before the first frame update
    void Start()
    {
        chillness = 6;

        float time = GameObject.Find("Player").GetComponent<playerController>().time;
        GameObject.Find("Player").GetComponent<playerController>().stopTimer = true;

        chillness = (int) time / 15;

        head.sprite = heads[chillness];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
