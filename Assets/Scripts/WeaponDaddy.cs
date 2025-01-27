using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponDaddy : MonoBehaviour
{
    // Adjust this value based on your object's design

    public float offsetAngle = 0f; // Adjust to rotate the object to face the correct direction



    void Update()

    {

        LookAtCursor();

    }



    void LookAtCursor()

    {

        // Get the mouse position in world space

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mousePosition.z = 0; // Set z to 0 to ignore depth



        // Calculate the direction from the object to the mouse

        Vector3 direction = mousePosition - transform.position;



        // Calculate the angle needed to rotate towards the mouse

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;



        // Apply the offset angle if needed

        angle += offsetAngle;



        // Set the rotation of the object

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

    }
}
