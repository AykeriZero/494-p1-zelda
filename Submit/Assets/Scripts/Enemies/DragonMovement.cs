using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement: MonoBehaviour
{
    public float directionChangeTime = 3f;
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

    }

    void Update()
    {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }


        if (c.allowed_to_move && throwBoomtime < Time.time && !boom_active) {
            StartCoroutine(throwBoomerang());
        }
    }

    IEnumerator throwBoomerang() {
        c.allowed_to_move = false;
        boom_active = true;

        boomerang.SetActive(true);
        positionBoom(boomerang);
        Vector3 td = throwDirection();

        while (boom_active) {
            boomerang.transform.position = boomerang.transform.position + td * Time.deltaTime * boom_speed;
            yield return null;
        }

        throwBoomtime = Time.time + .3f;
        c.allowed_to_move = true;
        boomerang.SetActive(false);
        boom_active = false;
    }

    Vector3 throwDirection() {
        return new Vector3(-1, 0, 0);
    }

    void positionBoom(GameObject s) {
        s.transform.position = transform.position - new Vector3(h_dist_from_center, 0, 0);
    }

    void calcuateNewMovementVector() {
        //choose a direction. 1 = up, 2 = right, 3 = down, 4 = left
        direction = (direction + 1) %  2;

        if (direction == 0) {
            horizontal = 1;
        }
        else {
            horizontal = -1;
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
