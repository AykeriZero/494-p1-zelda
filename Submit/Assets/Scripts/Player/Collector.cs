using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {

    public AudioClip rupee_collection_sound_clip;

    Inventory inventory;
    private Controller player;
    public BombDisplayer bombDisplayer;
    UseItem useItem;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        player = GetComponent<Controller>();
        bombDisplayer = bombDisplayer.GetComponent<BombDisplayer>();
        useItem = GetComponent<UseItem>();

        if(inventory == null)
        {
            Debug.LogWarning("WARNING: Game object with a collector has no inventory to store things in!");
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        GameObject object_collided_with = coll.gameObject;

        if(object_collided_with.tag == "rupee")
        {
            if(inventory != null)
            {
                inventory.AddRupees(Random.Range(1,6));
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "key")
        {
            if (inventory != null)
            {
                inventory.AddKey(1);
            }
            Debug.Log("Collected key!");
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "heart")
        {
            if (player.getHealth() <= 4)
            {
                player.addHealth();
                player.addHealth();
            }
            if(player.getHealth() == 5)
            {
                player.addHealth();
            }
            Debug.Log("Collected heart!");
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "bomb")
        {
            if (inventory != null)
            {
                inventory.AddBombs(4);
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "bow")
        {
            if (inventory != null)
            {
                bombDisplayer.displayBow();
                inventory.GetBow();
                useItem.itemB = "bow";
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "boomerang")
        {
            if (inventory != null)
            {
                bombDisplayer.displayBoomerang();
                inventory.GetBoomer();
                useItem.itemB = "boomerang";
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }

        if (object_collided_with.tag == "snowflake")
        {
            if (inventory != null)
            {
                bombDisplayer.displayIce();
                inventory.GetIce();
                useItem.itemB = "ice";
            }
            Destroy(object_collided_with);

            AudioSource.PlayClipAtPoint(rupee_collection_sound_clip, Camera.main.transform.position);
        }
    }


}
