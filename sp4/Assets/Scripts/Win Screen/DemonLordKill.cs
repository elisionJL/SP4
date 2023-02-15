using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonLordKill : MonoBehaviour
{
    public GameObject PlayerSword, PlayerCharacter;
    private bool Attack = false, Attack_Dir;
    private float AttackTime;
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
        if (Input.anyKeyDown && Attack == false)
        {
            PlayerSword.SetActive(true);
            PlayerSword.GetComponent<AttackScript>().enabled = true;
            Attack = true;
        }

        if (Attack == true)
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
