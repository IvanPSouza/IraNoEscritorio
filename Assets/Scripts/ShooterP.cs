using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class shooterP : MonoBehaviourPunCallbacks
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    #region projetil1

    [SerializeField] GameObject Projectil1;
    [SerializeField] float ShootTime1 = 0.2f;
    private float ShootCounter1 = 0f;
    [SerializeField] float speed1 = 15f;
    [SerializeField] float spreed1 = 10f;

    #endregion

    #region projetil2

    [SerializeField] GameObject Projectil2;
    [SerializeField] float ShootTime2 = 0.5f;
    private float ShootCounter2 = 0f;
    [SerializeField] float speed2 = 15f;
    [SerializeField] float spreed2 = 0f;

    #endregion

    #region projetil3

    [SerializeField] GameObject Projectil3;
    [SerializeField] float ShootTime3 = 1f;
    private float ShootCounter3 = 0f;
    [SerializeField] float speed3 = 20f;
    [SerializeField] float spreed3 = 0f;

    #endregion

    #region projetil3

    [SerializeField] GameObject Projectil4;
    [SerializeField] float ShootTime4 = 1f;
    private float ShootCounter4 = 0f;
    [SerializeField] float speed4 = 20f;
    [SerializeField] float spreed4 = 0f;

    #endregion

    [SerializeField] Transform BulletPosition;
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
            if (CurrentWeapon == 4)
            {
                shoot4();
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
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                CurrentWeapon = 4;
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
    private void shoot4()
    {
        ShootCounter4 += Time.deltaTime;

        if (ShootTime4 <= ShootCounter4)
        {
            // Lógica de disparo no botão esquerdo do mouse
            if (Input.GetMouseButton(0))
            {
                // Chama a função RPC para disparar o projétil em todos os jogadores
                photonView.RPC("FireProjectile4", RpcTarget.All, BulletPosition.position, DirectionTarget.transform.rotation);
                ShootCounter4 = 0;
            }
        }
    }

    // RPC que será chamada para disparar o projétil 1
    [PunRPC]
    private void FireProjectile1(Vector3 position, Quaternion direction)
    {
        float angleVariation = UnityEngine.Random.Range(-spreed1, spreed1);
        Quaternion newDirection = direction * Quaternion.Euler(0, 0, angleVariation);
        GameObject proj = Instantiate(Projectil1, position, newDirection);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * speed1;  // Ajuste a velocidade do projétil conforme necessário
        }
    }

    // RPC que será chamada para disparar o projétil 2
    [PunRPC]
    private void FireProjectile2(Vector3 position, Quaternion direction)
    {
        float angleVariation = UnityEngine.Random.Range(-spreed2, spreed2);
        Quaternion newDirection = direction * Quaternion.Euler(0, 0, angleVariation);
        GameObject proj = Instantiate(Projectil2, position, newDirection);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * speed2;  // Ajuste a velocidade do projétil conforme necessário
        }
    }

    // RPC que será chamada para disparar o projétil 3
    [PunRPC]
    private void FireProjectile3(Vector3 position, Quaternion direction)
    {
        float angleVariation = UnityEngine.Random.Range(-spreed3, spreed3);
        Quaternion newDirection = direction * Quaternion.Euler(0, 0, angleVariation);
        GameObject proj = Instantiate(Projectil3, position, newDirection);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * speed3;  // Ajuste a velocidade do projétil conforme necessário
        }
    }
    [PunRPC]
    private void FireProjectile4(Vector3 position, Quaternion direction)
    {
        float angleVariation = UnityEngine.Random.Range(-spreed4, spreed4);
        Quaternion newDirection = direction * Quaternion.Euler(0, 0, angleVariation);
        GameObject proj = Instantiate(Projectil4, position, newDirection);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = proj.transform.right * speed4;  // Ajuste a velocidade do projétil conforme necessário
        }
    }
}
