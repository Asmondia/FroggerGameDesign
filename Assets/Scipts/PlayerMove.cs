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
    // Start is called before the first frame update
    private void Awake()
    {
        mySpeed = setSpeed / 100.0f;
        rb2D = GetComponent<Rigidbody2D>();
        myTransform = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();

    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
            rb2D.position += Vector2.up * mySpeed;
        if (Input.GetKey(KeyCode.S))
            rb2D.position += Vector2.down * mySpeed;
        if (Input.GetKey(KeyCode.A))
            rb2D.position += Vector2.left * mySpeed;
        if (Input.GetKey(KeyCode.D))
            rb2D.position += Vector2.right * mySpeed;

    }
}
