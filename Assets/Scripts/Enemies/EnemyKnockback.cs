using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    private Rigidbody rb;
    public float knockbacktime = 0.5f;
    public float invtime = .7f;
    private bool invincible; // may move to controller...

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.tag == "weapon")
        {
            Vector3 moveDirection = rb.transform.position - coll.transform.position;
            StartCoroutine(knockback(moveDirection));
        }
    }

    IEnumerator knockback(Vector3 k)
    {

        invincible = true;
        float endTime = Time.time + knockbacktime;

        rb.AddForce(k.normalized * 650f);
        yield return new WaitForSeconds(knockbacktime);
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(invtime);

        invincible = false;
    }
}
