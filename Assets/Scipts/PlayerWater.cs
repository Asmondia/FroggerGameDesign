using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerWater : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Transform myTransform;
    private BoxCollider2D bc;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        myTransform = transform;
    }

    private void onTriggerEnter2D(Collider2D collision)
    {

        string collisonName = collision.name;
        Debug.Log(collisonName);
    }


}
