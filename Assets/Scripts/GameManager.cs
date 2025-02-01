using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform[] points;

    // Start is called before the first frame update
    void Start()
    {

        if (playerPrefab == null)
        {
            Debug.Log("Prefab di jogador não definido no GameManager");
        }
        else
        {
            PhotonNetwork.Instantiate("Prefabs/" + playerPrefab.name, points[Random.Range(0, points.Length)].position, Quaternion.identity);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
