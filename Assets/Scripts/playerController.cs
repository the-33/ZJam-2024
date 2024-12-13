using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public bool isShowered;
    public bool isDressed;
    public bool hasEaten;

    public Sprite suitBody;
    public Sprite nakedBody;
    public Sprite combedHair;

    public GameObject body;
    public GameObject head;

    public GameObject bocadillo;
    public float bocadilloVOffset;
    public float bocadilloHoffset;

    private playerMovement pm;

    private bool canInteract;
    public bool isInteracting;
    private string interactableName;

    private fadeInOut fadeInOut;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<playerMovement>();
        canInteract = false;
        isInteracting = false;
        interactableName = "";
        fadeInOut = GetComponent<fadeInOut>();
    }

    // Update is called once per frame
    void Update()
    {
        bocadillo.transform.position = new Vector3(transform.position.x + bocadilloHoffset, transform.position.y + bocadilloVOffset, 0);

        if(canInteract && !isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(interact());
            }
        }
    }

    IEnumerator interact()
    {
        isInteracting = true;
        switch (interactableName)
        {
            case "Wardrobe":
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                isDressed = true;
                body.GetComponent<SpriteRenderer>().sprite = suitBody;
                break;
            case "Shower":
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                isShowered = true;
                body.GetComponent<SpriteRenderer>().sprite = nakedBody;
                head.GetComponent<SpriteRenderer>().sprite = combedHair;
                break;
            case "Fridge":
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                hasEaten = true;
                break;

        }
        isInteracting = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pm.m_HorizontalInput == 0)
        {
            if ((collision.gameObject.layer == 6 || collision.gameObject.layer == 7) && !bocadillo.activeSelf) bocadillo.SetActive(true);
            canInteract = true;
            interactableName = collision.name;
        }
        else
        {
            bocadillo.SetActive(false);
            canInteract = false;
        }
    }
}
