using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createRandomPowerUp : MonoBehaviour
{
    [SerializeField] public GameObject invulnPowerUp;
    [SerializeField] public GameObject boostPowerUp;
    [SerializeField] public GameObject speedPowerUp;


    // Start is called before the first frame update
    void Start()
    {
        var random = Random.Range(1, 10);
        if (random >= 1 && random <= 5)
        {
            Instantiate(invulnPowerUp, this.transform.position, Quaternion.identity);
        } else if(random >= 6 && random <= 8) {
            Instantiate(boostPowerUp, this.transform.position, Quaternion.identity);
        } else if (random >= 9 && random <= 10){
            Instantiate(speedPowerUp, this.transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
