using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerMenuScript : MonoBehaviour
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
    private bool canMove = true;
    
    // Start is called before the first frame update
    private void Awake()
    {

        mySpeed = setSpeed;
        rb2D = GetComponent<Rigidbody2D>();
        myTransform = transform;
        targetSpace = new Vector2(Mathf.RoundToInt(myTransform.position.x) + 0.3f, Mathf.RoundToInt(myTransform.position.y) + 0.0f);
        previousSpace = targetSpace;
       

    }

    // Update is called once per frame
    private void Update()
    {
        var moving = Vector2.Distance((Vector2)myTransform.position, targetSpace) >= .05f;

        MoveTowardsTargetSpace();
        if (!moving)
        {
            previousSpace = targetSpace;
            SetTargetSpace();
        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisonName = collision.gameObject.name;

        if (collisonName == "TilemapHillsTop" || collisonName == "TilemapHillsBot")
        {
            targetSpace = previousSpace;
        } 


    }




    private void MoveTowardsTargetSpace()
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, targetSpace, mySpeed * Time.deltaTime);
    }
    private void SetTargetSpace()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    //targetSpace += Vector2Int.up;
                    targetSpace.y += 0.3f;
                    targetSpace.y = Mathf.RoundToInt(targetSpace.y);
                    targetSpace.y += 0.7f;
                    
                    if (targetSpace.y > 6.0f)
                    {
                        targetSpace.y = 6.0f;
                    }
                    

                }

                else if (Input.GetKey(KeyCode.S))
                {
                    //targetSpace += Vector2Int.down;
                    targetSpace.y += -0.7f;
                    targetSpace.y = Mathf.RoundToInt(targetSpace.y);
                    targetSpace.y += -0.3f;
                    
                    if (targetSpace.y < -1.0f)
                    {
                        targetSpace.y = -1.0f;
                    }
                    

                }
            }

            else if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    //targetSpace += Vector2Int.left;
                    targetSpace.x += -0.3f;
                    targetSpace.x = Mathf.RoundToInt(targetSpace.x);
                    targetSpace.x += -0.7f;
                    
                    if (targetSpace.x < -62.7f)
                    {
                        targetSpace.x = -62.7f;
                    }
                    
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    targetSpace.x += 0.7f;
                    targetSpace.x = Mathf.RoundToInt(targetSpace.x);
                    targetSpace.x += 0.3f;
                    
                    if (targetSpace.x > -47.7f)
                    {
                        targetSpace.x = -47.7f;
                    }
                    
                }
            }
        }
    }
}
