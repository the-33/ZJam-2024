using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
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

    public dialogBox dialogbox;

    public bool carWorks;

    public bool stopTimer;

    public float time;

    public bool isNaked;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<playerMovement>();
        canInteract = false;
        isInteracting = false;
        interactableName = "";
        fadeInOut = GetComponent<fadeInOut>();
        carWorks = Random.Range(0, 2) == 0 ? true:false ;
        stopTimer = false;
        isNaked = false;

        time = 90;
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

        if(!stopTimer)
        {
            if (time > 0) time -= Time.deltaTime; else time = 0;
        }
        
        GameObject.Find("despertador").GetComponentInChildren<TextMeshProUGUI>().text = ((int)time).ToString();
    }

    IEnumerator interact()
    {
        isInteracting = true;
        switch (interactableName)
        {
            case "Wardrobe":
                if (isDressed) { dialogbox.showDialog("Ya me he vestido"); break; }
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                isDressed = true;
                isNaked = false;
                body.GetComponent<SpriteRenderer>().sprite = suitBody;
                break;
            case "Shower":
                if (isShowered) { dialogbox.showDialog("Ya me he duchado"); break; }
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                isNaked = true;
                isShowered = true;
                isDressed = false;
                body.GetComponent<SpriteRenderer>().sprite = nakedBody;
                head.GetComponent<SpriteRenderer>().sprite = combedHair;
                break;
            case "Fridge":
                if (hasEaten) { dialogbox.showDialog("Ya he comido"); break; }
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                hasEaten = true;
                break;
            case "Top":
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                transform.position = GameObject.Find("Top").transform.parent.Find("Bottom").transform.position;
                break;
            case "Bottom":
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                transform.position = GameObject.Find("Bottom").transform.parent.Find("Top").transform.position;
                break;
            case "Door":
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                GameObject.Find("Door").GetComponent<door>().useDoor();
                bocadillo.SetActive(false);
                break;
            case "Car":
                if(carWorks)
                {
                    fadeInOut.StartFadeSequence();
                    yield return new WaitForSeconds(1);
                    GameObject.Find("Car").GetComponent<door>().useDoor();
                    bocadillo.SetActive(false);
                }
                else
                {
                    dialogbox.showDialog("No me queda gasolina");
                }
                break;
            case "notmycar":
                dialogbox.showDialog("No es el mio"); break;
            case "Bed":
                dialogbox.showDialog("Llego tarde, no puedo dormir ahora"); break;
            case "Button":
                GameObject.Find("Button").GetComponent<BoxCollider2D>().enabled = false;
                bocadillo.SetActive(false);
                elevator e = GameObject.Find("Elevator").GetComponent<elevator>();
                StartCoroutine(e.openDoors());
                break;
            case "Elevator":
                fadeInOut.StartFadeSequence();
                yield return new WaitForSeconds(1);
                GameObject.Find("Elevator").GetComponent<door>().useDoor();
                bocadillo.SetActive(false);
                break;
            case "Boss":
                if(GameObject.Find("Boss").GetComponent<boss>().chillness != 6)
                {
                    if(!isShowered) dialogbox.showDialog("Has llegado a tiempo, pero hueles fatal... DESPEDIDO POR IMPRESENTABLE");
                    else if(!isDressed)
                    {
                        if(isNaked) dialogbox.showDialog("Has llegado a tiempo, enhorabuena... PERO QUE HACES DESNUDO!!!!, CIERRA LA PUERTA POR FUERA ESTAS DESPEDIDO");
                        else dialogbox.showDialog("Has llegado a tiempo, pero que haces en pijama?!, anda tomate el resto de dias libres...");
                    }
                    else dialogbox.showDialog("Has estado a segundos de estar despedido, pero bueno por lo menos estas presentable, que no se vuelva a repetir");
                }
                else
                {
                    dialogbox.showDialog("ESTAS DESPEDIDO!");
                }
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
            dialogbox.closeDialog();
        }
    }
}
