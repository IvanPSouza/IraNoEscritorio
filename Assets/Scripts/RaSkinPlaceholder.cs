using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaSkinPlaceholder : MonoBehaviour
{
    public List<GameObject> objects; // Lista de objetos a serem ativados/desativados

    void Start()
    {
        ActivateRandomObject();
    }

    void ActivateRandomObject()
    {
        if (objects == null || objects.Count == 0)
        {
            Debug.LogWarning("A lista de objetos est� vazia ou n�o foi atribu�da!");
            return;
        }

        // Desativa todos os objetos
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        // Escolhe um objeto aleat�rio e ativa ele
        int randomIndex = Random.Range(0, objects.Count);
        objects[randomIndex].SetActive(true);
    }
}

