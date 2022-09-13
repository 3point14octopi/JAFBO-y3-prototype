using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{

    private bool canMove = false;
    private bool canChop = false;
    private GameObject currentTile;
    private int lookDir = 0;
    private bool hasStick = false;
    private bool canSmek = false;
    private bool canSwim = false;
    private bool isSoggy = false;
    public float statusEffectTime = 0.0f;
    public Material soggyEffect;
    public Material normalMat;

    public GameObject branch;
    private GameObject heldItem;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().size = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.GetComponent<BoxCollider>().center = new Vector3(1.5f, 0f, 0f);
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0,0,0);
            lookDir = 0;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            lookDir = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            lookDir = 2;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
            lookDir = 3;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {

            switch (lookDir)
            {
                case 0:
                    transform.position = transform.position + new Vector3(1, 0, 0);
                    break;
                case 1:
                    transform.position = transform.position + new Vector3(0, 0, -1);
                    break;
                case 2:
                    transform.position = transform.position + new Vector3(-1, 0, 0);
                    break;
                case 3:
                    transform.position = transform.position + new Vector3(0, 0, 1);
                    break;
            }
            canMove = false;
            Debug.Log("Player Moved");
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canChop)
        {
            Destroy(currentTile);
             heldItem = Instantiate(branch,transform.position, Quaternion.identity);
            switch (lookDir)
            {
                case 0:
                    heldItem.transform.eulerAngles = new Vector3(0, 0, -57);
                    break;
                case 1:
                    heldItem.transform.eulerAngles = new Vector3(0, 90, -57);
                    break;
                case 2:
                    heldItem.transform.eulerAngles = new Vector3(0, 180, -57);
                    break;
                case 3:
                    heldItem.transform.eulerAngles = new Vector3(0, 270, -57);
                    break;
            }
            heldItem.transform.SetParent(transform);
            heldItem.transform.position = transform.position + new Vector3(0,0,0.3f);
            canChop = false;
            Debug.Log("Player Chopped");
            hasStick = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canSmek)
        {
            Destroy(currentTile);
            Destroy(heldItem);
            canSmek = false;
            hasStick = false;
            Debug.Log("Player Smeked");
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canSwim)
        {
            isSoggy = true;
            canSwim = false;
            Debug.Log("Player Swimed");
        }


        if (isSoggy)
        {
            gameObject.GetComponent<MeshRenderer>().material = soggyEffect;
            statusEffectTime = 2;
            isSoggy = false;
        }

        if (statusEffectTime > 0)
        {
            statusEffectTime -= Time.deltaTime;
        }
        else 
        {
            gameObject.GetComponent<MeshRenderer>().material = normalMat;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        currentTile = other.gameObject;
        if (other.CompareTag("Ground"))
        {
            Debug.Log("Ground Tile Found");
            canMove = true;
        }
        else if (other.CompareTag("Rock"))
        {
            Debug.Log("Rock Tile Found");
            if (hasStick)
            {
                canSmek = true;
            }
        }
        else if (other.CompareTag("Water"))
        {
            Debug.Log("Water Tile Found");
            canSwim = true;
        }
        else if (other.CompareTag("Tree"))
        {
            Debug.Log("Tree Tile Found");
            if (!hasStick)
            {
                canChop = true;
            }
            
        }

    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Tile Left");
        canMove = false;
        canChop = false;
        canSmek = false;
        canSwim = false;
    }
}
