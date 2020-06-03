using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBomb : MonoBehaviour
{
    public float h_dist_from_center = 1.5f;
    public float v_dist_top = 1.5f;
    public float v_dist_bottom = 1.5f;
    public Sprite explosion_sprite;
    public Sprite bomb_sprite;

    public Inventory inventory;
    public BombDisplayer bombDisplayer;

    private Controller player_brain;
    private GameObject player_object;
    private GameObject bombWeapon;
    private SpriteRenderer sr;
    private BoxCollider bc;

    Animator player_anim;

    private bool bombing = false;

    // Start is called before the first frame update
    void Start()
    {
        player_object = GameObject.Find("Player");
        player_brain = player_object.GetComponent<Controller>();
        player_anim = player_object.GetComponent<Animator>();
        bombWeapon = GameObject.Find("Bomb Weapon");
        bombDisplayer = bombDisplayer.GetComponent<BombDisplayer>();
        sr = bombWeapon.GetComponent<SpriteRenderer>();
        bc = bombWeapon.GetComponent<BoxCollider>();
    }


    public void attack(){
        if (!bombing && inventory.GetBombs() >= 1){
            StartCoroutine(throwBomb());
            StartCoroutine(explodeBomb());
        }
    }

    IEnumerator throwBomb()
    {
        bombing = true;

        //decrement bombs in inventory
        inventory.SetBombs(inventory.GetBombs() - 1);

        //remove bomb from display UI if out of bombs
        //remove bomb from itemB if out of bombs
        if(inventory.GetBombs() == 0)
        {
            bombDisplayer.displayBomb();
            player_object.GetComponent<UseItem>().itemB = "";
        }


        // stop player movement
        //player_brain.allowed_to_move = false;

        // create bomb w/ collision
        positionBomb(bombWeapon);
        bombWeapon.SetActive(true);


        // wait
        yield return new WaitForSeconds(0.5f);

        //player_brain.allowed_to_move = true;
        bombing = false;
    }

    void positionBomb(GameObject s)
    {
        switch (player_brain.facing_direction)
        {
            case Controller.Direction.Left:
                s.transform.position = player_object.transform.position - new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Right:
                s.transform.position = player_object.transform.position + new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Up:
                s.transform.position = player_object.transform.position + new Vector3(0, v_dist_top, 0);
                break;
            case Controller.Direction.Down:
                s.transform.position = player_object.transform.position - new Vector3(0, v_dist_bottom, 0);
                break;
            default:
                Debug.Log("Invalid Facing Direction");
                break;
        }
    }

    IEnumerator explodeBomb()
    {
        Debug.Log("EXPLOOSION");

        //flash colors
        sr.color = new Color(0f, 0f, 0f, 1f); 
        yield return new WaitForSeconds(.2f);
        sr.color = new Color(1f, 1f, 1f, 1f);   //normal
        yield return new WaitForSeconds(.2f);
        sr.color = new Color(1f, .7f, .7f, 1f); 
        yield return new WaitForSeconds(.2f);
        sr.color = new Color(1f, 1f, 1f, 1f);  //normal
        yield return new WaitForSeconds(.2f);
        sr.color = new Color(1f, .7f, .7f, 1f); 
        yield return new WaitForSeconds(.2f);

        //activate box colider and change sprite
        bc.enabled = true;
        sr.sprite = explosion_sprite;

        yield return new WaitForSeconds(1.0f);

        //move offscreen and reset sprite
        bombWeapon.transform.position = new Vector3(0, 0, 0);
        sr.sprite = bomb_sprite;
        bc.enabled = false;
    }
}
