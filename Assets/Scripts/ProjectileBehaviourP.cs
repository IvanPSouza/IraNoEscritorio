using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEngine.Rendering.DebugUI.Table;

public class ProjectileBehaviourP : MonoBehaviour
{
    private Vector3 target;
    private Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    [SerializeField] float LifeTime = 5f;
    private float LifeTimeCounter = 0;
    [SerializeField] float SpinSpeed = 0f;
    //[SerializeField] Boolean Decay = false;
    void Start()
    {
        do
        {
            speed += 1;
        } while (speed <= 0);
        rb = GetComponent<Rigidbody2D>();
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        target = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 direction = target - transform.position;
        //- transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 180);
        transform.Rotate(new Vector3(0, 0, 45), Space.Self);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Decay == true)
        //{
            LifeTimeCounter += Time.deltaTime;
            if (LifeTime <= LifeTimeCounter)
            {
                Destroy(gameObject);
            }
        //}
        if (SpinSpeed != 0f)
        {
            transform.Rotate(new Vector3(0, 0, SpinSpeed), Space.Self);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.CompareTag("Wall") || col.CompareTag("Ground"))
        //{
        //    Decay = true;
        //    //Destroy(gameObject);
        //}

        LifeTime = 0.1f;
    }
}

