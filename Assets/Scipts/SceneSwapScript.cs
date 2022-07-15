using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator transition;
    public float transitionTime = 3f;
    public void Awake()
    {
        transition = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void LeaveScene(string sceneName)
    {

        Manager.profileIcons = new List<profileIcon>();
        StartCoroutine(LoadLevel(sceneName));
    }
    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }
}
