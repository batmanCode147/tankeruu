using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _spawnPosition;

    public Vector3 Target { get; set; }
    public float Speed { get; set; }
    public float RotationSpeed { get; set; }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _spawnPosition = _rigidbody.position;
    }

    private void FixedUpdate()
    {
        SeekTarget();
    }

    private void SeekTarget()
    {
        // fly and rotate towards target
        Quaternion rotateTowards = Quaternion.LookRotation(Target - _rigidbody.position);
        
        _rigidbody.velocity = transform.forward * Speed;
        _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotateTowards, RotationSpeed));
    }
}
