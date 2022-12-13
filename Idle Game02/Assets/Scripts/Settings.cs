using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Controller;
using static Methods;

public class Settings : MonoBehaviour
{
    public static Settings settings;
    public string[] NotationNames;
    public TMP_Text[] SettingText;
    public GameObject[] SettingPanels;

    private void Awake()
    {
        if (settings == null)
        {
            settings = this;
        }
    }
    public void StartSetting()
    {
        NotationNames = new[] { "Standard", "Scientific" };
        Notation = controller.data.notation;
        SyncSetting();
    }

    public void ChangeSetting(string settingName)
    {
        var data = controller.data;

        switch (settingName)
        {
            case "Notation":
                controller.data.notation++;
                if (controller.data.notation > NotationNames.Length - 1)
                {
                    data.notation = 0;
                }
                Notation = data.notation;
                break;
        }
        SyncSetting(settingName);
    }

    public void SyncSetting(string settingName = "")
    {
        if (settingName == string.Empty)
        {
            SettingText[0].text = "Notation:\n" + NotationNames[Notation];

            switch (settingName)
            {
                case "Notation":
                    SettingText[0].text = "Notation:\n" + NotationNames[Notation];
                    break;
            }
        }
    }

    public void NavigationSettings(string location)
    {
        foreach(var panel in SettingPanels)
        {
            panel.SetActive(false);
        }

        switch(location)
        {
            case "Save":
                SettingPanels[0].SetActive(true);
                break;
            case "Main":
                SettingPanels[1].SetActive(true);
                break;
        }
    }
}
