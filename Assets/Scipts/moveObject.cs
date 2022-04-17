using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool goLeft;


    // Update is called once per frame
    void Update()
    {
        if (goLeft)
        {
            if (this.transform.position.x <= -13)
            {
                this.transform.position = new Vector3(13, this.transform.position.y, this.transform.position.z);
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(-15f, this.transform.position.y, 0), 3f * speed* Time.deltaTime);
        }
        else
        {
            if (this.transform.position.x >= 13)
            {
                this.transform.position = new Vector3(-13, this.transform.position.y, this.transform.position.z);
            }
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(15f, this.transform.position.y, 0), 3f * speed * Time.deltaTime);
        }
    }
    public float getSpeed()
    {
        return speed;
    }
    public bool getLeft()
    {
        return goLeft;
    }
}