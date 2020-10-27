using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float velocity = 10f;
    private float xMin, xMax, yMin, yMax;

    void Start()
    {
        SetupMoveBoundaries();
    }

    void Update()
    {
        MoveShip();
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
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * velocity;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * velocity;
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        
        transform.position = new Vector2(newXPosition, newYPosition);
    }
}
