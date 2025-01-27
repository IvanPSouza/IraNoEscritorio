using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.UIElements;
using System.Reflection;

public class shooterP : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    [SerializeField] GameObject Projectil1;
    [SerializeField] Transform BulletPosition;
    [SerializeField] float ShootTime = 10f;
    private float ShootCounter = 0f;
    void Update()
    {
        shoot1();
    }
    private void shoot1()
    {
        ShootCounter += Time.deltaTime;

        if (ShootTime <= ShootCounter)
        {//Starts to trow projectile if L.click

            if (Input.GetMouseButton(0))
            {
                Instantiate(Projectil1, BulletPosition.position, Quaternion.identity);

                ShootCounter = 0;
            }
        }
    }
}