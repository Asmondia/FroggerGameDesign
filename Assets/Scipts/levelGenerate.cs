using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenerate : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentLevelEnd = 7.5f;
    private Transform myTransform;
    /*
    [SerializeField] private GameObject justCows;
    [SerializeField] private GameObject justWater;
    [SerializeField] private GameObject road2water;
    [SerializeField] private GameObject water2road;
    */
    [SerializeField] private GameObject waterSmallBridge;
    [SerializeField] private GameObject waterLargeBridge;
    [SerializeField] private GameObject road2water;
    [SerializeField] private GameObject water2road;
    [SerializeField] private GameObject road2grass;
    [SerializeField] private GameObject grass2road;
    [SerializeField] private GameObject grassMid;
    [SerializeField] private GameObject water2grass;
    [SerializeField] private GameObject grass2water;
    [SerializeField] private GameObject roadMid;
    private int numberOfSame = 0;

    private List<GameObject> levelComponents = new List<GameObject>();
    private string lastPlaced = "waterStart";
    private bool canCreate = true;

    void Start()
    {
        myTransform = transform;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (myTransform.position.y + 15f > currentLevelEnd)
        {
            generateLevel();
        }
    }
 
    private void generateLevel()
    {
        if (canCreate)
        {
            canCreate = false;


            if (lastPlaced == "water" || lastPlaced == "waterStart")
            {
                lastPlaceWater();
            }
            else if (lastPlaced == "road" || lastPlaced == "roadStart")
            {

                lastPlaceRoad();
            }
            else if (lastPlaced == "grassStart")
            {
                lastPlaceGrassStart();
            }
            else if (lastPlaced == "grass")
            {
                lastPlaceGrass();
            }
        }
    } 
    private int chanceToEnd(){
        return (6 - numberOfSame);
    }
    
    private void lastPlaceWater()
    {
        if(lastPlaced != "waterStart"){
            var endChance = Random.Range(1, chanceToEnd());
            if(endChance == 1){
                var newLand = Random.Range(1,10);
                if (newLand == 1){
                    water2grassCreate();
                } else {
                    water2roadCreate();
                }
            } else {
                numberOfSame += 1;
                var bridgeType = Random.Range(1,3);
                if (bridgeType == 1){
                    waterSmallBridgeCreate();
                } else {
                    waterLargeBridgeCreate();
                }
            }
        } else {
            numberOfSame += 1;
            var bridgeType = Random.Range(1,3);
            if (bridgeType == 1){
                waterSmallBridgeCreate();
            } else {
                waterLargeBridgeCreate();
            }
        }
    }

    private void lastPlaceRoad()
    {
        if(lastPlaced != "roadStart"){
            var endChance = Random.Range(1, chanceToEnd());
            if(endChance == 1){
                var newLand = Random.Range(1,10);
                if (newLand == 1){
                    road2grassCreate();
                } else {
                    road2waterCreate();
                }
            } else {
                numberOfSame += 1;
            roadMidCreate();
            }
        } else {
            numberOfSame += 1;
            roadMidCreate();
        }
    }

    private void lastPlaceGrassStart()
    { 
        grassMidCreate();
    }

    private void lastPlaceGrass()
    {
        var random = Random.Range(1, 3);
        if (random ==1 ){
            grass2roadCreate();
        } else {
            grass2waterCreate();
        }
    }
    private void setChildValues(GameObject parent)
    {
        int speedPre = (Random.Range(20, 150));
        float randomSpeed = speedPre / 100f;

        int randomLeft= Random.Range(1, 3);
        bool goLeft = true;
        if (randomLeft == 1)
        {
            goLeft = false;
        }
        for (int i = 0; i < parent.transform.GetChild(0).childCount; i++)
        {
            moveObject child = parent.transform.GetChild(0).GetChild(i).GetComponent<moveObject>();
            child.setLeft(goLeft);
            child.setSpeed(randomSpeed);
        }
    }
    private void waterSmallBridgeCreate()
    {
        GameObject created = Instantiate(waterSmallBridge, new Vector3(0f, currentLevelEnd +2.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        setChildValues(created);
        lastPlaced = "water";
        canCreate = true;
        levelComponents.Add(created);

    }
    private void waterLargeBridgeCreate()
    {
        GameObject created = Instantiate(waterLargeBridge, new Vector3(0f, currentLevelEnd + 2.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        setChildValues(created);
        lastPlaced = "water";
        canCreate = true;
        levelComponents.Add(created);

    }
    private void water2roadCreate()
    {
        numberOfSame = 0;
        GameObject created = Instantiate(water2road, new Vector3(0f, currentLevelEnd + 1.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        lastPlaced = "roadStart";
        canCreate = true;
        levelComponents.Add(created);
    }
    private void road2waterCreate()
    {
        numberOfSame = 0;
        GameObject created = Instantiate(road2water, new Vector3(0f, currentLevelEnd + 1.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        lastPlaced = "waterStart";
        canCreate = true;
        levelComponents.Add(created);
    }
    private void road2grassCreate()
    {
        numberOfSame = 0;
        GameObject created = Instantiate(road2grass, new Vector3(0f, currentLevelEnd + 2.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        lastPlaced = "grassStart";
        canCreate = true;
        levelComponents.Add(created);
    }
    private void grass2roadCreate()
    {
        numberOfSame = 0;
        GameObject created = Instantiate(grass2road, new Vector3(0f, currentLevelEnd + 2.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        lastPlaced = "roadStart";
        canCreate = true;
        levelComponents.Add(created);
    }
    private void grassMidCreate()
    {
        GameObject created = Instantiate(grassMid, new Vector3(0f, currentLevelEnd +2.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        lastPlaced = "grass";
        canCreate = true;
        levelComponents.Add(created);
    }
    private void water2grassCreate()
    {
        numberOfSame = 0;
        GameObject created = Instantiate(water2grass, new Vector3(0f, currentLevelEnd +2.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        lastPlaced = "grassStart";
        canCreate = true;
        levelComponents.Add(created);
    }
    private void grass2waterCreate()
    {
        numberOfSame = 0;
        GameObject created = Instantiate(grass2water, new Vector3(0f, currentLevelEnd +2.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        lastPlaced = "waterStart";
        canCreate = true;
        levelComponents.Add(created);

    }
    private void roadMidCreate()
    {
        GameObject created = Instantiate(roadMid, new Vector3(0f, currentLevelEnd +2.5f, 0f), Quaternion.identity);
        currentLevelEnd += 1f;
        created.GetComponent<levelDestroy>().setTransform(myTransform);
        setChildValues(created);
        lastPlaced = "road";
        canCreate = true;
        levelComponents.Add(created);
    }
    public void resetLevel()
    {
        foreach(GameObject levelSlice in levelComponents){
            Destroy(levelSlice);
        }
        GameObject[] powerUp;
        powerUp = GameObject.FindGameObjectsWithTag("invulnPowerUp");
        foreach (GameObject power in powerUp)
        {
            Destroy(power);
        }
        powerUp = GameObject.FindGameObjectsWithTag("boostPowerUp");
        foreach (GameObject power in powerUp)
        {
            Destroy(power);
        }
        powerUp = GameObject.FindGameObjectsWithTag("speedPowerUp");
        foreach (GameObject power in powerUp)
        {
            Destroy(power);
        }
        this.currentLevelEnd = 7.5f;
        this.lastPlaced = "waterStart";
    }
}
