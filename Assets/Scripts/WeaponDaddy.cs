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
            LookAtMouse(); // Atualiza a mira para a posi��o do mouse
        }
        else
        {
            SyncRotation(); // Sincroniza a rota��o para os outros jogadores
        }
    }

    void LookAtMouse()
    {
        // Converte a posi��o do mouse para o mundo 3D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Garantir que a posi��o do mouse tenha a mesma profundidade do objeto

        // Calcula a dire��o para o mouse
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
