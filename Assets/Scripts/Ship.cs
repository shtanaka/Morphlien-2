using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private int bulletsPerCycle = 7;
    [SerializeField] private float shipVelocity = 10f;
    [SerializeField] private float bulletVelocity = 2000f;
    [SerializeField] private GameObject regularBulletPrefab;

    private float throttleIncrease;
    private float xMin, xMax, yMin, yMax;

    void Start()
    {
        throttleIncrease = 0;
        SetupMoveBoundaries();
    }

    void Update()
    {
        MoveShip();
        Fire();
    }

    private void SetupMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        Vector2 shipSize = GetComponent<Renderer>().bounds.size;
        float xPadding = shipSize.x / 2;
        float yPadding = shipSize.y / 2;

        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    private void MoveShip()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * shipVelocity;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * shipVelocity;
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        
        transform.position = new Vector2(newXPosition, newYPosition);
    }

    private void Fire()
    {
        throttleIncrease += bulletsPerCycle * Time.deltaTime;
        if (Input.GetButton("Fire1") && throttleIncrease > 1)
        {
            throttleIncrease = 0;
            Vector2 shipSize = GetComponent<Renderer>().bounds.size;
            var bulletInstancePosition = new Vector2(transform.position.x, transform.position.y + shipSize.y / 2);
            var bullet = Instantiate(regularBulletPrefab, bulletInstancePosition, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, Time.deltaTime * bulletVelocity);
            Destroy(bullet, 2f);
        }
    }
}
