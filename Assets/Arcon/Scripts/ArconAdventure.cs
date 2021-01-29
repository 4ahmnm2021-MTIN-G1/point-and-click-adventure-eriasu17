using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArconAdventure : MonoBehaviour
{
    //Action Panel
    public Text dialogFenster;
    public GameObject actionPanel;
    public GameObject item1;
    public GameObject item2;
    public GameObject teaCan;
    public GameObject knife;
    public GameObject stageChanger1to2;
    public GameObject stageChanger2to3;
    public GameObject woodplayerPos;
    public GameObject interact1;
    public GameObject interact2;
    public GameObject woodTel1;

    //Movment Variables
    public Transform player;
    public Vector3 mousePos;
    public Vector3 worldPos;
    public Vector3 playerPos;
    public Vector2 worldPos2D;
    public Vector2 targetPos;
    RaycastHit2D rayhit;

    public float speed = 0.1f;
    public bool isMoving;
    public bool fade;
    public bool changeCam;
    
    private bool tea;
    private bool acPan;
    private bool moos1;
    private bool moos2;
    private bool sword;
    private bool swordCol;



    public Animator transition;
    public Canvas uiInterface;
    public Camera mainCam;
    public GameObject woodCam;

    void Start()
    {
        mainCam.enabled = true;
    }
    public void Update()
    {
        changeCam = transition.GetBool("ChangeCamPos");
        mousePos = Input.mousePosition;
        worldPos = mainCam.ScreenToWorldPoint(mousePos);
        worldPos2D = new Vector2(worldPos.x, worldPos.y);
        rayhit = Physics2D.Raycast(worldPos2D, Vector2.zero);

        if(rayhit.collider!= null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (rayhit.collider.gameObject.tag == "Ground")
                {
                    targetPos = rayhit.point;
                    isMoving = true;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (rayhit.collider.gameObject.tag == "Item")
                {
                    actionPanel.SetActive(true);
                    acPan = true;
                    actionPanel.transform.position = item1.transform.position;
                    tea = true;
                }
                if (rayhit.collider.gameObject.tag == "Item2")
                {
                    actionPanel.SetActive(true);
                    acPan = true;
                    actionPanel.transform.position = item2.transform.position;
                    swordCol = true;
                }
                if (rayhit.collider.gameObject.tag == "Item3")
                {

                }
                if(rayhit.collider.gameObject.tag == "Moos1")
                {
                    actionPanel.SetActive(true);
                    acPan = true;
                    actionPanel.transform.position = interact1.transform.position;
                    moos1 = true;
                }
                if (rayhit.collider.gameObject.tag == "Moos2")
                {
                    actionPanel.SetActive(true);
                    acPan = true;
                    actionPanel.transform.position = interact2.transform.position;
                    moos2 = true;
                }
                if (rayhit.collider.gameObject.tag == "Baum")
                {

                }
                if (rayhit.collider.gameObject.tag == "Snow")
                {

                }
            }
            if (Input.GetMouseButtonDown(2))
            {
                if (rayhit.collider.gameObject.tag == "Stage2")
                {
                    transition.SetBool("Start", true);
                    StartCoroutine(playerPort());
                    player.position = woodplayerPos.transform.position;
                }
                if (rayhit.collider.gameObject.tag == "Stage3")
                {
                    transition.SetBool("Start", true);
                }
                if (rayhit.collider.gameObject.tag == "Stage4")
                {
                    transition.SetBool("Start", true);
                }
                if (rayhit.collider.gameObject.tag == "Stage5")
                {
                    transition.SetBool("Start", true);
                }
            }
        }
       
        if (changeCam)
        {
            ChangeCamPos();
        }
    }
    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            player.position = Vector3.MoveTowards(player.position, targetPos, speed);
            print("Bewegung läuft");

            if(player.position.x == targetPos.x && player.position.y == targetPos.y)
            {
                isMoving = false;
                print("Bewegung abgeschloßen!");
            }
        }
    }

    public void ChangeCamPos()
    {
        mainCam.transform.position = woodCam.transform.position;
    }

    public void CollectItem()
    {
        if(tea == true)
        {
            teaCan.SetActive(true);
            item1.SetActive(false);
            dialogFenster.text = "Ich muss sie mit nehemn wenn sie mir von Nutzen sein soll!";
            StartCoroutine(ClearText());
            stageChanger1to2.SetActive(true);
            tea = false;
        }
        if(moos1 == true)
        {
            dialogFenster.text = "Ich will hier keine Wurzeln schlagen!";
            StartCoroutine(ClearText());
        }
        if(moos2 == true)
        {
            dialogFenster.text = "Wie soll ich das anstellen?";
            StartCoroutine(ClearText());
        }
        if(swordCol == true)
        {
            knife.SetActive(true);
            item2.SetActive(false);
            dialogFenster.text = "Das wird mir sehr nützlich sein!";
            StartCoroutine(ClearText());
            sword = true;
        }
    }

    public void exitOverlay()
    {
        if(acPan == true)
        {
            actionPanel.SetActive(false);
            tea = false;
            moos1 = false;
            moos2 = false;
            swordCol = false;
        }
    }

    public void InspectObject()
    {
        if(tea == true)
        {
            dialogFenster.text = "Da ist Sie die schöne Sirupkanne für das Hirschfest!";
            StartCoroutine(ClearText());
        }
        if(moos1 == true)
        {
            dialogFenster.text = "Eine große Menge Moos blockiert meinen Weg!";
            StartCoroutine(ClearText());
        }
        if(moos2 == true)
        {
            dialogFenster.text = "Eine große Menge Moos blockiert schon wieder meinen Weg!";
            StartCoroutine(ClearText());
        }
        if(swordCol == true)
        {
            dialogFenster.text = "Das legendäre Dornenschwert!";
            StartCoroutine(ClearText());
        }
    }

    public void UseObjects()
    {
        if(tea == true)
        {
            dialogFenster.text = "Siehst du was was ich hier verwenden könnte?";
            StartCoroutine(ClearText());
        }
        if(moos1 == true)
        {
            if(sword == true)
            {
                dialogFenster.text = "Nimm das du dämliches Wiesengrass!";
                StartCoroutine(ClearText());
                interact1.SetActive(false);
                woodTel1.SetActive(true);
                moos1 = false;
            }
            else
            {
                dialogFenster.text = "Was soll ich deiner Meinung nach anstellen?";
                StartCoroutine(ClearText());
            }
        }
        if (moos2 == true)
        {
            if (sword == true)
            {
                dialogFenster.text = "Nimm das du dämliches Wiesengrass!";
                StartCoroutine(ClearText());
                interact2.SetActive(false);
                stageChanger2to3.SetActive(true);
                moos2 = false;
                sword = false;
            }
            else
            {
                dialogFenster.text = "Was soll ich deiner Meinung nach anstellen?";
                StartCoroutine(ClearText());
            }
        }
        if (swordCol == true)
        {
            dialogFenster.text = "Ich hab nix um hier was zu machen?";
            StartCoroutine(ClearText());
        }
    }

    public IEnumerator ClearText()
    {
        yield return new WaitForSeconds(5);
        dialogFenster.text = "";
    }

    public IEnumerator playerPort()
    {
        yield return new WaitForSeconds(2);
        print("yep it works boy");
    }
}
