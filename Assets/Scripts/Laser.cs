using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected float lifeTime = 5.0f;

    private void Awake()
    {
        Invoke("DestroyLaser", lifeTime);
    }

    private void DestroyLaser()
    {
        Destroy(gameObject);
    }
}
