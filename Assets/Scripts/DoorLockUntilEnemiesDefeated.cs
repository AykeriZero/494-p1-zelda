using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockUntilEnemiesDefeated : MonoBehaviour
{
    public GameObject lockedDoor;

    public int enemiesRequired;

    public int enemiesDefeated = 0;

    private bool doorUnlocked = false;

    public AudioClip door_unlock_sound;

    public GameObject transition_east;

    void Start()
    {
        if (transition_east)
        {
            TransitionCamera e = transition_east.GetComponent<TransitionCamera>();
            e.transitionUpLeftEnd += LockDoor;
        }
    }

    void LockDoor()
    {
        StartCoroutine(waitABit());
        lockedDoor.SetActive(true);
        AudioSource.PlayClipAtPoint(door_unlock_sound, Camera.main.transform.position);
    }

    IEnumerator waitABit()
    {
        yield return new WaitForSeconds(1.0f);
    }
    // Update is called once per frame
    void Update()
    {
        if (!doorUnlocked)
        {
            if (enemiesDefeated >= enemiesRequired)
            {
                AudioSource.PlayClipAtPoint(door_unlock_sound, Camera.main.transform.position);
                lockedDoor.SetActive(false);
                doorUnlocked = true;
            }
        }
        if (doorUnlocked)
        {
            lockedDoor.SetActive(false);
        }
    }
}
