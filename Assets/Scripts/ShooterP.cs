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
    [SerializeField] GameObject Projectil2;
    [SerializeField] Transform BulletPosition;
    [SerializeField] float ShootTime1 = 0.2f;
    private float ShootCounter1 = 0f;
    [SerializeField] float ShootTime2 = 0.5f;
    private float ShootCounter2 = 0f;
    private int CurrentWeapon = 1;
    void Update()
    {
        if (CurrentWeapon == 1)
        {
            shoot1();
        }
        if (CurrentWeapon == 2)
        {
            shoot2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentWeapon = 2;
        }
    }
    private void shoot1()
    {
        ShootCounter1 += Time.deltaTime;

        if (ShootTime1 <= ShootCounter1)
        {//Starts to trow projectile if L.click

            if (Input.GetMouseButton(0))
            {
                Instantiate(Projectil1, BulletPosition.position, Quaternion.identity);

                ShootCounter1 = 0;
            }
        }
    }
    private void shoot2()
    {
        ShootCounter2 += Time.deltaTime;

        if (ShootTime2 <= ShootCounter2)
        {//Starts to trow projectile if L.click

            if (Input.GetMouseButton(0))
            {
                Instantiate(Projectil2, BulletPosition.position, Quaternion.identity);

                ShootCounter2 = 0;
            }
        }
    }
}

