using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float setSpeed;
    private float mySpeed;
    private Rigidbody2D rb2D;
    private Transform myTransform;
    private Vector2 targetSpace;
    private Vector2 previousSpace;
    public bool isOnWater = false;
    public bool isOnSafe = false;
    private int deathCounter = 0;
    private float floatSpeed = 0;
    private float futhestForwards = 0.7f;
    private bool isInvuln = false;
    private bool isBoosting = false;
    private GameObject deathScreen;
    private bool isAlive = true;
    private bool moving;
    Animator animator;
    private AchievementManager achievManager;
    private bool hasTriedBack = false;
    [SerializeField] public LayerMask wallLayer;
    [SerializeField] public LayerMask waterLayer;
    [SerializeField] public LayerMask safeLayer;

    // Start is called before the first frame update
    private void Awake()
    {

        mySpeed = setSpeed;
        rb2D = GetComponent<Rigidbody2D>();
        deathScreen = GameObject.Find("DeathScreen");
        deathScreen.SetActive(false);
        myTransform = transform;
        targetSpace = new Vector2(Mathf.RoundToInt(myTransform.position.x)+0.5f, Mathf.RoundToInt(myTransform.position.y)+0.7f);
        previousSpace = targetSpace;
        animator = GetComponent<Animator>();
        achievManager = FindObjectOfType<AchievementManager>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        moving = Vector2.Distance((Vector2)myTransform.position, targetSpace) >= .2f;
        if (myTransform.position.y > futhestForwards)
        {
            futhestForwards = myTransform.position.y;
        }
        MoveTowardsTargetSpace();
        checkAlive();
        if (isAlive)
        {
            animator.SetBool("isInvuln", isInvuln);
            animator.SetBool("isBoosting", isBoosting);
            
            if (!moving)
            {
                previousSpace = targetSpace;
                SetTargetSpace();
            }

            if (isOnWater)
            {
                if (!isOnSafe && !isInvuln)
                {
                    deathCounter += 1;
                    if (deathCounter > 6 && isAlive)
                    {
                        

                        death();
                    }
                }


            }
            if (isOnSafe)
            {

                targetSpace.x += 3f * floatSpeed * Time.deltaTime;
                if (targetSpace.x > 7.5f)
                {
                    targetSpace.x = 7.5f;
                }
                else if (targetSpace.x < -7.5f)
                {
                    targetSpace.x = -7.5f;
                }
            }
        } else {
            if(deathCounter == 0){
                deathCounter = 1;
                death();
            }
        }
   
        

    }
    private void checkAlive(){
        Collider2D groundCheck = Physics2D.OverlapCircle(myTransform.position, 0.3f, safeLayer);
        if (groundCheck != null){
            isOnSafe = true;
            moveObject moveScript = groundCheck.GetComponent<moveObject>();
            floatSpeed = moveScript.getSpeed();
            if(moveScript.getLeft()){
                floatSpeed = floatSpeed * -1;
            }
        } else {
            Collider2D waterCheck = Physics2D.OverlapCircle(myTransform.position, 0.1f, waterLayer);
            isOnSafe = false;
            if(waterCheck != null && !moving){
                isAlive = false;
            }
        }
    }
    private void death()
    {
        isAlive = false;
        deathScreen.SetActive(true);
        int score = (int)(previousSpace.y + 3.3f) ;
        if (score > 0)
        {
            achievManager.NotifyAchievement(2, "First Steps!");
        }
        if (score > 10)
        {
            achievManager.NotifyAchievement(3, "Getting Places!");
        }
        if (score > 50)
        {
            achievManager.NotifyAchievement(4, "The big 5 0!");
        }
        if (score > 100)
        {
            achievManager.NotifyAchievement(5, "I never thought you would get this far!");
        }
        deathScreen.transform.GetChild(0).GetChild(3).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score.ToString();
        if (Manager.data.getHighScore() < score)
        {
            Manager.data.setHighScore(score);
            deathScreen.transform.GetChild(0).GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = "New High Score!";
        }
        else
        {
            deathScreen.transform.GetChild(0).GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text = "High Score: " + Manager.data.getHighScore().ToString();
            
        }
        Manager.saveGame();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisonName = collision.gameObject.name;

        if (collision.gameObject.tag == "invulnPowerUp")
        {
            Destroy(collision.gameObject);
            isInvuln = true;
            StartCoroutine(stopInvuln(3f));

        }
        else if (collision.gameObject.tag == "boostPowerUp")
        {
            isBoosting = true;
            Destroy(collision.gameObject);
            isInvuln = true;
            targetSpace.y += 20f;
            mySpeed = 10 * setSpeed;
            StartCoroutine(stopBoost(0.4f));
        }
        else if (collision.gameObject.tag == "speedPowerUp")
        {
            Destroy(collision.gameObject);
            mySpeed = setSpeed * 3;
            StartCoroutine(stopSpeed(2f));

        }
        else if (collision.gameObject.tag == "cow")
        {
            if (!isInvuln && isAlive)
            {
                death();
            }
            
        }

    }
    IEnumerator stopInvuln(float duration)
    {
        yield return new WaitForSeconds(duration);// Wait a bit
        isInvuln = false;
        

    }
    IEnumerator stopSpeed(float duration)
    {
        yield return new WaitForSeconds(duration);// Wait a bit
        mySpeed = setSpeed;


    }


    IEnumerator stopBoost(float duration)
    {
        yield return new WaitForSeconds(duration);// Wait a bit
        mySpeed = setSpeed;
        isBoosting = false;
        StartCoroutine(stopInvuln(1f));
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        string collisonName = collision.gameObject.name;
        if (collision.gameObject.tag == "water")
        {
            isOnWater = false;
        }
        else if (collision.gameObject.tag == "bridge")
        {
            isOnSafe = false;
        }
    }
    // private void OnCollisionStay2D(Collision2D collision) 
    // {
    //     if (collision.gameObject.tag == "bridge")
    //     {
    //         isOnSafe = true;
    //     }
    //     if (collision.gameObject.tag == "water")
    //     {
    //         isOnWater = true;
    //     }
    //     else if (collision.gameObject.tag == "cow")
    //     {
    //         if (!isInvuln && isAlive)
    //         {
    //             death();
    //         }

    //     }
    // }



    private void MoveTowardsTargetSpace()
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, targetSpace, mySpeed * Time.deltaTime);
    }
    private void SetTargetSpace()
    {
        if (isAlive)
        {
            Vector2 possibleTarget = transform.position;
            if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    //targetSpace += Vector2Int.up;
                    possibleTarget.y += 0.3f;
                    possibleTarget.y = Mathf.RoundToInt(possibleTarget.y);
                    possibleTarget.y += 0.7f;

                }

                else if (Input.GetKey(KeyCode.S))
                {
                    //targetSpace += Vector2Int.down;
                    possibleTarget.y += -0.7f;
                    possibleTarget.y = Mathf.RoundToInt(possibleTarget.y);
                    possibleTarget.y += -0.3f;
                    if (possibleTarget.y < -3.3f)
                    {
                        possibleTarget.y = -3.3f;
                        if (!hasTriedBack)
                        {
                            hasTriedBack = true;
                            achievManager.NotifyAchievement(1, "Try to go back");

                        }
                    }else if (possibleTarget.y < (futhestForwards - 4f))
                    {
                        possibleTarget.y = (futhestForwards - 4f);
                    }

                }
            }

            else if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    //targetSpace += Vector2Int.left;
                    possibleTarget.x += -0.5f;
                    possibleTarget.x = Mathf.RoundToInt(possibleTarget.x);
                    possibleTarget.x += -0.5f;
                    if (possibleTarget.x < -7.5f)
                    {
                        possibleTarget.x = -7.5f;
                    }
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    possibleTarget.x += 0.5f;
                    possibleTarget.x = Mathf.RoundToInt(possibleTarget.x);
                    possibleTarget.x += 0.5f;
                    if (possibleTarget.x > 7.5f)
                    {
                        possibleTarget.x = 7.5f;
                    }
                }
            }
            Collider2D wallCheck = Physics2D.OverlapCircle(possibleTarget, 0.3f, wallLayer);
            if(wallCheck == null){
                Debug.Log("Can move to: " + possibleTarget.x + "," + possibleTarget.y);
                targetSpace = possibleTarget;
            } else {
                Debug.Log("Cannot move to: " + possibleTarget.x + "," + possibleTarget.y);
            }
        }
    }
    public void resetLevel()
    {
        mySpeed = setSpeed;
        deathScreen.SetActive(false);
        myTransform.position = new Vector2(0.5f,-3.3f);
        targetSpace = new Vector2(0.5f, -3.3f);
        hasTriedBack = false;
        isOnWater = false;
        isOnSafe = false;
        deathCounter = 0;
        floatSpeed = 0;
        futhestForwards = 0.7f;
        isInvuln = false;
        isBoosting = false;
        isAlive = true;
    }
   
}
