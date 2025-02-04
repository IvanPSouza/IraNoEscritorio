using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviourP : MonoBehaviour
{
    private Vector3 target;
    private Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] float LifeTime = 5f;
    private float LifeTimeCounter = 0;
    [SerializeField] float SpinSpeed = 0f;

    void Start()
    {
        do
        {
            speed += 1;
        } while (speed <= 0);

        rb = GetComponent<Rigidbody2D>();

        // Encontra todos os objetos com a tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            // Ordena os jogadores pela distância em relação ao projétil
            GameObject closestPlayer = null;
            GameObject secondClosestPlayer = null;
            float closestDistance = Mathf.Infinity;
            float secondClosestDistance = Mathf.Infinity;

            foreach (GameObject player in players)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);

                if (distance < closestDistance)
                {
                    secondClosestDistance = closestDistance;
                    secondClosestPlayer = closestPlayer;
                    closestDistance = distance;
                    closestPlayer = player;
                }
                else if (distance < secondClosestDistance)
                {
                    secondClosestDistance = distance;
                    secondClosestPlayer = player;
                }
            }

            // Se houver um segundo jogador, o projétil vai em direção a ele
            if (secondClosestPlayer != null)
            {
                target = secondClosestPlayer.transform.position;

                Vector3 direction = target - transform.position;
                rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
                float rot = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, rot + 180);
                transform.Rotate(new Vector3(0, 0, 45), Space.Self);
            }
        }
    }

    void Update()
    {
        LifeTimeCounter += Time.deltaTime;
        if (LifeTime <= LifeTimeCounter)
        {
            Destroy(gameObject);
        }

        if (SpinSpeed != 0f)
        {
            transform.Rotate(new Vector3(0, 0, SpinSpeed), Space.Self);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        LifeTime = 0.5f;
    }
}

