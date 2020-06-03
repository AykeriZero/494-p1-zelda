using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushToUnlock : MonoBehaviour
{
    private Collider myCollider;
    public bool blockPushed = false;
    bool unlocked = false;
    public Sprite mySprite;


    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (blockPushed)
        {
            //disable the door's box collider
            if (!unlocked)
            {
                myCollider.enabled = !myCollider.enabled;
                unlocked = true;
            }


            //change the door's sprite to open
            this.GetComponent<SpriteRenderer>().sprite = mySprite;
        }
    }

}
