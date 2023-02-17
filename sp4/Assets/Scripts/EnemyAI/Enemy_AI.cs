using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_AI : MonoBehaviour
{
    public Transform TargetObject;
    public float maxAngle;
    public float maxRadius;
    public int HP;
    public int ArmorType = 0;
    public Slider HPSlider;
    private bool isInFov = false;
    public List<Transform> targets;
    public float CurrentPercentage;
    private Transform objectRotation;
    private bool GravityMagicUsed = false;
    private bool DamageDealt = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DisableScript()
    {
        GravityMagicUsed = true;
        if (gameObject.GetComponent<Priest>() != null)
            gameObject.GetComponent<Priest>().enabled = false;
        else if (gameObject.GetComponent<Knight>() != null)
            gameObject.GetComponent<Knight>().enabled = false;
        else if (gameObject.GetComponent<Heroine>() != null)
            gameObject.GetComponent<Heroine>().enabled = false;
        else if (gameObject.GetComponent<Villager>() != null)
            gameObject.GetComponent<Villager>().enabled = false;
    }
    public void EnableScript()
    {
        GravityMagicUsed = false;
        if (gameObject.GetComponent<Priest>() != null)
            gameObject.GetComponent<Priest>().enabled = true;
        else if (gameObject.GetComponent<Knight>() != null)
            gameObject.GetComponent<Knight>().enabled = true;
        else if (gameObject.GetComponent<Heroine>() != null)
            gameObject.GetComponent<Heroine>().enabled = true;
        else if (gameObject.GetComponent<Villager>() != null)
            gameObject.GetComponent<Villager>().enabled = true;

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().AddForce(0, -100, 0);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        DamageDealt = false;
    }


    public void GetWaypoints(GameObject WaypointGO)
    {
        targets.Clear();
        foreach (Transform child in WaypointGO.transform)
        {
            targets.Add(child.transform);
        }
    }

    public List<Transform> ReturnWaypoints()
    {
        return targets;
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsGrounded() && !GravityMagicUsed && !DamageDealt)
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            Debug.Log("HitGround");
            DamageDealt = true;
            MinusHP(30);
        }

        Collider[] overlaps = new Collider[999];
        int count = Physics.OverlapSphereNonAlloc(this.transform.position, maxRadius, overlaps);
        if (TargetObject == null && count > 0)
        {
            for (int i = 0; i < count + 1; i++)
            {
                if (overlaps[i] != null)
                {
                    if (overlaps[i].tag == "interactable") //If Target enters field of view
                    {
                        if (Vector3.Distance(overlaps[i].transform.position, transform.position) < maxRadius)
                        {
                            TargetObject = overlaps[i].transform;
                        }
                    }
                }
            }
        }
        else
        {
            isInFov = inFov(transform, TargetObject, maxAngle, maxRadius);
        }
    }

/*    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if(!isInFov)
        {
            Gizmos.color = Color.red;
        }

        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawRay(transform.position, (TargetObject.position - transform.position).normalized * maxRadius);
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward * maxRadius);
    }*/ //Draw field of view for debugging purposes
    public bool inFov (Transform checkingObject, Transform target, float maxAngle, float maxRadius) //Detection Range
    {
        Collider[] overlaps = new Collider[999];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if(overlaps[i] != null)
            {
                if(overlaps[i].transform == target) //If Target enters field of view
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                    Vector3 TargetXZ = new Vector3(target.position.x, checkingObject.position.y, target.position.z);

                    checkingObject.LookAt(TargetXZ);

                    setRotation(checkingObject);
                    if(angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if(Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
                        {
                            if (hit.transform == target)
                            {
                                return true;
                            }    
                        }
                    }
                }
            }
        }
        return false;
    }
    //return the quaternion from point of attack to target
    public Transform GetQuaternionTarget(Transform checkingObject, float maxRadius)
    {
        Collider[] overlaps = new Collider[999];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == TargetObject) //If Target enters field of view
                {
                    Vector3 directionBetween = (TargetObject.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                    // Vector3 TargetXZ = new Vector3(TargetObject.position.x, checkingObject.position.y, TargetObject.position.z);

                    checkingObject.LookAt(TargetObject);

                    setRotation(checkingObject);
                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position + new Vector3(0, 0.5f, 0), TargetObject.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
                        {
                            if (hit.transform == TargetObject)
                                return hit.transform;
                        }
                    }
                }
            }
        }
        return null;
    }
    public void setRotation(Transform currRotate)
    {
        objectRotation = currRotate;
    }

    public bool IsGrounded() // Check if player is close or touching a grounded area
    {
        Vector3 origin = transform.position;
        origin.y -= 0; //Minus by size of radius to move origin down to bottom part of player


        Vector3 direction = -Vector3.up; //Direction that points down on the y axis to only check the ground
        Ray ray = new Ray(origin, direction);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) //if sees ground
        {
            float distance = hit.distance; //Get the distance from the ray to the landable area

            Debug.DrawRay(origin, direction * distance, Color.red);

            if (distance <= (0.1f)) //If distance from bottom of player to ground is close enough
            {
                return true; //Let Player jump
            }
            else
            {
                Debug.Log("Jumping");
                return false;
            }
        }

        return false;
    }

    public Transform getRotation()
    {
        return objectRotation;
    }

    public void MinusHP(int MinusBy)
    {
        HP -= MinusBy;
        HPSlider.value = HP;
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
