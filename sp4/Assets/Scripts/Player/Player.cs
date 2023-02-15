using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform PlayerBox;
    public GameObject Crosshair;
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
        Souls = 100000;
    }

    // Update is called once per frame
    void Update()
    {
        if (Health > 0)
        {
            Health -= 1;
            Mana -= 1;
            //Souls += 10;
        }
        else
        {
            Health = 100;
            Mana = 100;
            //Souls -= 100;
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
            Souls -= SoulsNeeded;
            return true;
        }

        return false;
    }
}
