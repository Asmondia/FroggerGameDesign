using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float setSpeed;
    private float mySpeed;
    private Rigidbody2D rb2D;
    private Transform myTransform;
    private Vector2 targetSpace;
    private Vector2 previousSpace;
    private bool isOnWater = false;
    private bool isOnSafe = false;
    private int deathCounter = 0;
    private float floatSpeed = 0;
    // Start is called before the first frame update
    private void Awake()
    {

        mySpeed = setSpeed;
        rb2D = GetComponent<Rigidbody2D>();
        myTransform = transform;
        targetSpace = new Vector2(Mathf.RoundToInt(myTransform.position.x)+0.5f, Mathf.RoundToInt(myTransform.position.y)+0.7f);
        previousSpace = targetSpace;
        
    }

    // Update is called once per frame
    private void Update()
    {
        var moving = Vector2.Distance((Vector2)myTransform.position,targetSpace) >= .05f;

        MoveTowardsTargetSpace();
        if (!moving)
        {
            previousSpace = targetSpace;
            SetTargetSpace();
        } 
        
        if (isOnWater)
        {   if (!isOnSafe)
            {
                deathCounter += 1;
                if(deathCounter > 5)
                {
                    Debug.Log("Dead");
                }
            }
            else
            {
                targetSpace.x += 3f * floatSpeed * Time.deltaTime;
                if (targetSpace.x > 7.5f)
                {
                    targetSpace.x = 7.5f;
                }
                else if (targetSpace.x < -7.5f)
                {
                    targetSpace.x = -7.5f;
                }
                deathCounter = 0;
            }
        } else
        {
            deathCounter = 0;
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisonName = collision.gameObject.name;
        Debug.Log(collisonName);
        //Debug.Log(collisonName == "TilemapHillsTop" || collisonName == "TilemapHillsBot");
        if(collisonName == "TilemapHillsTop" || collisonName == "TilemapHillsBot")
        {
            Debug.Log("How did i get here");
            targetSpace = previousSpace;
        } else if (collisonName == "Water")
        {
            isOnWater = true;
        } else if (collisonName == "bridgeSmall")
        {
            isOnSafe = true;
            moveObject script = collision.gameObject.GetComponent<moveObject>();
            if (script.getLeft())
            {
                floatSpeed = -1f * script.getSpeed();
            }
            else
            {
                floatSpeed = 1f * script.getSpeed();
            }
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Trigger Exit");
        string collisonName = collision.gameObject.name;
        if (collisonName == "Water")
        {
            isOnWater = false;
        }
        else if (collisonName == "bridgeSmall")
        {
            isOnSafe = false;
        }
    }



    private void MoveTowardsTargetSpace()
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, targetSpace, mySpeed * Time.deltaTime);
    }
    private void SetTargetSpace()
    {
        if(Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
        {
            if(Input.GetKey(KeyCode.W))
            {
                //targetSpace += Vector2Int.up;
                targetSpace.y += 0.3f;
                targetSpace.y = Mathf.RoundToInt(targetSpace.y);
                targetSpace.y += 0.7f;

            }

            else if (Input.GetKey(KeyCode.S))
            {
                //targetSpace += Vector2Int.down;
                targetSpace.y += -0.7f;
                targetSpace.y = Mathf.RoundToInt(targetSpace.y);
                targetSpace.y += -0.3f;

            }
        }
   
        else if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                //targetSpace += Vector2Int.left;
                targetSpace.x += -0.5f;
                targetSpace.x = Mathf.RoundToInt(targetSpace.x);
                targetSpace.x += -0.5f;
                if (targetSpace.x < -7.5f)
                {
                    targetSpace.x = -7.5f;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                targetSpace.x += 0.5f;
                targetSpace.x = Mathf.RoundToInt(targetSpace.x);
                targetSpace.x += 0.5f;
                if (targetSpace.x > 7.5f)
                {
                    targetSpace.x = 7.5f;
                }
            }
        }
    }
   
}
