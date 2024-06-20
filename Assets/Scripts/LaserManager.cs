using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject indicatorPrefab;
    [SerializeField] float shootRate = 2.5f;
    private int lasersPerShoot = 3;

    //Variables used to limit spawn range.
    private Vector2 spawnLimits = new Vector2(-14, 15);
    private Vector2 rotationLimits = new Vector2(50, 180);

    // Start is called before the first frame update
    void Start()
    {
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

        for (int i = 0; i < lasersPerShoot; i++)
        {
            xPos = Random.Range((int)spawnLimits.x, (int)spawnLimits.y);
            yPos = Random.Range((int)spawnLimits.x, (int)spawnLimits.y);
            Vector3 indicatorPos = new Vector3(xPos, 0, yPos);
            positions.Add(indicatorPos);
            Instantiate(indicatorPrefab, indicatorPos, Quaternion.identity);


            eulerX = Random.Range(-rotationLimits.x, rotationLimits.x);
            eulerY = Random.Range(-rotationLimits.y, rotationLimits.y);
            rotations.Add(Quaternion.Euler(eulerX, eulerY, 0));
        }


        yield return new WaitForSeconds(shootRate);

        for (int i = 0; i < positions.Count; i++)
        {
            Instantiate(laserPrefab, positions[i], rotations[i]);
        }
        StartCoroutine(ShootLaser());
    }

    public void IncreaseLasersPerShoot(int lasersAdded)
    {
        lasersPerShoot += lasersAdded;
    }
}
