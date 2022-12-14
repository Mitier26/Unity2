using UnityEngine;
using TMPro;
using BreakInfinity;
using static Settings;
using static UpgradeManager;

public class Controller : MonoBehaviour
{
    // unsigned 
    // public uint a;
    // public ulong b;
    // public BigInteger c;

    public static Controller controller;

    public Data data;

    [SerializeField] private TextMeshProUGUI currentPotatoText;
    [SerializeField] private TextMeshProUGUI potatoPersecondText;
    [SerializeField] private TextMeshProUGUI potatoClickPowerText;

    public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for(int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += upgradeManager.UpgradeHandlers[0].UpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }
        return total;
    }

    public BigDouble PotatoPerSecond()
    {
        BigDouble total = 0;
        for (int i = 0; i < data.productionUpgradeLevel.Count; i++)
        {
            total += upgradeManager.UpgradeHandlers[1].UpgradesBasePower[i] * (data.productionUpgradeLevel[i] + data.productionUpgradeGenerated[i]);
        }
        return total + PrestigeManager.prestigeManager.PrestigeEffect();
    }

    public BigDouble UpgradesPerSecond(int index)
    {
        return upgradeManager.UpgradeHandlers[2].UpgradesBasePower[index] * data.generatorUpgradeLevel[index];
    }

    private void Awake()
    {
        if(controller == null) controller= this;
    }

    private const string dataFileName = "PlayerData";
    public void Start()
    {
        //data = new Data();
        data = SaveSystem.SaveExists(dataFileName) ? SaveSystem.LoadData<Data>(dataFileName) : new Data();
        upgradeManager.StartUpgradeManager();

        settings.StartSetting();
    }

    public float SaveTime;

    private void Update()
    {
        currentPotatoText.text = $"{data.potatoes.Notate():F2} Potatoes";
        potatoPersecondText.text = $"{PotatoPerSecond():F2}/s";
        potatoClickPowerText.text = $"+{ClickPower():F2} Potatoes";

        data.potatoes += PotatoPerSecond() * Time.deltaTime;

        for(var i = 0; i < data.productionUpgradeLevel.Count; i++)
        {
            data.productionUpgradeGenerated[i] += UpgradesPerSecond(i) * Time.deltaTime;
        }

        SaveTime += Time.deltaTime * (1 / Time.timeScale);
        if(SaveTime >= 15)
        {
            SaveSystem.SaveData(data, dataFileName);
            Save();
            SaveTime = 0;
        }
    }

    public void Save()
    {
        SaveSystem.SaveData(data, dataFileName);
    }

    public void CreatePotato()
    {
        data.potatoes += ClickPower();
    }
}
