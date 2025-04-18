using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;

public class PlayFabLeaderboard : MonoBehaviour
{
    public Transform _LBTransform;
    public GameObject _LBRow;
    public GameObject[] _LBEntries;

    private void Update()
    {
        if(_LBTransform == null) 
        {
        // Verifica se a cena atual � "create game" e busca o objeto "tabela"
            GameObject tabelaObj = GameObject.Find("Tabela");
            if (tabelaObj != null)
            {
                _LBTransform = tabelaObj.transform;
            }
            else
            {
               // Debug.LogWarning("[PlayFabLeaderboard] Objeto 'tabela' n�o encontrado na cena 'create game'.");
            }
        }
    }
    public void RecuperarLeaderboard()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest
        {
            StartPosition = 0,
            StatisticName = "vitorias",
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(
            request,
            result =>
            {

                // TODO: limpar a tabela antes de fazer a rotinha de mostrar os novos resultados
                // fazer um la�o para destruir os registros, SE HOUVER registros
                for (int i = 0; i < _LBEntries.Length; i++) 
                {
                    Destroy(_LBEntries[i]);
                }
                
                // limpar a lista/array _LBEntries
                Array.Clear(_LBEntries, 0, _LBEntries.Length);

                // inicializar o array de linhas da tabela
                _LBEntries = new GameObject[result.Leaderboard.Count];

                // popula as linhas da tabela com as informa��es do playfab
                for (int i = 0; i < _LBEntries.Length; i++)
                {
                    _LBEntries[i] = Instantiate(_LBRow, _LBTransform);
                    TMP_Text[] colunas = _LBEntries[i].GetComponentsInChildren<TMP_Text>();
                    colunas[0].text = result.Leaderboard[i].Position.ToString(); // valor da posi��o do ranking
                    colunas[1].text = result.Leaderboard[i].DisplayName; // nome do player ou player id
                    colunas[2].text = result.Leaderboard[i].StatValue.ToString(); // valor do estat�stica
                }
            },
            error => 
            {
                Debug.LogError($"[PlayFab] {error.GenerateErrorReport()}");
            }
        );
    }

    public void UpdateLeaderboard()
    {
        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "vitorias",
                    Value = 1
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(
            request, 
            result =>
            {
                Debug.Log("[Playfab] Leaderboard foi atualizado!");
            },
            error => 
            {
                Debug.LogError($"[PlayFab] {error.GenerateErrorReport()}");
            }
        );
    }

    public void ShowLeaderboard()
    {

    }
}
