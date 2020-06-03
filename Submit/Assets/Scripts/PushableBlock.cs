using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    Rigidbody rb;
    //public Transform target;
    public bool CanMove = true;
    public float speed = 0.5f;
    public PushToUnlock door;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.localPosition.x >= 8 || rb.transform.localPosition.x < 7)
        {
            CanMove = false;
        }
    }

    private void OnCollisionStay(Collision coll)
    {
        GameObject object_collided_with = coll.gameObject;

        if (CanMove)
        {
            if (object_collided_with.tag == "Player")
            {
                door.blockPushed = true;
                float step = speed * Time.deltaTime;
                //rb.MovePosition(new Vector3(transform.position.x + 1, transform.position.y, 0));
                rb.transform.position = Vector3.MoveTowards(rb.transform.position, new Vector3(rb.transform.position.x + 1,
                    rb.transform.position.y, 0), step);
            }
        }

    }

    public void unlockDoor()
    {
        door.blockPushed = true;
    }
}
