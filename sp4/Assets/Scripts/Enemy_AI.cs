﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField]
    protected Transform Player;
    public float maxAngle;
    public float maxRadius;
    public int HP;
    public Slider HPSlider;
    private bool isInFov = false;

    private Transform objectRotation;

    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Player == null && GameObject.FindGameObjectsWithTag("interactable").Length > 0)
            Player = GameObject.FindGameObjectsWithTag("interactable")[0].transform;
        else
            isInFov = inFov(transform, Player, maxAngle, maxRadius);

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

        Gizmos.DrawRay(transform.position, (Player.position - transform.position).normalized * maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
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
                                return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    //return the quaternion from point of attack to target
    public bool GetQuaternionTarget(Transform checkingObject, float maxRadius)
    {
        Collider[] overlaps = new Collider[999];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count + 1; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == Player) //If Target enters field of view
                {
                    Vector3 directionBetween = (Player.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween); //Rotate to face target

                   // Vector3 TargetXZ = new Vector3(Player.position.x, checkingObject.position.y, Player.position.z);

                    checkingObject.LookAt(Player);

                    setRotation(checkingObject);
                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, Player.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, maxRadius)) //If raycast collides with target
                        {
                            if (hit.transform == Player)
                                return true;
                        }
                    }
                }
            }
        }
        return false;
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
