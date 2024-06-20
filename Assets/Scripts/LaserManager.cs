using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
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
        
        yield return new WaitForSeconds(shootRate);

        for (int i = 0; i < lasersPerShoot; i++)
        {
            xPos = Random.Range((int)spawnLimits.x, (int)spawnLimits.y);
            yPos = Random.Range((int)spawnLimits.x, (int)spawnLimits.y);

            eulerX = Random.Range(-rotationLimits.x, rotationLimits.x);
            eulerY = Random.Range(-rotationLimits.y, rotationLimits.y);
            Quaternion laserRotation = Quaternion.Euler(eulerX, eulerY, 0);

            Vector3 laserPosition = new Vector3(xPos, 0, yPos);
            Instantiate(laserPrefab, laserPosition, laserRotation);
        }
        StartCoroutine(ShootLaser());
    }
}
