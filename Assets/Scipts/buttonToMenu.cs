using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonToMenu : MonoBehaviour
{
    public GameObject player;
    public Button thisButton;
    public CameraFollowScript camera;
    private SceneSwapScript sceneChange;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollowScript>();
        Debug.Log("Player Found");
        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);
        sceneChange = GameObject.Find("SceneSwap").GetComponent<SceneSwapScript>();
    }

    public void OnClick()
    {
        Manager.saveGame();
        sceneChange.LeaveScene("mainMenu");


    }

}
