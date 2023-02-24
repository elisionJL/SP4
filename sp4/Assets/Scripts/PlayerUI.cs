using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public GameObject Player;
    public Slider slider;
    public Slider slider2;
    public TMP_Text Soul;
    public TMP_Text SoulChange;
    public GameObject MiniMap;
    // Start is called before the first frame update
    void Start()
    {
        MiniMap.SetActive(false);
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Player.gameObject.transform.GetChild(0).GetComponent<Player>().Health;
        slider2.value = Player.gameObject.transform.GetChild(0).GetComponent<Player>().Mana;
        Soul.text = "$" + Player.gameObject.transform.GetChild(0).GetComponent<Player>().Souls;
        if (Input.GetKey(KeyCode.Tab))
        {
            MiniMap.SetActive(true);
        }
        else
        {
            MiniMap.SetActive(false);
        }
    }
}
