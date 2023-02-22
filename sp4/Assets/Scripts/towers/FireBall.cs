using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject explosionPrefab;
    public void Explode(Transform pos)
    {
        Instantiate(explosionPrefab, pos.position, Quaternion.identity);
    }
}
