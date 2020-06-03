using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelMovement: MonoBehaviour
{
    public float directionChangeTime = 3f;
    public float speed = 4f;

    private GridMovement movement;
    private Controller c;

    private float latestDirectionChangeTime;

    private float horizontal;
    private float vertical;
    private float direction;
    private bool waiting;

    void Start()
    {
        movement = GetComponent<GridMovement>();
        c = GetComponent<Controller>();

        latestDirectionChangeTime = 0f;
        waiting = false;
        calcuateNewMovementVector();

        movement.set_speed(speed);
    }

    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
            if (waiting) {
                horizontal = vertical = 0;
            }

            waiting = !waiting;
        }

        //move enemy:
        movement.move(horizontal, vertical);
    }

    void calcuateNewMovementVector() {
        //choose a direction. 1 = up, 2 = right, 3 = down, 4 = left
        direction = (direction + Random.Range(1, 4)) % 4;

        if(direction == 0) {
            horizontal = 0;
            vertical = 1;
        }
        else if (direction == 1) {
            horizontal = 1;
            vertical = 0;
        }
        else if (direction == 2) {
            horizontal = 0;
            vertical = -1;
        }
        else if (direction == 3) {
            horizontal = -1;
            vertical = 0;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject object_collided_with = coll.gameObject;

        if (object_collided_with.tag != "Player")
        {

            calcuateNewMovementVector();

        }
    }
}
