using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentLevelEnd = 5.5f;
    private Transform myTransform;
    [SerializeField] private GameObject justCows;
    private GameObject lastPlaced = null;

    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (myTransform.position.y + 15f > currentLevelEnd)
        {
            generateLevel();
        }
    }
    private void generateLevel()
    {
        if(lastPlaced == null)
        {
            
            Instantiate(justCows,new Vector3(1f, currentLevelEnd + 1.5f, 0f), Quaternion.identity);
            currentLevelEnd += 3f;
            lastPlaced = justCows;
        } else if (lastPlaced == justCows)
        {

            Instantiate(justCows,new Vector3(1f, currentLevelEnd + 1.5f, 0f), Quaternion.identity);
            currentLevelEnd += 3f;
            lastPlaced = justCows;
        }
    }
}
