using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playButton : MonoBehaviour
{
    private SceneSwapScript sceneChange;
    private void Awake()
    {
        sceneChange = GameObject.Find("SceneSwap").GetComponent<SceneSwapScript>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            sceneChange.LeaveScene("SampleScene");

        }
    }
    // Start is called before the first frame update
    

}
