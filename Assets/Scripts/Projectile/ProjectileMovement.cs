using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    public int moveSpeed = 20;
    public bool invertDirection;

    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }

    void Update()
    {
        if(!invertDirection)
            _transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        else
            _transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
    }
}
