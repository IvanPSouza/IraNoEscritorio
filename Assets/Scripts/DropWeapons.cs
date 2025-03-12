using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DropWeapons : MonoBehaviourPun
{
    // Lista de objetos que podem ser instanciados
    public List<GameObject> objetosParaSpawn;

    // Lista de GameObjects que representam as posi��es de spawn
    public List<GameObject> posicoesDeSpawn;

    // Intervalo de tempo entre os spawns
    public float tempoDeSpawn = 5f;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient) // Apenas o MasterClient gerencia o spawn
        {
            StartCoroutine(InvocarObjeto());
        }
    }

    IEnumerator InvocarObjeto()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoDeSpawn);

            // Escolhe um objeto aleat�rio da lista
            GameObject objetoEscolhido = objetosParaSpawn[Random.Range(0, objetosParaSpawn.Count)];

            // Escolhe uma posi��o aleat�ria da lista de posi��es
            GameObject posicaoEscolhida = posicoesDeSpawn[Random.Range(0, posicoesDeSpawn.Count)];

            // Instancia o objeto na posi��o escolhida de forma sincronizada
            PhotonNetwork.Instantiate("prefabs/" + objetoEscolhido.name, posicaoEscolhida.transform.position, Quaternion.identity);
        }
    }
}
