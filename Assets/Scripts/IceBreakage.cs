using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreakage : MonoBehaviour
{
    public AudioClip ice_break_sound;
    public float totalHealth = 3f;

    public Sprite Health1_ice_block;
    public Sprite Health2_ice_block;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "weapon") {
            totalHealth -= 1;
            AudioSource.PlayClipAtPoint(ice_break_sound, Camera.main.transform.position);

            if (totalHealth == 0) {
                Destroy(gameObject);
            } else if (totalHealth == 1) {
                GetComponent<SpriteRenderer>().sprite = Health1_ice_block;
            } else if (totalHealth == 2) {
                GetComponent<SpriteRenderer>().sprite = Health2_ice_block;
            }
        }
    }
}
