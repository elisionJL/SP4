using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowHostages : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Hostages Left: " + GlobalStuffs.Hostages;
    }
}
