using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBow : MonoBehaviour
{
    private Controller player_c;
    private GameObject player_object;
    private Inventory inv;

    public GameObject arrow;

    public float speed = 8;

    public float arrow_length = 0.2f;
    public float arrow_width = 0.07f;

    public float h_dist_from_center = 0.5f;
    public float v_dist_top = 0.5f;
    public float v_dist_bottom = 0.5f;


    void Start() {
        player_object = GameObject.Find("Player");
        player_c = player_object.GetComponent<Controller>();
        inv = player_object.GetComponent<Inventory>();
    }

    public void attack() {
        if (inv.GetRupees() > 0) {
            inv.SetRupees(inv.GetRupees() - 1);
            StartCoroutine(fire_arrow());
        }
    }

    IEnumerator fire_arrow() {

        GameObject a = Instantiate(arrow, Vector3.zero, Quaternion.identity );
        positionArrow(a);


        ArrowHit ah = a.GetComponent<ArrowHit>();
        ah.active = true;
        Vector3 arrow_direction = getArrowDirection();

        while (ah.active) {
            a.transform.position = a.transform.position + arrow_direction * speed * Time.deltaTime;
            yield return null;
        }

        Destroy(a);
    }

    Vector3 getArrowDirection() {
        switch (player_c.facing_direction) {
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


    void positionArrow(GameObject s) {
        switch (player_c.facing_direction) {
            case Controller.Direction.Left:
                s.transform.localScale = new Vector3(arrow_length, arrow_width, 0);
                s.transform.position = player_object.transform.position - new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Right:
                s.transform.localScale = new Vector3(arrow_length, arrow_width, 0);
                s.transform.position = player_object.transform.position + new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Up:
                s.transform.localScale = new Vector3(arrow_width, arrow_length, 0);
                s.transform.position = player_object.transform.position + new Vector3(0, v_dist_top, 0);
                break;
            case Controller.Direction.Down:
                s.transform.localScale = new Vector3(arrow_width, arrow_length, 0);
                s.transform.position = player_object.transform.position - new Vector3(0, v_dist_bottom, 0);
                break;
            default:
                Debug.Log("Invalid Facing Direction");
                break;
        }
    }

}
