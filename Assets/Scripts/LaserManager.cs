using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float shootRate = 2.5f;
    private int lasersPerShoot = 1;

    private Vector2 spawnLimits = new Vector2(-14, 15);
    private Vector2 rotationLimits = new Vector2(50, 180);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootLaser());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(shootRate);
        int xPos = Random.Range((int)spawnLimits.x, (int)spawnLimits.y);
        int yPos = Random.Range((int)spawnLimits.x, (int)spawnLimits.y);

        float eulerX = Random.Range(-rotationLimits.x, rotationLimits.x);
        float eulerY = Random.Range(-rotationLimits.y, rotationLimits.y);
        Quaternion laserRotation = Quaternion.Euler(eulerX, eulerY, 0);

        Vector3 laserPosition = new Vector3(xPos, 0, yPos);
        Instantiate(laserPrefab, laserPosition, laserRotation);
        StartCoroutine(ShootLaser());
    }
}
