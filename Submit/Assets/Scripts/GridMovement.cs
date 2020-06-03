using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {
    // Only gameobjects with rigitbodies should move.
    // Objects with only colliders are static.

    // -------------- MEMBER VARIABLES -------------
    private Controller brain;
    private Rigidbody rb;
    private Animator anim;

    private float speed = -1f;
    private float horizontal = 0;
    private float vertical = 0;

    // ------------ PUBLIC METHODS ----------------

    public void set_speed(float s) { speed = s; }

    public void move(float h, float v) {
        horizontal = h;
        vertical = v;

        // only allow movement in one direction
        if (horizontal != 0) {
            vertical = 0;
        }
        set_facing_direction();
    }

    // -------------- PRIVATE METHODS --------------

    void Start() {
        rb = GetComponent<Rigidbody>();
        brain = GetComponent<Controller>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if (speed == -1f) {
            Debug.Log("Please set the speed of GridMovement.");
            return;
        }

        if (brain.allowed_to_move == false) {
            return;
        }

        set_animator_data();
        move_rb();
    }

    private void move_rb() {

        rb.MovePosition(transform.position +
            new Vector3(horizontal, vertical, 0) * speed * Time.fixedDeltaTime);

        // if h == 0, snap h to grid and vice versa
        if (horizontal == 0) {
            float rounded = Mathf.Round(rb.position.x * 2) / 2;
            rb.position = new Vector3(rounded, rb.position.y, 0);
        }
        if (vertical == 0) {
            float rounded = Mathf.Round(rb.position.y * 2) / 2;
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
