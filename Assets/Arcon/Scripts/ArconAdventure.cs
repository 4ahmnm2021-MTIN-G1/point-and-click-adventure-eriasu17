using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArconAdventure : MonoBehaviour
{
    //Action Panel
    public GameObject actionPanel;
    public Transform item1;

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

        if (Input.GetMouseButtonDown(0))
        {
            if(rayhit.collider != null)
            {
                print("Objekt mit Collider getroffen");
                if(rayhit.collider.gameObject.tag == "Ground")
                {
                    targetPos = rayhit.point;
                    isMoving = true;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(rayhit.collider != null)
            {
                Debug.Log("Objekt wurde getroffen!");
                if(rayhit.collider.gameObject.tag == "Item")
                {
                    Debug.Log("ActionPanel is working!");
                    actionPanel.SetActive(true);
                    actionPanel.transform.position = item1.position;
                }
            }
        }
        if (Input.GetMouseButtonDown(2))
        {
            if(rayhit.collider != null)
            {
                Debug.Log("Level change");
                if (rayhit.collider.gameObject.tag == "Stage2")
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
}
