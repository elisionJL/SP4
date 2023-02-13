using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform PlayerBox;
    public GameObject ShopUI;
    float RotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MouseControls();
        SetMouseCursor();

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ShopUI.activeSelf == false)
                ShopUI.SetActive(true);
            else if (ShopUI.activeSelf == true)
                ShopUI.SetActive(false);
        }
    }

    public void SetMouseCursor()
    {
        if (ShopUI.activeSelf == true)
            Cursor.lockState = CursorLockMode.Confined;
        else if (ShopUI.activeSelf == false)
            Cursor.lockState = CursorLockMode.Locked;
    }
    private void MouseControls()
    {
        float MouseX = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * 100f * Time.deltaTime;

        RotationX -= MouseY;
        RotationX = Mathf.Clamp(RotationX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(RotationX, 0f, 0f);
        PlayerBox.Rotate(Vector3.up * MouseX);
    }
}
