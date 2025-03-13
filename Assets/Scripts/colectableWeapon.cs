using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class colectableWeapon : MonoBehaviour
{
    [SerializeField] float unusableTime = 1;
    [SerializeField] Boolean colectable = false;
    [SerializeField] Boolean beenCollected = false;
    [SerializeField] int weaponType = 1;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(0>=unusableTime)
        {
            unusableTime -= Time.deltaTime;
        }
        
        if (0 <= unusableTime)
        {
            colectable = true;
        }

        if (beenCollected == true)
        {
                
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( colectable == true && collision.collider.CompareTag("Player"))
        {
            beenCollected = true;

            PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();

            player.PegaArma(weaponType);
                
            Destroy(gameObject);
        }
    }
}
