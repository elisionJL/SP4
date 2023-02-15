using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float Spd = 12f;
    public bool Jump = false;

    public float GroundDistance = 0.0f;
    public Animator Player_Anim;

    // Start is called before the first frame update
    void Start()
    {
        GroundDistance = gameObject.GetComponent<CapsuleCollider>().radius;
    }

    public bool IsGrounded() // Check if player is close or touching a grounded area
    {
        Vector3 origin = transform.position;
        origin.y -= gameObject.GetComponent<CapsuleCollider>().radius; //Minus by size of radius to move origin down to bottom part of player

        Vector3 direction = -Vector3.up; //Direction that points down on the y axis to only check the ground
        Ray ray = new Ray(origin, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) //if sees ground
        {
            float distance = hit.distance; //Get the distance from the ray to the landable area

            Debug.DrawRay(origin, direction * distance, Color.red);

            if (distance <= (gameObject.GetComponent<CapsuleCollider>().radius + 0.1f)) //If distance from bottom of player to ground is close enough
            {
                return true; //Let Player jump
            }
            else
                return false;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Apply inputs to move
        Vector3 move = (transform.right * x + transform.forward * z) * Spd * Time.deltaTime; 

        //Change player position according to move
        gameObject.transform.position += move;
        if (x != 0 || z != 0)
            Player_Anim.SetBool("isWalking", true);
        else
            Player_Anim.SetBool("isWalking", false);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true) //Check if user can jump
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 250, 0)); //If yes, push player upwards
        }
    }
}
