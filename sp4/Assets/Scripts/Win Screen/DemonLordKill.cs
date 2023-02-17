using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DemonLordKill : MonoBehaviour
{
    public GameObject PlayerSword, PlayerCharacter, TellPlayer, TimeCount, MainCamera, King, YouWin;
    private bool Attack = false, Attack_Dir, TimeFinished, AttackTimeBool = false;
    private float AttackTime, TimeLeftToAttack = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Player_Attack();
    }

    private void Player_Attack()
    {
        if(TimeLeftToAttack > 0f)
        {
            if (Input.anyKeyDown && Attack == false)
            {
                PlayerSword.SetActive(true);
                PlayerSword.GetComponent<AttackScript>().enabled = true;
                Attack = true;
                AttackTimeBool = true;
                King.gameObject.GetComponent<Animation>().Play("resist");
            }

            if (AttackTimeBool == true)
            {
                if (TellPlayer.gameObject.activeSelf == true)
                {
                    TellPlayer.gameObject.SetActive(false);
                    TimeCount.gameObject.SetActive(true);
                }

                MainCamera.transform.position = new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(2.55f, 3.05f), MainCamera.transform.position.z);
                TimeLeftToAttack -= 1f * Time.deltaTime;
                TimeCount.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "" + Mathf.RoundToInt(TimeLeftToAttack);
            }

            if (Attack == true)
            {
                if (TimeCount.gameObject.activeSelf == true)
                {
                    if (Attack_Dir == false)
                        PlayerSword.transform.RotateAround(PlayerCharacter.transform.position, Vector3.up, -1000 * Time.deltaTime);
                    else if (Attack_Dir == true)
                        PlayerSword.transform.RotateAround(PlayerCharacter.transform.position, Vector3.up, 1000 * Time.deltaTime);

                    AttackTime += 1f * Time.deltaTime;

                    if (AttackTime >= 0.145f)
                    {
                        AttackTime = 0;
                        Attack = false;
                        PlayerSword.SetActive(false);

                        if (Attack_Dir == false)
                            Attack_Dir = true;
                        else if (Attack_Dir == true)
                            Attack_Dir = false;
                    }
                }
            }
        } 

        else //When Timer hits 0, turn king to skeleton
        {
            if (PlayerSword.gameObject.activeSelf == true)
                PlayerSword.gameObject.SetActive(false);
            if (PlayerCharacter.gameObject.activeSelf == true)
            {
                PlayerCharacter.SetActive(false);
                King.gameObject.GetComponent<Animation>().Play("die");
                YouWin.SetActive(true);
            }

            TimeCount.gameObject.SetActive(false);

            MainCamera.transform.position = new Vector3(-3.41f, 1.56f, -9.71f);
            MainCamera.transform.rotation = Quaternion.Euler(0, 90, 0);

        }
    }
}
