using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviourP : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float LifeTime = 5f;
    private float LifeTimeCounter = 0;
    [SerializeField] float SpinSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
