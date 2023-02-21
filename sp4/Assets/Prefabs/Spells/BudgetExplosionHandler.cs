using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudgetExplosionHandler : MonoBehaviour
{
    private bool changeview;
    private Vector3 startPosition;
    private float shakeDuration = 1;
    public AnimationCurve Curve;
    // Start is called before the first frame update
    void Start()
    {
        //changeview = false;
        startPosition = Camera.main.transform.position;
        StartCoroutine(ShakeCamera());
    }

    // Update is called once per frame
    void Update()
    {
        if (changeview)
        {
            //StartCoroutine(ShakeCamera());
        }
        //    Camera.main.transform.position = startPosition + Random.insideUnitSphere;
            
        //}
        //else
        //{
        //    Camera.main.transform.position = startPosition;
        //    changeview = true;
        //}

        if (transform.localScale.x < 150)
        {
            Vector3 CurrScale = transform.localScale;
            transform.localScale = new Vector3(CurrScale.x + 0.3f, CurrScale.y + 0.3f, CurrScale.z + 0.3f);
            transform.Rotate(0, 500 * Time.deltaTime, 0);
        }
        else
        {
            Camera.main.transform.position = startPosition;
            Destroy(gameObject);
        }
    }
    IEnumerator ShakeCamera()
    {
        float elapsedTime = 0;
        Debug.Log("shake1");
        Debug.Log(elapsedTime + " : " + shakeDuration);
        while (elapsedTime < shakeDuration)
        {
            Debug.Log("shake2");
            elapsedTime += Time.deltaTime;
            float strength = Curve.Evaluate(elapsedTime / shakeDuration);
            Camera.main.transform.position = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        //reset to start position
        Camera.main.transform.position = startPosition;
        changeview = false;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy_AI>().MinusHP(50);
        }
    }
}
