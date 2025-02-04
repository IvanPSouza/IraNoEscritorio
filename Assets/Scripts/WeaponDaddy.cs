using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponDaddy : MonoBehaviourPunCallbacks, IPunObservable
{
    public float offsetAngle = 0f; // Ajustar para a dire��o correta da mira

    private float currentAngle;

    void Update()
    {
        if (photonView.IsMine)
        {
            LookAtSecondPlayer(); // Atualiza a mira para o segundo jogador mais pr�ximo
        }
        else
        {
            SyncRotation(); // Sincroniza a rota��o para os outros jogadores
        }
    }

    void LookAtSecondPlayer()
    {
        // Encontra todos os jogadores com a tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            // Ordena os jogadores pela dist�ncia em rela��o ao jogador local
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

            // Se houver um segundo jogador, a mira ser� direcionada para ele
            if (secondClosestPlayer != null)
            {
                Vector3 targetPosition = secondClosestPlayer.transform.position;
                Vector3 direction = targetPosition - transform.position;

                // Calcula o �ngulo necess�rio para rotacionar em dire��o ao segundo jogador
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Aplica o �ngulo de offset, se necess�rio
                angle += offsetAngle;

                // Atualiza a rota��o do objeto
                currentAngle = angle;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }

    // Sincroniza a rota��o com os outros jogadores
    void SyncRotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
    }

    // M�todo para enviar e receber a rota��o atrav�s da rede
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Envia a rota��o para os outros jogadores
            stream.SendNext(currentAngle);
        }
        else
        {
            // Recebe a rota��o dos outros jogadores
            currentAngle = (float)stream.ReceiveNext();
            SyncRotation(); // Atualiza a rota��o do jogador remoto
        }
    }
}

