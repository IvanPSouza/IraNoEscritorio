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

    // Nova variável para o GameObject que será usado para determinar a direção
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
        // Se não for o jogador local, a lógica de disparo será tratada pelos RPCs.
    }

    private void shoot1()
    {
        ShootCounter1 += Time.deltaTime;

        if (ShootTime1 <= ShootCounter1)
        {
            // Lógica de disparo no botão esquerdo do mouse
            if (Input.GetMouseButton(0))
            {
                // Chama a função RPC para disparar o projétil em todos os jogadores
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
            // Lógica de disparo no botão esquerdo do mouse
            if (Input.GetMouseButton(0))
            {
                // Chama a função RPC para disparar o projétil em todos os jogadores
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
            // Lógica de disparo no botão esquerdo do mouse
            if (Input.GetMouseButton(0))
            {
                // Chama a função RPC para disparar o projétil em todos os jogadores
                photonView.RPC("FireProjectile3", RpcTarget.All, BulletPosition.position, DirectionTarget.transform.rotation);
                ShootCounter3 = 0;
            }
        }
    }

    // RPC que será chamada para disparar o projétil 1
    [PunRPC]
    private void FireProjectile1(Vector3 position, Quaternion direction)
    {
        GameObject proj = Instantiate(Projectil1, position, direction);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * 15f;  // Ajuste a velocidade do projétil conforme necessário
        }
    }

    // RPC que será chamada para disparar o projétil 2
    [PunRPC]
    private void FireProjectile2(Vector3 position, Quaternion direction)
    {
        GameObject proj = Instantiate(Projectil2, position, direction);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * 15f;  // Ajuste a velocidade do projétil conforme necessário
        }
    }

    // RPC que será chamada para disparar o projétil 3
    [PunRPC]
    private void FireProjectile3(Vector3 position, Quaternion direction)
    {
        GameObject proj = Instantiate(Projectil3, position, direction);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * 20f;  // Ajuste a velocidade do projétil conforme necessário
        }
    }
}
