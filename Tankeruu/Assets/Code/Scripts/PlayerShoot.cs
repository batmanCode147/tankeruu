using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = default;
    [SerializeField] private Transform _debugTarget;
    private Camera _camera;
    private Vector3 _lookAt;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        GetInput();
        RotateTowards();

        _debugTarget.position = _lookAt;
    }

    private void RotateTowards()
    {
        Vector3 dir = _lookAt - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(dir, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
    }

    private void GetInput()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            _lookAt = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
    }
}
