using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkins : MonoBehaviour
{
    public GameObject Player;
    public Material Text1;
    public Material Text2;
    public Material Text3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ChangeSkinToRed()
    {
        Player.transform.GetComponent<SkinnedMeshRenderer>().material = Text1;
    }
    public void ChangeSkinToBlack()
    {
        Player.transform.GetComponent<SkinnedMeshRenderer>().material = Text2;
    }
    public void ChangeSkinToGalaxy()
    {
        Player.transform.GetComponent<SkinnedMeshRenderer>().material = Text3;
    }
}
