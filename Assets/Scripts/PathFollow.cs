using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public Action OnMoveComplete = delegate { };
    public Path path;
    public float speed = 1f;

    private Transform _transform;
    private float progress = 0f;
    private bool continueMoving = true;

    public void SetPath(Path path)
    {
        this.path = path;
    }

    private void Start()
    {
        _transform = transform;
    }

    private void Update()
    {
        if(continueMoving)
            MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if(progress < 1)
        {
            progress += speed * Time.deltaTime;
            _transform.position = Bezier.GetPoint(path.waypoints[0].position, path.waypoints[1].position, 
                                                 path.waypoints[2].position, path.waypoints[3].position, progress);

            _transform.up = path.GetDirection(progress);
        }
        else
        {
            continueMoving = false;
            OnMoveComplete();
        }
    }

    private void OnEnable()
    {
        progress = 0;
        continueMoving = true;
    }
}
