using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayFabManager_2 : MonoBehaviour
{
    public TMP_InputField Input_1, Input_2, Input_3;
    public TMP_Text StateText;

    public void SetState()
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                // PlayFab�� Input_1 �̸��� �����Ϳ� ���� �����Ѵ�.
                new StatisticUpdate{ StatisticName = "Input_1", Value = int.Parse(Input_1.text)},
                new StatisticUpdate{ StatisticName = "Input_2", Value = int.Parse(Input_2.text)},
                new StatisticUpdate{ StatisticName = "Input_3", Value = int.Parse(Input_3.text)}
            },

        },
            (result) => { StateText.text = "�� �����"; },
            (error) => { StateText.text = "�� �������"; });
    }

    public void GetState()
    {
        PlayFabClientAPI.GetPlayerStatistics(
            new GetPlayerStatisticsRequest(),
            (result) => // Callback() ����
            {
                StateText.text = "";
                foreach (var eachStat in result.Statistics)
                {
                    StateText.text += eachStat.StatisticName + " : " + eachStat.Value + "\n";
                }
            },
            (error) =>
            {
                StateText.text = "�� �ҷ����� ����";
            });
    }

    private void Callback()
    {

    }
}
