using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] protected float lifeTime = 5.0f;

    private void OnEnable()
    {
        Invoke("Rest", lifeTime);
    }
    private void Rest()
    {
        this.gameObject.SetActive(false);
    }
}
