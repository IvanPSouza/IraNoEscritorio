using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabLeaderboard : MonoBehaviour
{
    public Transform _LBTransform;
    public GameObject _LBRow;
    public GameObject[] _LBEntries;

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
                // fazer um laço para destruir os registros, SE HOUVER registros
                for (int i = 0; i < _LBEntries.Length; i++) 
                {
                    Destroy(_LBEntries[i]);
                }
                
                // limpar a lista/array _LBEntries
                Array.Clear(_LBEntries, 0, _LBEntries.Length);

                // inicializar o array de linhas da tabela
                _LBEntries = new GameObject[result.Leaderboard.Count];

                // popula as linhas da tabela com as informações do playfab
                for (int i = 0; i < _LBEntries.Length; i++)
                {
                    _LBEntries[i] = Instantiate(_LBRow, _LBTransform);
                    TMP_Text[] colunas = _LBEntries[i].GetComponentsInChildren<TMP_Text>();
                    colunas[0].text = result.Leaderboard[i].Position.ToString(); // valor da posição do ranking
                    colunas[1].text = result.Leaderboard[i].DisplayName; // nome do player ou player id
                    colunas[2].text = result.Leaderboard[i].StatValue.ToString(); // valor do estatística
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
