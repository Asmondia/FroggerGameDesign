using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y != player.transform.position.y)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, player.transform.position.y,-10f), 3f * Time.deltaTime);
        }
    }
}
