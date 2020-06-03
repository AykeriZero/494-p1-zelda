using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public string itemA = "sword";
    public string itemB = "";

    private string[] secondary_items = new string[] {"boomerang", "bow", "bomb", "ice"};
    private int secondary_ind = 0;

    private UseSword sword;
    private UseBow bow;
    private UseBoomerang boomerang;
    private UseBomb bomb;
    private UseIce ice;
    private Controller brain;

    Inventory inventory;

    // Start is called before the first frame update
    void Start() {
        brain = GetComponent<Controller>();
        GameObject weapons = GameObject.Find("Weapons");
        sword = weapons.GetComponent<UseSword> ();
        bow = weapons.GetComponent<UseBow> ();
        boomerang = weapons.GetComponent<UseBoomerang> ();
        bomb = weapons.GetComponent<UseBomb>();
        ice = weapons.GetComponent<UseIce>();
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update() {
        if (!brain.allowInput()) { return; }

        if (Input.GetKeyDown(KeyCode.X)) {
            useItem(itemA);
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            useItem(itemB);
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            Debug.Log(itemB);
            //don't switch weapon if boomerang, bow, bomb not obtained
            if(inventory.GetBombs() <= 0 && !inventory.hasBoomer && !inventory.hasBow && !inventory.hasIce)
            {
                itemB = "";
                return;
            }

            //switch weapon
            secondary_ind = (secondary_ind + 1) % secondary_items.Length;
            itemB = secondary_items[secondary_ind];

            //don't switch to bomb if no bombs obtained yet
            if(itemB == "bomb" && inventory.GetBombs() <= 0)
            {
                secondary_ind = (secondary_ind + 1) % secondary_items.Length;
                itemB = secondary_items[secondary_ind];
            }
            //don't switch to boomerang if not obtained
            if (itemB == "boomerang" && inventory.hasBoomer == false)
            {
                secondary_ind = (secondary_ind + 1) % secondary_items.Length;
                itemB = secondary_items[secondary_ind];
            }
            //don't switch to bow if not obtained
            if (itemB == "bow" && inventory.hasBow == false)
            {
                secondary_ind = (secondary_ind + 1) % secondary_items.Length;
                itemB = secondary_items[secondary_ind];
            }
            //don't switch to ice if not obtained
            if (itemB == "ice" && inventory.hasIce == false)
            {
                secondary_ind = (secondary_ind + 1) % secondary_items.Length;
                itemB = secondary_items[secondary_ind];
            }
        }

    }

    void useItem(string item) {
        switch(item) {
            case "sword":
                sword.attack();
                break;
            case "bow":
                bow.attack();
                break;
            case "boomerang":
                boomerang.attack();
                break;
            case "bomb":
                bomb.attack();
                break;
            case "ice":
                ice.attack();
                break;
            default:
                Debug.Log("No item equipped. " + item);
                break;
        }
    }
}
