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
    public Slider HPSlider;
    private bool isInFov = false;

    private Transform objectRotation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (TargetObject != null && Vector3.Distance(transform.position, TargetObject.position) > maxRadius)
        {
            TargetObject = null;
        }

        if (TargetObject == null && GameObject.FindGameObjectsWithTag("interactable").Length > 0)
        {
            TargetObject = GameObject.FindGameObjectsWithTag("interactable")[0].transform;
            if (Vector3.Distance(TargetObject.position, this.transform.position) > maxRadius)
            {
                TargetObject = null;
            }
        }
        else
        {
            isInFov = inFov(transform, TargetObject, maxAngle, maxRadius);
        }

        if (Vector3.Distance(TargetObject.position, this.transform.position) > maxRadius)
        {
            TargetObject = null;
        }
    }

    private void OnDrawGizmos()
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
    } //Draw field of view for debugging purposes
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
