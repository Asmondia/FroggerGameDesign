using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowMove : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= 13)
        {
            this.transform.position = new Vector3(-13, this.transform.position.y, this.transform.position.z);
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(15f, this.transform.position.y, 0), 3f * Time.deltaTime);
    }
}
