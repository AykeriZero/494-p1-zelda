using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemWhenEnemiesDefeated : MonoBehaviour
{
    public GameObject item;
    public int enemiesRequired;

    public int enemiesDefeated = 0;

    public AudioClip key_appears_sound;

    private bool itemAppeared = false;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!itemAppeared)
        {
            if (enemiesDefeated >= enemiesRequired)
            {
                if (item.tag == "key")
                {
                    AudioSource.PlayClipAtPoint(key_appears_sound, Camera.main.transform.position);
                }
                item.SetActive(true);
                itemAppeared = true;
            }
        }
    }
}
