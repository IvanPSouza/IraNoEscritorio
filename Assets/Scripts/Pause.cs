using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] public GameObject paused;
    [SerializeField] float pausado = 0;
    [SerializeField] KeyCode key = KeyCode.P;
    private int time = 3;
    // Start is called before the first frame update
    void Start()
    {
        if (pausado == 0)
        {
            pausado = 0;
            paused.SetActive(false);
        }
        else
        {
            pausado = 1;
            paused.SetActive(true);
        }
        {
            StartCoroutine(ExecutarAposTempo());
        }

        IEnumerator ExecutarAposTempo()
        {
            Debug.Log("Esperando...");
            yield return new WaitForSeconds(0.5f); // Espera 0.5 segundos
            Debug.Log("Executou depois de 0.5 segundos!");
            pausado = 0;
            paused.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && pausado == 0)
        {
            pausado = 1;
            paused.SetActive(true);
        }
        else if (Input.GetKeyDown(key) && pausado == 1)
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
