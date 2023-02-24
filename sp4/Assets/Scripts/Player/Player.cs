using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
public class Player : MonoBehaviour
{
    public Transform PlayerBox;
    public GameObject Crosshair;
    public PostProcessLayer CameraPPL;
    public MoveScript PlayerMoveScript;
    public Base_Interaction PlayerBaseInteraction;
    public Tower_Shop PlayerTowerShop;
    public GameObject RespawnText;
    public GameObject PlayerModel;
    public GameObject PausePanel;
    public PlayerUI Canvas;
    private float timerbeforecolorreturns;
    private float RespawnCount;
    float RotationX = 0f;
    public int Health = 0;
    public int Mana = 0;
    public int Souls = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Health = 100;
        Mana = 100;
        Souls = 1000;
        RespawnText.SetActive(false);
        CameraPPL.enabled = false;
        RespawnCount = 0;
        PausePanel.SetActive(false);
        Canvas = GameObject.Find("Canvas").GetComponent<PlayerUI>();
        timerbeforecolorreturns = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (timerbeforecolorreturns > 0)
        {
            timerbeforecolorreturns -= Time.deltaTime;
            if (timerbeforecolorreturns <= 0)
            {
                Canvas.SoulChange.enabled = false;
                Canvas.Soul.color = Color.white;
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                PausePanel.SetActive(true);
                UnlockMouse();
            }
            else
            {
                Time.timeScale = 1;
                PausePanel.SetActive(false);
                LockMouse();
            }
        }
        //check if dead
        if (Health < 0 && RespawnCount <= 0)
        {
            gameObject.GetComponent<Base_Interaction>().DisableSword();
            PlayerModel.SetActive(false);
            RespawnText.SetActive(true);
            Crosshair.SetActive(false);
            PlayerMoveScript.enabled = false;
            PlayerBaseInteraction.enabled = false;
            PlayerTowerShop.enabled = false;
            CameraPPL.enabled = true;
            RespawnCount = 5;
            RespawnText.GetComponent<TMP_Text>().text = Mathf.Ceil(RespawnCount).ToString();
            Health = 0;

        }
        else if(RespawnCount > 0)
        {
            RespawnCount -= Time.deltaTime;
            RespawnText.GetComponent<TMP_Text>().text = Mathf.Ceil(RespawnCount).ToString();
        }
        else if (Health == 0)
        {
            RespawnText.SetActive(false);
            PlayerModel.SetActive(true);
            Crosshair.SetActive(true);
            PlayerMoveScript.enabled = true;
            CameraPPL.enabled = false;
            PlayerBaseInteraction.enabled = true;
            PlayerTowerShop.enabled = true;
            RespawnCount = 0;
            Health = 100;
            transform.parent.transform.position = new Vector3(0, 10, 0);
        }
        MouseControls();
    }
    public void LockMouse() //Sets cursor to locked when in play mode, unlocked when in shop
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockMouse() //Sets cursor to locked when in play mode, unlocked when in shop
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void MouseControls()
    {
        if(Cursor.lockState == CursorLockMode.Locked) //Only when cursor mode is locked to center
        {
            //Get the X and Y movement
            float MouseX = Input.GetAxis("Mouse X") * 300f * Time.deltaTime;
            float MouseY = Input.GetAxis("Mouse Y") * 300f * Time.deltaTime;

            //Get the Rotation
            RotationX -= MouseY;
            RotationX = Mathf.Clamp(RotationX, -90f, 90f);

            //Set local rotation of camera
            transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
            PlayerBox.Rotate(Vector3.up * MouseX);
        }
    }

    public bool MinusSouls(int SoulsNeeded)
    {
        if(SoulsNeeded <= Souls)
        {
            Canvas.SoulChange.enabled = true;
            if (SoulsNeeded < 0)
            {
                Canvas.Soul.color = Color.green;
                Canvas.SoulChange.color = Color.green;
                Canvas.SoulChange.text = "+"+(-SoulsNeeded).ToString();
                timerbeforecolorreturns = 0.5f;
            }
            else
            {
                Canvas.Soul.color = Color.red;
                Canvas.SoulChange.color = Color.red;
                Canvas.SoulChange.text = (-SoulsNeeded).ToString();
                timerbeforecolorreturns = 0.5f;
            }
            Souls -= SoulsNeeded;
            return true;
        }

        return false;
    }
    public void MinusHP(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
            Health = -1;
    }
}
