using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    [SerializeField] private GameObject player;

    void Update()
    {
        if (this.transform.position.y <= player.transform.position.y)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(this.transform.position.x, player.transform.position.y,-10f), 60f * Time.deltaTime);
        }
    }
    public void resetCamera()
    {
        this.transform.position = new Vector3(this.transform.position.x,player.transform.position.y + 3.3f,-10f);

    }
}
