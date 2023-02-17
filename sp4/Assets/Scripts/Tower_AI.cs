using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tower_AI : MonoBehaviour
{
    public Transform playerTransform;
    public float maxAngle;
    public float maxRadius;
    public int HP;
    public GameObject Canvas;
    public Slider HPSlider;
    public TMP_Text TargetingText;
    private bool isInFov = false;
    public int countt;
    private Transform objectRotation;
    public Transform targetObject;
    public enum TARGETING
    {
        CLOSEST,
        STRONGEST,
        FIRST,
        RESET
    }
    public TARGETING targetingMode = TARGETING.CLOSEST;
    //private void OnEnable()
    //{
    //    Canvas = gameObject.transform.GetChild(2).gameObject;
    //}
    // Start is called before the first frame update
    void Start()
    {
        Canvas.SetActive(false);
        HP = 100;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Canvas.activeSelf)
            Canvas.transform.LookAt(Camera.main.transform.position);

        if (playerTransform == null && GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            playerTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
        else
            isInFov = inFov(transform, playerTransform, maxAngle, maxRadius);

        GL.PushMatrix();
        GL.Begin(GL.LINES);
        GL.Color(Color.yellow);
        GL.MultMatrix(transform.localToWorldMatrix);
        float a = 1 / (float)1;
        float angle = a * Mathf.PI * 2;
        // Vertex colors change from red to green
        GL.Color(new Color(a, 1 - a, 0, 0.8F));
        // One vertex at transform position
        GL.Vertex3(0, 0, 0);
        // Another vertex at edge of circle
        GL.Vertex3(Mathf.Cos(angle) * maxRadius, Mathf.Sin(angle) * maxRadius, 0);
        GL.End();
        GL.PopMatrix();
    }
 //Draw field of view for debugging purposes
    //back up
/*    public bool inFov(Transform checkingObject, Transform target, float maxAngle, float maxRadius) //Detection Range
    {
        Collider[] overlaps = new Collider[999];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target) //If Target enters field of view
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                    Vector3 TargetXZ = new Vector3(target.position.x, checkingObject.position.y, target.position.z);

                    checkingObject.LookAt(TargetXZ);

                    setRotation(checkingObject);
                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
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
    }*/
    public bool inFov(Transform checkingObject, Transform target, float maxAngle, float maxRadius) //Detection Range
    {
        Collider[] overlaps = new Collider[999];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);
        if (targetingMode == TARGETING.CLOSEST)
        {
            float nearestDistance = maxRadius;
            Transform nearestTarget = null;
            for (int i = 0; i < count + 1; i++)
            {
                if (overlaps[i] != null)
                {
                    if (overlaps[i].gameObject.tag == "Enemy") //If Target enters field of view
                    {
                        if (Vector3.Distance(overlaps[i].transform.position, checkingObject.position) < nearestDistance)
                        {
                            nearestDistance = Vector3.Distance(overlaps[i].transform.position, checkingObject.position);
                            nearestTarget = overlaps[i].transform;
                        }
                    }
                }
            }
            if (nearestTarget != null)
            {
                Vector3 directionBetween = (nearestTarget.position - checkingObject.position).normalized;
                directionBetween.y *= 0;

                float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                Vector3 TargetXZ = new Vector3(nearestTarget.position.x, checkingObject.position.y, nearestTarget.position.z);

                checkingObject.LookAt(TargetXZ);

                setRotation(checkingObject);
                if (angle <= maxAngle)
                {
                    Vector3 direction = nearestTarget.transform.position - checkingObject.position;
                    direction.y = 0;
                    Ray ray = new Ray(checkingObject.position, direction);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
                    {
                        if (hit.transform == nearestTarget.transform)
                        {
                            return true;
                        }
                    }
                }
            }
        }
        if (targetingMode == TARGETING.STRONGEST)
        {
            int highestHP = 0;
            Transform strongestTarget = null;
            for (int i = 0; i < countt + 1; i++)
            {
                if (overlaps[i] != null)
                {

                    if (overlaps[i].gameObject.tag == "Enemy") //If Target enters field of view
                    {                        
                        int enemyHP = overlaps[i].gameObject.GetComponent<Enemy_AI>().HP;
                        if (enemyHP > highestHP)
                        {
                            highestHP = enemyHP;
                            strongestTarget = overlaps[i].transform;
                        }
                    }
                }
            }
            if (strongestTarget != null)
            {
                Vector3 directionBetween = (strongestTarget.position - checkingObject.position).normalized;
                directionBetween.y *= 0;

                float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                Vector3 TargetXZ = new Vector3(strongestTarget.position.x, checkingObject.position.y, strongestTarget.position.z);

                checkingObject.LookAt(TargetXZ);

                setRotation(checkingObject);
                if (angle <= maxAngle)
                {
                    Ray ray = new Ray(checkingObject.position, strongestTarget.transform.position - checkingObject.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
                    {
                        if (hit.transform == strongestTarget.transform)
                        {
                            return hit.transform;
                        }
                    }
                }
            }
        }
        return false;
    }
    //get the target that the tower is aiming for
    public Transform GetQuaternionTarget(Transform checkingObject, float maxRadius)
    {
        Collider[] overlaps = new Collider[999];
        countt = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);
        if (targetingMode == TARGETING.CLOSEST)
        {
            float nearestDistance = maxRadius;
            Transform nearestTarget = null;
            for (int i = 0; i < countt + 1; i++)
            {
                if (overlaps[i] != null)
                {
                    if (overlaps[i].gameObject.tag == "Enemy") //If Target enters field of view
                    {
                        if (Vector3.Distance(overlaps[i].transform.position, checkingObject.position) < nearestDistance)
                        {
                            nearestDistance = Vector3.Distance(overlaps[i].transform.position, checkingObject.position);
                            nearestTarget = overlaps[i].transform;
                        }
                    }
                }
            }
            if (nearestTarget != null)
            {
                Vector3 directionBetween = (nearestTarget.position - checkingObject.position).normalized;
                directionBetween.y *= 0;

                float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                checkingObject.LookAt(nearestTarget);


                setRotation(checkingObject);
                if (angle <= maxAngle)
                {
                    Debug.Log("nearestTarget: " + nearestTarget);
                    Vector3 direction = nearestTarget.transform.position - checkingObject.position;
                    Ray ray = new Ray(checkingObject.position, direction);
                    RaycastHit hit;
                    Debug.Log("ray: " + Physics.Raycast(ray, out hit, maxRadius));
                    if (Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
                    {
                        //Debug.Log("rayHit: " + hit.transform);
                        if (hit.transform == nearestTarget.transform)
                        {
                            Debug.Log("returnTarget: " + hit.transform);
                            return hit.transform;
                        }
                    }
                }
            }
        }
        if (targetingMode == TARGETING.STRONGEST)
        {
            int highestHP = 0;
            Transform strongestTarget = null;
            for (int i = 0; i < countt + 1; i++)
            {
                if (overlaps[i] != null)
                {
                    if (overlaps[i].gameObject.tag == "Enemy") //If Target enters field of view
                    {
                        int enemyHP = overlaps[i].gameObject.GetComponent<Enemy_AI>().HP;
                        if (enemyHP > highestHP)
                        {
                            highestHP = enemyHP;
                            strongestTarget = overlaps[i].transform;
                        }
                    }
                }
            }
            if (strongestTarget != null)
            {
                Vector3 directionBetween = (strongestTarget.position - checkingObject.position).normalized;
                directionBetween.y *= 0;

                float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                checkingObject.LookAt(strongestTarget);


                setRotation(checkingObject);
                if (angle <= maxAngle)
                {
                    Ray ray = new Ray(checkingObject.position, strongestTarget.transform.position - checkingObject.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
                    {
                        if (hit.transform == strongestTarget.transform)
                        {
                            return hit.transform;
                        }
                    }
                }
            }
        }
        return null;
    }
    //backup
    /*public Transform GetQuaternionTarget(Transform checkingObject, float maxRadius)
    {
        Collider[] overlaps = new Collider[999];
        countt = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

            float nearestDistance = maxRadius;
            Transform nearestTarget;
        for (int i = 0; i < countt + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].gameObject.tag == "Enemy") //If Target enters field of view
                {
                    Vector3 directionBetween = (overlaps[i].transform.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                    checkingObject.LookAt(overlaps[i].transform);

                    setRotation(checkingObject);
                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, overlaps[i].transform.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
                        {
                            if (hit.transform == playerTransform)
                            {
                                return hit.transform;
                            }
                        }
                    }
                }
            }
        }        
        return null;
    }*/
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
    public Transform getPlayer()
    {
        return playerTransform;
    }
    private string getTargetingName()
    {
        switch (targetingMode)
        {
            case TARGETING.CLOSEST:
                return "CLOSEST";
            case TARGETING.STRONGEST:
                return "STRONGEST";
            case TARGETING.FIRST:
                return "FIRST";
            default:
                return null;
        }
    }
    public void UpdateTargeting()
    {
        targetingMode++;
        if (targetingMode == TARGETING.RESET)
        {
            targetingMode = TARGETING.CLOSEST;
        }
        TargetingText.text = "Targeting: " + getTargetingName();
    }
}
