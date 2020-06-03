using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTakeDamage : MonoBehaviour {

    public float knockbacktime = 0.5f;
    public float invtime = .7f;

    //decrease health by 1 when player comes in contact with an enemy
    private Controller player;
    private Rigidbody rb;
    private SpriteRenderer sr;
    private Invincibility inv;

    private bool invincible; // may move to controller...

    void Start() {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Controller>();
        sr = GetComponent<SpriteRenderer>();
        inv = GetComponent<Invincibility>();

        player.onDeath += playerDies;
        invincible = false;
    }

    void playerDies() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "enemy" && !invincible && !inv.isGodMode()) {
            Vector3 moveDirection = rb.transform.position - coll.transform.position;
            player.decreaseHealth();
            StartCoroutine(knockback(moveDirection));
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "enemy" && !invincible && !inv.isGodMode()) {
            Vector3 moveDirection = rb.transform.position - coll.transform.position;
            player.decreaseHealth();
            StartCoroutine(knockback(moveDirection));
        }
    }

    IEnumerator knockback(Vector3 k) {
        player.stopInput();
        player.allowed_to_move = false;
        sr.color = new Color(1f, .7f, .7f, 1f);

        invincible = true;
        float endTime = Time.time + knockbacktime;

        rb.AddForce(k.normalized * 650f);
        yield return new WaitForSeconds(knockbacktime);
        rb.velocity = Vector3.zero;

        player.allowed_to_move = true;
        player.startInput();

        yield return new WaitForSeconds(invtime);

        sr.color = new Color(1f, 1f, 1f, 1f);
        invincible = false;
    }
}
