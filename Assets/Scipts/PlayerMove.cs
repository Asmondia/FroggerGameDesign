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
    Animator animator;
    private AchievementManager achievManager;
    private bool hasTriedBack = false;

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
        if (isAlive)
        {
            animator.SetBool("isInvuln", isInvuln);
            animator.SetBool("isBoosting", isBoosting);
            var moving = Vector2.Distance((Vector2)myTransform.position, targetSpace) >= .2f;
            if (myTransform.position.y > futhestForwards)
            {
                futhestForwards = myTransform.position.y;
            }
            MoveTowardsTargetSpace();
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
                else
                {
                    deathCounter = 0;
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
                deathCounter = 0;
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

        if (collisonName == "TilemapHillsTop" || collisonName == "TilemapHillsBot")
        {

            targetSpace = previousSpace;
        }
        else if (collision.gameObject.tag == "water")
        {
            isOnWater = true;
        }
        else if (collision.gameObject.tag == "bridge")
        {
            isOnSafe = true;

            moveObject script = collision.gameObject.GetComponent<moveObject>();
            if (script.getLeft())
            {
                floatSpeed = -1f * script.getSpeed();
            }
            else
            {
                floatSpeed = 1f * script.getSpeed();
            }

        }
        else if (collision.gameObject.tag == "invulnPowerUp")
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
    private void OnCollisionStay2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "bridge")
        {
            isOnSafe = true;
        }
        if (collision.gameObject.tag == "water")
        {
            isOnWater = true;
        }
        else if (collision.gameObject.tag == "cow")
        {
            if (!isInvuln && isAlive)
            {
                death();
            }

        }
    }



    private void MoveTowardsTargetSpace()
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, targetSpace, mySpeed * Time.deltaTime);
    }
    private void SetTargetSpace()
    {
        if (isAlive)
        {
            if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.W))
                {
                    //targetSpace += Vector2Int.up;
                    targetSpace.y += 0.3f;
                    targetSpace.y = Mathf.RoundToInt(targetSpace.y);
                    targetSpace.y += 0.7f;

                }

                else if (Input.GetKey(KeyCode.S))
                {
                    //targetSpace += Vector2Int.down;
                    targetSpace.y += -0.7f;
                    targetSpace.y = Mathf.RoundToInt(targetSpace.y);
                    targetSpace.y += -0.3f;
                    if (targetSpace.y < -3.3f)
                    {
                        targetSpace.y = -3.3f;
                        if (!hasTriedBack)
                        {
                            hasTriedBack = true;
                            achievManager.NotifyAchievement(1, "Try to go back");

                        }
                    }else if (targetSpace.y < (futhestForwards - 4f))
                    {
                        targetSpace.y = (futhestForwards - 4f);
                    }

                }
            }

            else if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    //targetSpace += Vector2Int.left;
                    targetSpace.x += -0.5f;
                    targetSpace.x = Mathf.RoundToInt(targetSpace.x);
                    targetSpace.x += -0.5f;
                    if (targetSpace.x < -7.5f)
                    {
                        targetSpace.x = -7.5f;
                    }
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    targetSpace.x += 0.5f;
                    targetSpace.x = Mathf.RoundToInt(targetSpace.x);
                    targetSpace.x += 0.5f;
                    if (targetSpace.x > 7.5f)
                    {
                        targetSpace.x = 7.5f;
                    }
                }
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
