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
            LookAtMouse(); // Atualiza a mira para a posição do mouse
        }
        else
        {
            SyncRotation(); // Sincroniza a rotação para os outros jogadores
        }
    }

    void LookAtMouse()
    {
        // Converte a posição do mouse para o mundo 3D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Garantir que a posição do mouse tenha a mesma profundidade do objeto

        // Calcula a direção para o mouse
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
