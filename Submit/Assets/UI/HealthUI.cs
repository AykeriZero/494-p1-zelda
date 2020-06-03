using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

    private Controller player;
    private Sprite currentHealth;

    public Sprite health6;
    public Sprite health5;
    public Sprite health4;
    public Sprite health3;
    public Sprite health2;
    public Sprite health1;
    public Sprite health0;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = GetComponent<Image>().sprite;
        player = GameObject.Find("Player").GetComponent<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getHealth() == 6)
        {
            this.GetComponent<Image>().sprite = health6;
        }
        if (player.getHealth() == 5)
        {
            this.GetComponent<Image>().sprite = health5;
        }
        if (player.getHealth() == 4)
        {
            this.GetComponent<Image>().sprite = health4;
        }
        if (player.getHealth() == 3)
        {
            this.GetComponent<Image>().sprite = health3;
        }
        if (player.getHealth() == 2)
        {
            this.GetComponent<Image>().sprite = health2;
        }
        if (player.getHealth() == 1)
        {
            this.GetComponent<Image>().sprite = health1;

        }
        if (player.getHealth() == 0)
        {
            this.GetComponent<Image>().sprite = health0;
        }
    }
}
