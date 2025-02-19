using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;  // Para m�todos de lista, caso necess�rio
using UnityEngine.SceneManagement;

public class TESTETESTE : MonoBehaviourPunCallbacks
{
    // Chaves para Custom Properties
    private const string KEY_ALIVE_PLAYERS = "AlivePlayers";
    private const string KEY_WINNER = "Winner";
    [SerializeField] string Wscene;
    [SerializeField] string Lscene;

    void Start()
    {
        // Quando a cena de jogo iniciar, se eu for MasterClient, configurar a lista de vivos
        if (PhotonNetwork.IsMasterClient)
        {
            InitializeAlivePlayers();
        }
    }

    // M�todo para popular a Property 'AlivePlayers' com todos os ActorNumbers
    private void InitializeAlivePlayers()
    {
        // Coletamos todos os jogadores da sala
        Player[] playersInRoom = PhotonNetwork.PlayerList;

        // Extrair os ActorNumbers
        List<int> actorNumbers = new List<int>();
        foreach (var p in playersInRoom)
        {
            actorNumbers.Add(p.ActorNumber);
        }

        // Convertemos List<int> para int[] (Photon n�o lida diretamente com List<T> em Custom Properties)
        int[] aliveArray = actorNumbers.ToArray();

        // Armazenamos em uma Hashtable
        Hashtable roomProps = new Hashtable();
        roomProps[KEY_ALIVE_PLAYERS] = aliveArray;

        // Aplicamos na sala
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProps);

        Debug.Log("AlivePlayers inicializado no MasterClient.");
    }

    // Chamado localmente por qualquer jogador que morrer
    public void LocalPlayerDied()
    {
        // Envia RPC ao MasterClient informando que esse jogador est� morto
        photonView.RPC(nameof(NotifyDeath), RpcTarget.MasterClient, PhotonNetwork.LocalPlayer.ActorNumber);
    }

    [PunRPC]
    private void NotifyDeath(int actorNumber)
    {
        // S� o Master Client processa
        if (!PhotonNetwork.IsMasterClient) return;

        // Recupera a propriedade "AlivePlayers" atual
        var currentProps = PhotonNetwork.CurrentRoom.CustomProperties;

        if (currentProps.ContainsKey(KEY_ALIVE_PLAYERS))
        {
            int[] aliveArray = (int[])currentProps[KEY_ALIVE_PLAYERS];
            List<int> aliveList = new List<int>(aliveArray);

            // Remove o jogador morto
            if (aliveList.Contains(actorNumber))
            {
                aliveList.Remove(actorNumber);
            }

            // Atualiza a property
            int[] updatedAlive = aliveList.ToArray();
            Hashtable newProps = new Hashtable();
            newProps[KEY_ALIVE_PLAYERS] = updatedAlive;

            // Se sobrou s� 1, definimos o vencedor
            if (aliveList.Count == 1)
            {
                newProps[KEY_WINNER] = aliveList[0];  // ActorNumber do vencedor
            }

            PhotonNetwork.CurrentRoom.SetCustomProperties(newProps);
        }
    }

    // Quando as Custom Properties do quarto mudam, esse callback � chamado
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        base.OnRoomPropertiesUpdate(propertiesThatChanged);

        // Se a chave 'Winner' apareceu ou foi modificada, significa que temos um vencedor
        if (propertiesThatChanged.ContainsKey(KEY_WINNER))
        {
            int winnerActor = (int)propertiesThatChanged[KEY_WINNER];
            Debug.Log("Temos um vencedor! ActorNumber: " + winnerActor);

            // Compare com o seu ActorNumber
            if (winnerActor == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                // Eu sou o vencedor
                // Exibir tela de vit�ria ou realizar alguma a��o
                Debug.Log("VOC� VENCEU!");
                SceneManager.LoadScene(Wscene);
                PhotonNetwork.Disconnect();
            }
            else
            {
                // Exibir tela de derrota
                Debug.Log("Voc� perdeu. O vencedor foi " + winnerActor);
                SceneManager.LoadScene(Lscene);
                PhotonNetwork.Disconnect();
            }

            // Poder�amos tamb�m for�ar todos a voltarem ao menu ou trocar de cena
            // SceneManager.LoadScene("EndGameScene");
        }
    }

    // Opcional: Se um jogador abandonar a sala ou cair, MasterClient deve atualizar AlivePlayers tamb�m.
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        if (PhotonNetwork.IsMasterClient)
        {
            RemovePlayerFromAlive(otherPlayer.ActorNumber);
        }
    }

    private void RemovePlayerFromAlive(int actorNumber)
    {
        var currentProps = PhotonNetwork.CurrentRoom.CustomProperties;

        if (currentProps.ContainsKey(KEY_ALIVE_PLAYERS))
        {
            int[] aliveArray = (int[])currentProps[KEY_ALIVE_PLAYERS];
            List<int> aliveList = new List<int>(aliveArray);

            if (aliveList.Contains(actorNumber))
            {
                aliveList.Remove(actorNumber);
                Hashtable newProps = new Hashtable();
                newProps[KEY_ALIVE_PLAYERS] = aliveList.ToArray();

                // Se agora s� restar 1, declarar vencedor
                if (aliveList.Count == 1)
                {
                    newProps[KEY_WINNER] = aliveList[0];
                }
                PhotonNetwork.CurrentRoom.SetCustomProperties(newProps);
            }
        }
    }
}
