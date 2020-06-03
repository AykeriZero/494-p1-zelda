using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombDisplayer : MonoBehaviour
{
    public Inventory inventory;
    Text text_component;
    public Image bomb_image_component;
    public Image boomerang_image_component;
    public Image bow_image_component;
    public Image ice_image_component;

    public UseItem useItem;

    // Start is called before the first frame update
    void Start()
    {
        text_component = GetComponent<Text>();
        bomb_image_component = bomb_image_component.GetComponent<Image>();
        boomerang_image_component = boomerang_image_component.GetComponent<Image>();
        bow_image_component = bow_image_component.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && text_component != null)
        {
            text_component.text = "x" + inventory.GetBombs().ToString();
        }

        //check which itemB is active
        if(useItem.itemB == "boomerang")
        {
            undisplayBomb();
            undisplayIce();
            undisplayBow();
            displayBoomerang();
        }
        else if(useItem.itemB == "bomb" && inventory.GetBombs() > 0)
        {
            undisplayBow();
            undisplayBoomerang();
            undisplayIce();
            displayBomb();
        }
        else if(useItem.itemB == "bow"){
            undisplayBoomerang();
            undisplayBomb();
            undisplayIce();
            displayBow();
        }
        else if(useItem.itemB == "ice")
        {
            undisplayBomb();
            undisplayBoomerang();
            undisplayBow();
            displayIce();
        }
        else if (useItem.itemB == "")
        {
            undisplayBoomerang();
            undisplayBomb();
            undisplayBow();
            undisplayIce();
        }
    }

    public void displayBomb()
    {
        bomb_image_component.enabled = true;
    }

    public void undisplayBomb()
    {
        bomb_image_component.enabled = false;
    }

    public void displayBoomerang()
    {
        boomerang_image_component.enabled = true;
    }

    public void undisplayBoomerang()
    {
        boomerang_image_component.enabled = false;
    }

    public void displayBow()
    {
        bow_image_component.enabled = true;
    }

    public void undisplayBow()
    {
        bow_image_component.enabled = false;
    }

    public void displayIce()
    {
        ice_image_component.enabled = true;
    }

    public void undisplayIce()
    {
        ice_image_component.enabled = false;
    }
}
