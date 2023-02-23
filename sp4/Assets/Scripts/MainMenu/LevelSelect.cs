using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    public Transform Locks;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GlobalStuffs.level);
        Cursor.lockState = CursorLockMode.Confined;
        for (int i = 0; i < GlobalStuffs.level + 1;++i)
        {
            Locks.GetChild(i).gameObject.SetActive(false);
        }
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
