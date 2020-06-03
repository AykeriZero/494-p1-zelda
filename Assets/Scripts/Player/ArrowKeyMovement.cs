using System.Collections    ;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour {
    public float speed = 2f;

    private Controller brain;
    private GridMovement movement;

	void Start () {
        brain = GetComponent<Controller>();
        movement = GetComponent<GridMovement>();
        movement.set_speed(speed);
	}

	void Update () {
        if (!brain.allowInput()) { return; }

        float horizontal_input = Input.GetAxisRaw("Horizontal");
        float vertical_input = Input.GetAxisRaw("Vertical");

        movement.move(horizontal_input, vertical_input);
    }
}
