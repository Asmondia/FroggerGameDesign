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
    private Vector2Int targetSpace;
    private Vector2Int previousSpace;
    // Start is called before the first frame update
    private void Awake()
    {

        mySpeed = setSpeed;
        rb2D = GetComponent<Rigidbody2D>();
        myTransform = transform;
        targetSpace = new Vector2Int(Mathf.RoundToInt(myTransform.position.x), Mathf.RoundToInt(myTransform.position.y));
        previousSpace = targetSpace;
        
    }

    // Update is called once per frame
    private void Update()
    {
        var moving = (Vector2)myTransform.position != targetSpace;
        if (moving)
        {
            MoveTowardsTargetSpace();
        }
        else
        {
            previousSpace = targetSpace;
            SetTargetSpace();
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Help");
        targetSpace = previousSpace;
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
                targetSpace += Vector2Int.up;
            }

            else if (Input.GetKey(KeyCode.S))
            {
                targetSpace += Vector2Int.down;
            }
        }
   
        else if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                targetSpace += Vector2Int.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                targetSpace += Vector2Int.right;
            }
        }
    }
   
}
