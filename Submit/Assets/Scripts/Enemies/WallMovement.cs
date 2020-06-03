using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement: MonoBehaviour
{
    public float horizontal_limit_lower;
    public float horizontal_limit_upper;
    public float vertical_limit_upper;
    public float vertical_limit_lower;
    public float directionChangeTime = 5f;
    public float speed = 4f;

    private GridMovement movement;
    private Controller c;
    private GameObject player;
    private Controller player_brain;

    public float horizontal;
    public float vertical;
    private float direction;

    static bool wall_caught_player;
    private bool player_caught;

    private Vector3 start_position;

    void Start()
    {
        movement = GetComponent<GridMovement>();
        c = GetComponent<Controller>();
        player = GameObject.Find("Player");
        player_brain = player.GetComponent<Controller>();

        start_position = transform.position;
        wall_caught_player = false;

        movement.set_speed(speed);
    }

    void Update()
    {
        movement.move(horizontal, vertical);

        if (transform.position.x < horizontal_limit_lower || transform.position.x > horizontal_limit_upper ||
            transform.position.y < vertical_limit_lower || transform.position.y > vertical_limit_upper) {
                transform.position = start_position;
                if (player_caught) {
                    player.transform.position = new Vector3(39.5f,0,0);
                    GameObject.Find("Main Camera").transform.position = new Vector3(39.48f, 6.92f, -10f);
                    player_caught = false;
                    wall_caught_player = false;
                    TransitionReset c = GameObject.Find("TransitionTriggers").GetComponent<TransitionReset>();
                    c.reset();
                    player_brain.startInput();
                }
            }
        if (player_caught) {
            player.transform.position = transform.position;
        }
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

    void OnTriggerEnter(Collider coll)
    {
        Invincibility inv = GameObject.Find("Player").GetComponent<Invincibility>();

        if (coll.gameObject.tag == "Player" && !wall_caught_player && !inv.isGodMode())
        {
            player_brain.stopInput();
            wall_caught_player = true;
            player_caught = true;
            player_brain.decreaseHealth();
        }
    }
}
