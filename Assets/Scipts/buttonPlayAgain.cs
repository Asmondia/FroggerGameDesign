using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPlayAgain : MonoBehaviour
{
    public GameObject player;
    public Button thisButton;
    public CameraFollowScript camera;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        camera = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollowScript>();

        thisButton = gameObject.GetComponent<Button>();
        thisButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Manager.saveGame();

        player.GetComponent<PlayerMove>().resetLevel();
        player.GetComponent<levelGenerate>().resetLevel();
        camera.resetCamera();

    }
}
