using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponDaddy : MonoBehaviourPunCallbacks, IPunObservable
{
    public float offsetAngle = 0f; // Ajustar para a direção correta da mira

    private float currentAngle;

    void Update()
    {
        if (photonView.IsMine)
        {
            LookAtSecondPlayer(); // Atualiza a mira para o segundo jogador mais próximo
        }
        else
        {
            SyncRotation(); // Sincroniza a rotação para os outros jogadores
        }
    }

    void LookAtSecondPlayer()
    {
        // Encontra todos os jogadores com a tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            // Ordena os jogadores pela distância em relação ao jogador local
            GameObject closestPlayer = null;
            GameObject secondClosestPlayer = null;
            float closestDistance = Mathf.Infinity;
            float secondClosestDistance = Mathf.Infinity;

            foreach (GameObject player in players)
            {
                // Ignora o jogador local
                if (player == gameObject) continue;

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

            // Se houver um segundo jogador, a mira será direcionada para ele
            if (secondClosestPlayer != null)
            {
                Vector3 targetPosition = secondClosestPlayer.transform.position;
                Vector3 direction = targetPosition - transform.position;

                // Calcula o ângulo necessário para rotacionar em direção ao segundo jogador
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Aplica o ângulo de offset, se necessário
                angle += offsetAngle;

                // Atualiza a rotação do objeto
                currentAngle = angle;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }

    // Sincroniza a rotação com os outros jogadores
    void SyncRotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
    }

    // Método para enviar e receber a rotação através da rede
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Envia a rotação para os outros jogadores
            stream.SendNext(currentAngle);
        }
        else
        {
            // Recebe a rotação dos outros jogadores
            currentAngle = (float)stream.ReceiveNext();
            SyncRotation(); // Atualiza a rotação do jogador remoto
        }
    }
}

