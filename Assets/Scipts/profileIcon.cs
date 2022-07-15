using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class profileIcon : MonoBehaviour
{
    [SerializeField] Sprite activeSprite;
    [SerializeField] Sprite inactiveSprite;
    [SerializeField] string profileName;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Manager.addProfileIcon(this);

    }
    public void setNotCurrent()
    {
        spriteRenderer.sprite = inactiveSprite;
    }
    public void setCurrent()
    {
        spriteRenderer.sprite = activeSprite;
    }

    public string getProfileName()
    {
        return this.profileName;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Manager.swapProfile(this);
            
        }
    }
}
