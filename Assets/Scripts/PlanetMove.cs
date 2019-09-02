using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMove : MonoBehaviour
{
    public Sprite[] planets;
    public float moveSpeed = 2f;

    private SpriteRenderer currentPlanet;
    private Transform _transform;
    private Vector3 startPos;
    private Vector3 moveVector = Vector3.down;
    private float endPosY;

    private void Awake()
    {
        _transform = transform;
        currentPlanet = GetComponent<SpriteRenderer>();

        startPos = _transform.position;
        endPosY = -startPos.y;
    }

    void Start()
    {
        ChangePlanetSprite();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        while(true)
        {
            _transform.position += moveVector * moveSpeed * Time.deltaTime;
            if (_transform.position.y < endPosY)
            {
                SetRandomXPos();
                ChangePlanetSprite();
            }
                

            yield return null;
        }
    }

    private void ChangePlanetSprite()
    {
        currentPlanet.sprite = planets[Random.Range(0, planets.Length)];
    }

    private void SetRandomXPos()
    {
        startPos.x = Random.Range(-startPos.x, startPos.x);
        _transform.position = startPos;
    }
}
