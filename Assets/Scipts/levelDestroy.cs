using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerLocation;
    // Update is called once per frame
    void Update()
    {
        if (playerLocation != null)
        {
            if ( playerLocation.transform.position.y - this.transform.position.y  > 10)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void setTransform(Transform newTransform)
    {
        this.playerLocation = newTransform;
    }
}
