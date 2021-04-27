using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = default;
    [SerializeField] private float _rotationSpeed = default;
    private Rigidbody _rigidbody;
    private float _movement;
    private float _rotation;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        transform.forward = Quaternion.AngleAxis(_rotation * _rotationSpeed * Time.deltaTime, Vector3.up) * transform.forward;
        _rigidbody.AddForce(_movement * transform.forward * _movementSpeed);
    }

    private void GetInput()
    {
        _movement = 0f;
        _rotation = 0f;

        if(Input.GetKey(KeyCode.W))
            _movement = 1f;
        if(Input.GetKey(KeyCode.S))
            _movement = -1;
        if(Input.GetKey(KeyCode.A))
            _rotation = -1f;
        if(Input.GetKey(KeyCode.D))
            _rotation = 1;
    }
}
