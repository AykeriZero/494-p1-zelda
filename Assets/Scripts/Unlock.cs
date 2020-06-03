using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour
{
    public Inventory inventory;
    private Collider myCollider;
    public Sprite mySprite;

    public AudioClip door_unlock_sound;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
    }


    private void OnCollisionEnter(Collision coll)
    {
        GameObject object_collided_with = coll.gameObject;


        //if player touches door and has keys
        if (object_collided_with.tag == "Player" && inventory.GetKeys() != 0)
        {
            AudioSource.PlayClipAtPoint(door_unlock_sound, Camera.main.transform.position);
            StartCoroutine(pause());

            //decrease key count
            int newKeys = inventory.GetKeys() - 1;
            inventory.SetKeys(newKeys);

            //disable the door's box collider
            myCollider.enabled = !myCollider.enabled;

            //change the door's sprite to open
            this.GetComponent<SpriteRenderer>().sprite = mySprite;
        }
    }

    IEnumerator pause()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
