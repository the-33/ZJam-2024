using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private bool isShowered;
    private bool isDressed;
    private bool hasEaten;

    [SerializeField]
    private GameObject bocadillo;

    private playerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<playerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pm.m_HorizontalInput == 0)
        {
            if (collision.gameObject.layer == 6 || collision.gameObject.layer == 7) bocadillo.SetActive(true);
        }
        else
        {
            bocadillo.SetActive(false);
        }
    }
}
