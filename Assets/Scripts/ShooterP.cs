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
    [SerializeField] Transform BulletPosition;
    [SerializeField] float ShootTime1 = 0.2f;
    private float ShootCounter1 = 0f;
    [SerializeField] float ShootTime2 = 0.5f;
    private float ShootCounter2 = 0f;
    private int CurrentWeapon = 1;

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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CurrentWeapon = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CurrentWeapon = 2;
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
                photonView.RPC("FireProjectile1", RpcTarget.All, BulletPosition.position);
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
                photonView.RPC("FireProjectile2", RpcTarget.All, BulletPosition.position);
                ShootCounter2 = 0;
            }
        }
    }

    // RPC que ser� chamada para disparar o proj�til 1
    [PunRPC]
    private void FireProjectile1(Vector3 position)
    {
        Instantiate(Projectil1, position, Quaternion.identity);
    }

    // RPC que ser� chamada para disparar o proj�til 2
    [PunRPC]
    private void FireProjectile2(Vector3 position)
    {
        Instantiate(Projectil2, position, Quaternion.identity);
    }
}
