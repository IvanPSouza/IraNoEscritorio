using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class shooterP : MonoBehaviourPunCallbacks
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    [SerializeField] GameObject Projectil1;
    [SerializeField] GameObject Projectil2;
    [SerializeField] GameObject Projectil3;
    [SerializeField] Transform BulletPosition;
    [SerializeField] float ShootTime1 = 0.2f;
    private float ShootCounter1 = 0f;
    [SerializeField] float ShootTime2 = 0.5f;
    private float ShootCounter2 = 0f;
    [SerializeField] float ShootTime3 = 1f;
    private float ShootCounter3 = 0f;
    private int CurrentWeapon = 1;

    // Nova vari�vel para o GameObject que ser� usado para determinar a dire��o
    [SerializeField] GameObject DirectionTarget;

    void Update()
    {
        // Apenas o jogador local pode controlar o disparo.
        if (photonView.IsMine)
        {
            if (CurrentWeapon == 1)
            {
                shoot1();
            }
            if (CurrentWeapon == 2)
            {
                shoot2();
            }
            if (CurrentWeapon == 3)
            {
                shoot3();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CurrentWeapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CurrentWeapon = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CurrentWeapon = 3;
            }
        }
        // Se n�o for o jogador local, a l�gica de disparo ser� tratada pelos RPCs.
    }

    private void shoot1()
    {
        ShootCounter1 += Time.deltaTime;

        if (ShootTime1 <= ShootCounter1)
        {
            // L�gica de disparo no bot�o esquerdo do mouse
            if (Input.GetMouseButton(0))
            {
                // Chama a fun��o RPC para disparar o proj�til em todos os jogadores
                photonView.RPC("FireProjectile1", RpcTarget.All, BulletPosition.position, DirectionTarget.transform.rotation);
                ShootCounter1 = 0;
            }
        }
    }

    private void shoot2()
    {
        ShootCounter2 += Time.deltaTime;

        if (ShootTime2 <= ShootCounter2)
        {
            // L�gica de disparo no bot�o esquerdo do mouse
            if (Input.GetMouseButton(0))
            {
                // Chama a fun��o RPC para disparar o proj�til em todos os jogadores
                photonView.RPC("FireProjectile2", RpcTarget.All, BulletPosition.position, DirectionTarget.transform.rotation);
                ShootCounter2 = 0;
            }
        }
    }

    private void shoot3()
    {
        ShootCounter3 += Time.deltaTime;

        if (ShootTime3 <= ShootCounter3)
        {
            // L�gica de disparo no bot�o esquerdo do mouse
            if (Input.GetMouseButton(0))
            {
                // Chama a fun��o RPC para disparar o proj�til em todos os jogadores
                photonView.RPC("FireProjectile3", RpcTarget.All, BulletPosition.position, DirectionTarget.transform.rotation);
                ShootCounter3 = 0;
            }
        }
    }

    // RPC que ser� chamada para disparar o proj�til 1
    [PunRPC]
    private void FireProjectile1(Vector3 position, Quaternion direction)
    {
        GameObject proj = Instantiate(Projectil1, position, direction);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * 15f;  // Ajuste a velocidade do proj�til conforme necess�rio
        }
    }

    // RPC que ser� chamada para disparar o proj�til 2
    [PunRPC]
    private void FireProjectile2(Vector3 position, Quaternion direction)
    {
        GameObject proj = Instantiate(Projectil2, position, direction);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * 15f;  // Ajuste a velocidade do proj�til conforme necess�rio
        }
    }

    // RPC que ser� chamada para disparar o proj�til 3
    [PunRPC]
    private void FireProjectile3(Vector3 position, Quaternion direction)
    {
        GameObject proj = Instantiate(Projectil3, position, direction);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * 20f;  // Ajuste a velocidade do proj�til conforme necess�rio
        }
    }
}
