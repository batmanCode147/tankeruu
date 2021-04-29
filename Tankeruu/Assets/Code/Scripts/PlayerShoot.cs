using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = default;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _missileSpeed;
    [SerializeField] private float _missileSteerSpeed;

    [SerializeField] private Transform _debugTarget;
    [SerializeField] private GameObject _projectile = default;
    [SerializeField] private GameObject _missile = default;
    [SerializeField] private Transform _missileSpawn = default;

    private Camera _camera;
    private Vector3 _targetPos;
    private const int MOUSE_LEFT_BTN = 0;
    private const int MOUSE_RIGHT_BTN = 1;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        GetInput();
        RotateTowards();

        _debugTarget.position = _targetPos;
    }

    private void RotateTowards()
    {
        Vector3 toTarget = _targetPos - transform.position;
        Quaternion desiredRotation = Quaternion.LookRotation(toTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);
    }

    private void GetInput()
    {
        CalcMousePos();

        if (Input.GetMouseButtonDown(MOUSE_LEFT_BTN))
        {
            ShootProjectile();
        }

        if (Input.GetMouseButtonDown(MOUSE_RIGHT_BTN))
        {
            ShootMissile();
        }
    }

    private void ShootMissile()
    {
        Vector3 target = MouseRayCast();
        
        GameObject missile = Instantiate(_missile, _missileSpawn.position, Quaternion.Euler(-90f,0f,0f));
        
        HomingMissile missileBrain = missile.GetComponent<HomingMissile>();

        missileBrain.Speed = _missileSpeed;
        missileBrain.RotationSpeed = _missileSteerSpeed;
        missileBrain.Target = target;
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(_projectile, transform.position + transform.forward * 3f, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * _projectileSpeed);
    }

    private void CalcMousePos()
    {
        Vector3 mouseScreenPos = Input.mousePosition;

        // account for offset of camera
        mouseScreenPos.z = Camera.main.transform.position.y - transform.position.y;

        Vector3 mouseWorldPos = _camera.ScreenToWorldPoint(mouseScreenPos);

        Debug.DrawLine(transform.position, mouseWorldPos, Color.red);
        _targetPos = mouseWorldPos;
    }

    private Vector3 MouseRayCast()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
            return hit.point;

        return default;
    }
}
