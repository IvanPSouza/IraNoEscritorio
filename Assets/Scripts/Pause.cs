using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] public GameObject paused;
    private float pausado = 0;
    // Start is called before the first frame update
    void Start()
    {
        pausado = 0;
        paused.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausado == 0)
        {
            pausado = 1;
            paused.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pausado == 1)
        {
            pausado = 0;
            paused.SetActive(false);
        }
    }
    public void Unpause()
    {
        pausado = 0;
        paused.SetActive(false);
    }
}