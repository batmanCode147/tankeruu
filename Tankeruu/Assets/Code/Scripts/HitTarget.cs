using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    [SerializeField] private GameObject _hitFX;

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    public void Explode()
    {
        Instantiate(_hitFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
