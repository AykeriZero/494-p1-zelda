using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement: MonoBehaviour {
    // Only gameobjects with rigitbodies should move.
    // Objects with only colliders are static.

    // -------------- MEMBER VARIABLES -------------
    public float speed = 2f;
    public float changeTime = 2f;

    private Controller brain;
    private Rigidbody rb;
    private Animator anim;

    private float horizontal = 0;
    private float vertical = 0;

    private float Endtime;

    // -------------- PRIVATE METHODS --------------
    void new_bat_movement() {
        horizontal = Random.Range(-1, 2);
        vertical = Random.Range(-1, 2);
    }

    void OnCollisionStay() {
        new_bat_movement();
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        brain = GetComponent<Controller>();
        anim = GetComponent<Animator>();

        Endtime = Time.time + changeTime;
        new_bat_movement();
    }

    void FixedUpdate() {
        if (speed == -1f) {
            Debug.Log("Please set the speed of GridMovement.");
            return;
        }

        if (brain.allowed_to_move == false) {
            return;
        }

        if (Time.time >= Endtime) {
            Endtime = Time.time + changeTime;
            new_bat_movement();
        }

        set_animator_data();
        move_rb();
        set_facing_direction();
    }

    private void move_rb() {

        rb.MovePosition(transform.position +
            new Vector3(horizontal, vertical, 0) * speed * Time.fixedDeltaTime);

        // if h == 0, snap h to grid
        if (horizontal == 0 && vertical == 0) {
            float rounded = Mathf.Round(rb.position.x * 2) / 2;
            rb.position = new Vector3(rounded, rb.position.y, 0);

            rounded = Mathf.Round(rb.position.y * 2) / 2;
            rb.position = new Vector3(rb.position.x, rounded, 0);
        }
    }

    private void set_facing_direction() {
        if (horizontal < 0) {
            brain.facing_direction = Controller.Direction.Left;
        } else if (horizontal > 0) {
            brain.facing_direction = Controller.Direction.Right;
        } else if (vertical > 0) {
            brain.facing_direction = Controller.Direction.Up;
        } else if (vertical < 0) {
            brain.facing_direction = Controller.Direction.Down;
        }
    }

    private void set_animator_data() {

        if (horizontal == 0 && vertical == 0) {
            anim.speed = 0.0f;
        } else {
            anim.speed = 1.0f;
            anim.SetFloat("horizontal", horizontal);
            anim.SetFloat("vertical", vertical);
        }
    }
}
