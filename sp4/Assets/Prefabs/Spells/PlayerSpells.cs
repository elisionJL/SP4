using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.PyroParticles;

public class PlayerSpells : MonoBehaviour
{
    #region Player Magic
    public GameObject PlayerCharacter;
    public List<GameObject> ListOfEnemies;
    private bool AntiGravity = false;
    private float ElapsedVariableForMagic;
    private bool UsingMagic = false;
    public GameObject FireBalls;
    public GameObject Explosion;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L) && !UsingMagic && PlayerCharacter.gameObject.transform.parent.GetChild(0).GetComponent<Player>().Mana >= 10)
        {
            ListOfEnemies.Clear();
            ListOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

            for (int i = 0; i < ListOfEnemies.Count; ++i)
            {
                ListOfEnemies[i].GetComponent<Enemy_AI>().DisableScript();
            }
            AntiGravity = true;
            UsingMagic = true;
            PlayerCharacter.gameObject.transform.parent.GetChild(0).GetComponent<Player>().Mana -= 10;
        }
        if (AntiGravity)
        {
            if (ElapsedVariableForMagic < 1)
            {
                ElapsedVariableForMagic += Time.deltaTime;
                for (int i = 0; i < ListOfEnemies.Count; ++i)
                {
                    ListOfEnemies[i].transform.position = new Vector3(ListOfEnemies[i].transform.position.x, ListOfEnemies[i].transform.position.y + (Time.deltaTime * 2), ListOfEnemies[i].transform.position.z);
                }
            }
            else
            {
                for (int i = 0; i < ListOfEnemies.Count; ++i)
                {
                    ListOfEnemies[i].GetComponent<Enemy_AI>().EnableScript(Explosion);
                }
                UsingMagic = false;
                AntiGravity = false;
                ElapsedVariableForMagic = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            FireBalls.GetComponent<MeteorSwarmScript>().FindPlayer(gameObject);
            Instantiate(FireBalls);
        }
    }
}
