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
            LookAtCursor(); // Atualiza a mira apenas para o jogador local
        }
        else
        {
            SyncRotation(); // Sincroniza a rota��o para os outros jogadores
        }
    }

    void LookAtCursor()
    {
        // Obt�m a posi��o do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ignora a profundidade, pois estamos em 2D

        // Calcula a dire��o do objeto at� o mouse
        Vector3 direction = mousePosition - transform.position;

        // Calcula o �ngulo necess�rio para rotacionar em dire��o ao mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica o �ngulo de offset, se necess�rio
        angle += offsetAngle;

        // Atualiza a rota��o do objeto
        currentAngle = angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
