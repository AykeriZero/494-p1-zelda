using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable_block : MonoBehaviour
{
    public float push_time = 1f;
    public float speed = .5f;
    private float end_time;
    private float start_y;
    public AudioClip push_block_sound;
    public PushToUnlock door;

    void OnCollisionEnter(Collision other) {
        end_time = Time.time + push_time;
        start_y = transform.position.y;
    }

    void OnCollisionStay(Collision other) {
        float v_input = Input.GetAxisRaw("Vertical");
        float h_input = Input.GetAxisRaw("Horizontal");
        if (h_input == 0 && v_input == 1 && Time.time > end_time && transform.position.y - start_y < 1) {
            transform.position = transform.position + new Vector3(0,1,0) * Time.deltaTime * speed;
            AudioSource.PlayClipAtPoint(push_block_sound, Camera.main.transform.position);
            if(door != null)
            {
                unlockDoor();
            }
        }
    }

    public void unlockDoor()
    {
        door.blockPushed = true;
    }
}
