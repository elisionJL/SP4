using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.PyroParticles;

public class PlayerSpells : MonoBehaviour
{
    #region Player Magic
    public GameObject PlayerCharacter;
    public List<GameObject> ListOfEnemies;
    private bool AntiGravity = false;
    private float ElapsedVariableForMagic;
    private bool UsingMagic = false;
    public GameObject FireBalls;
    public GameObject Explosion;
    public GameObject MExplosion;
    public Light LightOfScene;
    public bool BigUlt = false;
    public bool CreatedDomain = false;
    public bool SpawnedSword;
    public GameObject Universe;
    public GameObject ClonedUniverse;
    public GameObject PortalToComeOutOf;
    public GameObject PortalToDelete;
    public GameObject SwordToLookAt;
    public GameObject SwordSpawn;
    private float countup;
    #endregion Player Magic

    #region cameraShaking
    //for camera shakes
    private Vector3 startPosition;
    private float shakeDuration = 3;
    public AnimationCurve Curve;
    #endregion cameraShaking
    // Start is called before the first frame update
    void Start()
    {
        startPosition = Camera.main.transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.L) && !UsingMagic && GetComponent<Player>().Mana >= 10)
        {
            ListOfEnemies.Clear();
            ListOfEnemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

            for (int i = 0; i < ListOfEnemies.Count; ++i)
            {
                ListOfEnemies[i].GetComponent<Enemy_AI>().DisableScript();
            }
            AntiGravity = true;
            UsingMagic = true;
            StartCoroutine(ShakeCamera());
            GetComponent<Player>().Mana -= 10;
        }
        if (AntiGravity)
        {
            if (ElapsedVariableForMagic < 1)
            {
                ElapsedVariableForMagic += Time.deltaTime;
                for (int i = 0; i < ListOfEnemies.Count; ++i)
                {
                    ListOfEnemies[i].transform.position = new Vector3(ListOfEnemies[i].transform.position.x, ListOfEnemies[i].transform.position.y + (Time.deltaTime * 5), ListOfEnemies[i].transform.position.z);
                }
            }
            else
            {
                for (int i = 0; i < ListOfEnemies.Count; ++i)
                {
                    ListOfEnemies[i].GetComponent<Enemy_AI>().EnableScript(Explosion);
                }
                UsingMagic = false;
                AntiGravity = false;
                ElapsedVariableForMagic = 0;
            }
        }
        else if (BigUlt)
        {
            LightOfScene.intensity -= Time.deltaTime;
            if (LightOfScene.intensity <= 0.01f && !CreatedDomain)
            {
                //Make the new skybox brighter
                Universe.GetComponent<Renderer>().sharedMaterial.SetFloat("_Exposure", 0.0f);
                countup = 0;

                //instantiate it as well as the portal
                ClonedUniverse = Instantiate(Universe);
                PortalToComeOutOf.transform.position = new Vector3(0, 400, 0);
                PortalToDelete = Instantiate(PortalToComeOutOf);
                CreatedDomain = true;
                SpawnedSword = false;
            }
            else if (LightOfScene.intensity <= 0.01f)
            {
                countup += Time.deltaTime;
                if (countup < 2)
                {
                    ClonedUniverse.GetComponent<Renderer>().sharedMaterial.SetFloat("_Exposure", countup);
                }
                else if (!SpawnedSword)
                {
                    Vector3 Destination;

                    //draw a ray from player to see where to shoot the fireball;
                    Vector3 origin = transform.position;
                    Vector3 direction = transform.forward;
                    Ray ray = new Ray(origin, direction);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit)) //if Object is hit
                    {
                        Destination = hit.point;
                    }
                    else
                    {
                        Destination = transform.position;
                    }

                    //create the meteor and make it such that the player camera is no longer affected for the duration

                    SwordSpawn.GetComponent<SwordSpell>().SetDestination(Destination);
                    SwordToLookAt = Instantiate(SwordSpawn);
                    gameObject.GetComponent<Player>().UnlockMouse();
                    SpawnedSword = true;
                }

                if (SpawnedSword)
                {
                    if (SwordToLookAt != null)
                    {
                        Camera.main.transform.LookAt(SwordToLookAt.transform);
                    }
                    else
                    {
                        gameObject.GetComponent<Player>().LockMouse();

                        LightOfScene.intensity = 1;
                        Destroy(ClonedUniverse);
                        Destroy(PortalToDelete);
                        BigUlt = false;
                    }
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            FireBalls.GetComponent<MeteorSwarmScript>().FindPlayer(gameObject);
            Instantiate(FireBalls);
        }
        if (Input.GetKeyUp(KeyCode.P) && !BigUlt)
        {
            BigUlt = true;
            CreatedDomain = false;
        }
        else if (Input.GetKeyUp(KeyCode.P) && BigUlt)
        {
            LightOfScene.intensity = 1;
            Destroy(ClonedUniverse);
            Destroy(PortalToDelete);
            Destroy(SwordToLookAt);
            BigUlt = false;
        }
    }
    IEnumerator ShakeCamera()
    {
        float elapsedTime = 0;
        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = Curve.Evaluate(elapsedTime / shakeDuration);
            Camera.main.transform.localPosition = startPosition + Random.insideUnitSphere * strength;
            yield return null;
        }
        //reset to start position
        Camera.main.transform.localPosition = startPosition;
    }
}
