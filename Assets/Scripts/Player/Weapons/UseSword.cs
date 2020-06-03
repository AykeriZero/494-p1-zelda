using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSword : MonoBehaviour {
    public float sword_width = 0.07f;
    public float sword_length  = 0.2f;
    public float h_dist_from_center = 0.5f;
    public float v_dist_top = 0.5f;
    public float v_dist_bottom = 0.5f;
    public float sword_stab_time = 2;
    public AudioClip sword_beam_sound;
    public AudioClip sword_stab_sound;

    public float sword_beam_speed = 2;

    private Controller player_brain;
    private GameObject player_object;
    private GameObject sword;
    private GameObject sword_beam;

    Animator player_anim;

    private bool stabbing = false;
    public bool active_sword_beam = false;

    // Start is called before the first frame update
    void Start()
    {
        player_object = GameObject.Find("Player");
        player_brain = player_object.GetComponent<Controller>();
        sword = GameObject.Find("Sword");
        sword_beam = GameObject.Find("Sword Beam");
        player_anim= player_object.GetComponent<Animator>();
    }

    public void attack() {
        if (!stabbing) {
            StartCoroutine(stabSword());
        }
    }

    IEnumerator stabSword() {
        stabbing = true;

        AudioSource.PlayClipAtPoint(sword_stab_sound, Camera.main.transform.position);
        // stop player movement
        player_brain.allowed_to_move = false;

        // create sword w/ collision
        positionSword(sword);
        sword.SetActive(true);
        // animation
        player_anim.SetTrigger("stab");
        player_anim.speed = 1.0f;

        // sword beam
        if (player_brain.atFullHeath() && !active_sword_beam) {
            StartCoroutine(makeSwordBeam());
        }

        // wait
        yield return new WaitForSeconds(sword_stab_time);

        // start player movement and destroy sword
        player_brain.allowed_to_move = true;
        sword.SetActive(false);
        stabbing = false;
    }

    IEnumerator makeSwordBeam() {
        AudioSource.PlayClipAtPoint(sword_beam_sound, Camera.main.transform.position);
        active_sword_beam = true; // sword beam trigger turns this back to false
        sword_beam.SetActive(true);

        positionSword(sword_beam);
        Vector3 sword_beam_direction = getSwordBeamDirection();

        while (active_sword_beam) {
            sword_beam.transform.position = sword_beam.transform.position + sword_beam_direction * sword_beam_speed * Time.deltaTime;
            yield return null;
        }

        sword_beam.SetActive(false);
    }

    Vector3 getSwordBeamDirection() {
        switch (player_brain.facing_direction) {
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

    void positionSword(GameObject s) {
        switch (player_brain.facing_direction) {
            case Controller.Direction.Left:
                s.transform.localScale = new Vector3(sword_length, sword_width, 0);
                s.transform.position = player_object.transform.position - new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Right:
                s.transform.localScale = new Vector3(sword_length, sword_width, 0);
                s.transform.position = player_object.transform.position + new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Up:
                s.transform.localScale = new Vector3(sword_width, sword_length, 0);
                s.transform.position = player_object.transform.position + new Vector3(0, v_dist_top, 0);
                break;
            case Controller.Direction.Down:
                s.transform.localScale = new Vector3(sword_width, sword_length, 0);
                s.transform.position = player_object.transform.position - new Vector3(0, v_dist_bottom, 0);
                break;
            default:
                Debug.Log("Invalid Facing Direction");
                break;
        }
    }
}
