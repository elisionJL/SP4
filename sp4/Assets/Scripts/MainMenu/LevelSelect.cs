using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void retrunToMainMenu()
    {
        SceneManager.LoadScene("TowerSelectScene");
    }
}
