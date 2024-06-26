using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    //Spawn variables
    [Header("Spawner parameters")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject indicatorPrefab;
    [SerializeField] float shootRate = 2.5f;
    [SerializeField] int lasersPoolingCount = 25;
    [SerializeField] int indicatorsPoolingCount = 15;
    private int lasersPerShoot = 4;
    private List<GameObject> pooledLasers = new List<GameObject>();
    private List<GameObject> pooledIndicators = new List<GameObject>();


    //Variables used to limit spawn range.
    private Vector2 spawnLimits = new Vector2(-14, 15);
    private Vector2 rotationLimits = new Vector2(50, 180);

    // Start is called before the first frame update
    void Start()
    {
        PrepareObjects();
        StartCoroutine(ShootLaser());
    }

    //Couroutine that controls laser's shot.
    IEnumerator ShootLaser()
    {
        int xPos;
        int yPos;
        float eulerY;
        float eulerX;
        List<Vector3> positions = new List<Vector3>();
        List<Quaternion> rotations = new List<Quaternion>();
        GameObject indicatorAux;
        GameObject laserAux;

        //First we spawn indicators for tha lasers
        for (int i = 0; i < lasersPerShoot; i++)
        {
            indicatorAux = GetPooledIndicator();
            // If there not an available object, we create one and we add it to the list
            if (indicatorAux == null)
            {
                indicatorAux = Instantiate(laserPrefab);
                pooledIndicators.Add(indicatorAux);
            }

            //Calculation position
            xPos = Random.Range((int)spawnLimits.x, (int)spawnLimits.y);
            yPos = Random.Range((int)spawnLimits.x, (int)spawnLimits.y);
            Vector3 indicatorPos = new Vector3(xPos, 0, yPos);
            positions.Add(indicatorPos);

            // We aplied position and rotation to the indicator
            indicatorAux.SetActive(true);
            indicatorAux.transform.SetLocalPositionAndRotation(indicatorPos, Quaternion.identity);

            //Calculation of rotation
            eulerX = Random.Range(-rotationLimits.x, rotationLimits.x);
            eulerY = Random.Range(-rotationLimits.y, rotationLimits.y);
            rotations.Add(Quaternion.Euler(eulerX, eulerY, 0));
        }


        yield return new WaitForSeconds(shootRate);

        //Secondly, we spawn the lasers
        for (int i = 0; i < positions.Count; i++)
        {
            laserAux = GetPooledLaser();
            // If there not an available object, we create one and we add it to the list
            if (laserAux == null)
            {
                laserAux = Instantiate(laserPrefab);
                pooledLasers.Add(laserAux);
            }
            // We aplied position and rotation to the laser
            laserAux.SetActive(true);
            laserAux.transform.SetPositionAndRotation(positions[i], rotations[i]);
        }

        //Repeating the process
        StartCoroutine(ShootLaser());
    }

    //Instatiates initial object for object pooling
    private void PrepareObjects()
    {
        for (int i = 0; i < lasersPoolingCount; i++)
        {
            GameObject laser = Instantiate(laserPrefab, transform);
            pooledLasers.Add(laser);
            laser.SetActive(false);
        }

        for (int i = 0; i < indicatorsPoolingCount; i++)
        {
            GameObject indicator = Instantiate(indicatorPrefab, transform);
            pooledIndicators.Add(indicator);
            indicator.SetActive(false);
        }
    }

    //Return an available laser object to spawn
    private GameObject GetPooledLaser()
    {
        foreach (GameObject obj in pooledLasers)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    //Return an available indicator object to spawn
    private GameObject GetPooledIndicator()
    {
        foreach (GameObject obj in pooledIndicators)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null;
    }

    //Increse the amount of active lasers.
    public void IncreaseLasersPerShoot(int lasersAdded)
    {
        lasersPerShoot += lasersAdded;
    }
}
