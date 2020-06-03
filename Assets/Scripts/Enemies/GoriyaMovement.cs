using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoriyaMovement: MonoBehaviour
{
    public readonly float directionChangeTime = 3f;
    public float speed = 1.5f;
    public float boom_speed = 8f;

    public float h_dist_from_center = 0.5f;
    public float v_dist_top = 0.5f;
    public float v_dist_bottom = 0.5f;

    private GridMovement movement;
    private Controller c;
    private GameObject boomerang;

    private float latestDirectionChangeTime;

    private float horizontal;
    private float vertical;
    private float direction;

    private float throwBoomtime;
    public bool boom_active;
    public bool rebounding;

    void Start()
    {
        c = GetComponent<Controller>();
        movement = GetComponent<GridMovement>();
        boomerang = transform.GetChild(0).gameObject;
        boomerang.SetActive(false);
        throwBoomtime = 0;
        boom_active = false;

        latestDirectionChangeTime = 0f;
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
        }

        //move enemy:
        movement.move(horizontal, vertical);

        if (c.allowed_to_move && throwBoomtime < Time.time && !boom_active) {
            StartCoroutine(throwBoomerang());
        }
    }

    IEnumerator throwBoomerang() {
        c.allowed_to_move = false;
        boom_active = true;
        rebounding = false;

        boomerang.SetActive(true);
        positionBoom(boomerang);
        Vector3 td = throwDirection();

        while (!rebounding) {
            boomerang.transform.position = boomerang.transform.position + td * Time.deltaTime * boom_speed;
            yield return null;
        }

        while (boom_active) {
            boomerang.transform.position = boomerang.transform.position - td * Time.deltaTime * boom_speed;
            yield return null;
        }

        throwBoomtime = Time.time + Random.Range(3, 7);
        c.allowed_to_move = true;
        boomerang.SetActive(false);
        boom_active = false;
    }

    Vector3 throwDirection() {
        switch (c.facing_direction) {
            case Controller.Direction.Left:
                return new Vector3(-1, 0, 0);
            case Controller.Direction.Right:
                return new Vector3(1, 0, 0);
            case Controller.Direction.Up:
                return new Vector3(0, 1, 0);
            case Controller.Direction.Down:
                return new Vector3(0, -1, 0);
            default:
                Debug.Log("Invalid Direction");
                return Vector3.zero;
        }
    }

    void positionBoom(GameObject s) {
        switch (c.facing_direction) {
            case Controller.Direction.Left:
                s.transform.position = transform.position - new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Right:
                s.transform.position = transform.position + new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Up:
                s.transform.position = transform.position + new Vector3(0, v_dist_top, 0);
                break;
            case Controller.Direction.Down:
                s.transform.position = transform.position - new Vector3(0, v_dist_bottom, 0);
                break;
            default:
                Debug.Log("Invalid Facing Direction");
                break;
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

    private void OnCollisionStay(Collision coll)
    {
        GameObject object_collided_with = coll.gameObject;

        if (object_collided_with.tag != "Player")
        {
            calcuateNewMovementVector();
        }
    }
}
