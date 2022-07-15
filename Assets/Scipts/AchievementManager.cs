using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    private Queue<achievement> achievementQueue = new Queue<achievement>();
    private Text achievementText;
    public GameObject achievementTextObject;
    public GameObject achievementBackground;
    public RectTransform textTransform;
    public RectTransform backgroundTransform;
    private Vector2 backgroundStart;
    private Vector2 textStart;
    public Vector2 backgroundTarget;
    public Vector2 textTarget;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AchievementQueue");
        achievementText = GameObject.FindWithTag("achievement").transform.GetChild(1).gameObject.GetComponent<Text>();
        achievementTextObject = GameObject.FindWithTag("achievement").transform.GetChild(1).gameObject;
        achievementBackground = GameObject.FindWithTag("achievement").transform.GetChild(0).gameObject;
        textStart = achievementTextObject.GetComponent<RectTransform>().anchoredPosition;
        backgroundStart = achievementBackground.GetComponent<RectTransform>().anchoredPosition;
        textTransform = achievementTextObject.GetComponent<RectTransform>();
        backgroundTransform = achievementBackground.GetComponent<RectTransform>();
        backgroundTarget = backgroundStart;
        textTarget = textStart;

    }
    void Update()
    {
        textTransform.anchoredPosition = Vector2.MoveTowards(textTransform.anchoredPosition, textTarget, 150f * Time.deltaTime);
        backgroundTransform.anchoredPosition = Vector2.MoveTowards(backgroundTransform.anchoredPosition, backgroundTarget, 150f * Time.deltaTime);
    }

    public void NotifyAchievement(int ID, string description)
    {
        achievementQueue.Enqueue(new achievement(ID, description));
    }
    private void UnlockAchievement(achievement unlock)
    {
        bool contained = false;
        foreach(achievement data in Manager.data.getAchievements())
        {
            if(data.ID == unlock.ID)
            {
                contained = true;
            }
        }
        if (!contained)
        {

            Manager.data.addAchievement(unlock);

            achievementText.text = "Achievement Get:\n" + unlock.description;
            StartCoroutine("moveUp");


        }
        
    }
    private IEnumerator moveUp()
    {
        backgroundTarget = new Vector2(backgroundStart.x, backgroundStart.y + 250);
        textTarget = new Vector2(textStart.x, textStart.y + 250);
        yield return new WaitForSeconds(3f);
        backgroundTarget = backgroundStart;
        textTarget = textStart;


    }
 
    private IEnumerator AchievementQueue()
    {
        for (; ; )
        {
            if(achievementQueue.Count > 0)
            {
                UnlockAchievement(achievementQueue.Dequeue());
            }
            yield return new WaitForSeconds(5f);
        }
    }
}
