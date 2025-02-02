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
            LookAtCursor(); // Atualiza a mira apenas para o jogador local
        }
        else
        {
            SyncRotation(); // Sincroniza a rotação para os outros jogadores
        }
    }

    void LookAtCursor()
    {
        // Obtém a posição do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ignora a profundidade, pois estamos em 2D

        // Calcula a direção do objeto até o mouse
        Vector3 direction = mousePosition - transform.position;

        // Calcula o ângulo necessário para rotacionar em direção ao mouse
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplica o ângulo de offset, se necessário
        angle += offsetAngle;

        // Atualiza a rotação do objeto
        currentAngle = angle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
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
