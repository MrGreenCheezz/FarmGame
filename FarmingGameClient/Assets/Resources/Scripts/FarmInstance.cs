
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class FarmInstance : MonoBehaviour
{
    public static FarmInstance instance;

    public PlantsList PlayersPlants;
    public PlayerData LocalPlayerData;
    public PlantsTypesList PlantsTypes;

    [SerializeField]
    private GameObject[] farmsList;

    public FarmAnyLevelScript CurrentFarmObject;

    [SerializeField]
    private GameObject overlayPrefab;
    private GameObject overlayObject;

    public delegate void MoneyChanged();
    public static event MoneyChanged OnMoneyChanged;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null)
        {
            Destroy(this);
            Debug.Log("Another farm instance already created, destroying!");
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        CreateFarm();
        CreateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CreateUI()
    {
        overlayObject = Instantiate(overlayPrefab);
    }
    public async void CreateFarm()
    {
        await GetPlayerData();
        await GetPlantsData();
        await GetPlantsTypes();

        CurrentFarmObject = Instantiate(farmsList[LocalPlayerData.farmLevel - 1]).GetComponent<FarmAnyLevelScript>();
        CurrentFarmObject.initializePlants();
    }

    public async Task<bool> GetPlayerData()
    {
        var Url = GameInstance.instance.URL + "player/playerData";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Url))
        {

            webRequest.SetRequestHeader("Authorization", "Bearer " + GameInstance.instance.JwtToken);

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
                return false;
            }
            else
            {
               
                LocalPlayerData = JsonUtility.FromJson<PlayerData>(webRequest.downloadHandler.text);
                OnMoneyChanged?.Invoke();
                return true;
            }
        }
    }
    
    public async Task<bool> HarvestPlant(int potIndex)
    {
        var Url = GameInstance.instance.URL + "plants/harvestPlant";
        string encodedPotIndex = UnityWebRequest.EscapeURL(potIndex.ToString());
        string urlWithParams = $"{Url}?potIndex={encodedPotIndex}";

        using (UnityWebRequest webRequest = new UnityWebRequest(urlWithParams, "POST"))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + GameInstance.instance.JwtToken);

            webRequest.downloadHandler = new DownloadHandlerBuffer();

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                Debug.Log("Response: " + webRequest.downloadHandler?.text);
                return false;
            }
            else
            {
                string responseText = webRequest.downloadHandler.text;
                print("Server response: " + responseText);
                var plant = CurrentFarmObject.PlantsOnFarm[potIndex];
                LocalPlayerData.money += PlantsTypes.items[plant.PlantType-1].harvestedPrice;
  
                OnMoneyChanged?.Invoke();

                return true;
            }
        }
    }

    public async Task<bool> BuyPlant(int potIndex, int plantType)
    {
        var Url = GameInstance.instance.URL + "plants/buyPlant";
        string encodedPotIndex = UnityWebRequest.EscapeURL(potIndex.ToString());
        string encodedPlantType = UnityWebRequest.EscapeURL(plantType.ToString());
        string urlWithParams = $"{Url}?potIndex={encodedPotIndex}&plantType={encodedPlantType}";

        using (UnityWebRequest webRequest = new UnityWebRequest(urlWithParams, "POST"))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + GameInstance.instance.JwtToken);

            webRequest.downloadHandler = new DownloadHandlerBuffer();

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                Debug.Log("Response: " + webRequest.downloadHandler?.text);
                return false;
            }
            else
            {
                LocalPlayerData.money -= PlantsTypes.items[plantType - 1].storePrice;
                string responseText = webRequest.downloadHandler.text;
                print("Server response: " + responseText);


                var newPlant = JsonUtility.FromJson<Plant>(responseText);
                CurrentFarmObject.PlantsOnFarm[potIndex].SetData(newPlant);
                CurrentFarmObject.PlantsOnFarm[potIndex].SetupPlant();
                OnMoneyChanged?.Invoke();

                return true;
            }
        }
    }

    public async Task<bool> DeletePlant(int potIndex)
    {
        var Url = GameInstance.instance.URL + "plants/deletePlant";
        string encodedPotIndex = UnityWebRequest.EscapeURL(potIndex.ToString());
        string urlWithParams = $"{Url}?potIndex={encodedPotIndex}";

        using (UnityWebRequest webRequest = new UnityWebRequest(urlWithParams, "POST"))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + GameInstance.instance.JwtToken);

            webRequest.downloadHandler = new DownloadHandlerBuffer();

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
                webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
                Debug.Log("Response: " + webRequest.downloadHandler?.text);
                return false;
            }
            else
            {
                string responseText = webRequest.downloadHandler.text;
                CurrentFarmObject.PlantsOnFarm[potIndex].DeletePlant();

                return true;
            }
        }
    }

    public async Task<bool> GetPlantsTypes()
    {
        var Url = GameInstance.instance.URL + "plants/plantsTypes";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Url))
        {

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError( webRequest.error);
                return false;
            }
            else
            {
                string json = "{\"items\":" + webRequest.downloadHandler.text + "}";
                PlantsTypes = JsonUtility.FromJson<PlantsTypesList>(json);
                return true;
            }
        }
    }


    public async Task<bool> GetPlantsData()
    {
        var finalUrl = GameInstance.instance.URL + "plants/allPlants";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(finalUrl))
        {

            webRequest.SetRequestHeader("Authorization", "Bearer " + GameInstance.instance.JwtToken);

           await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(webRequest.error);
                return false;
            }
            else
            {
                string json = "{\"items\":" + webRequest.downloadHandler.text + "}";
                PlayersPlants = JsonUtility.FromJson<PlantsList>(json);
                return true;
            }
        }
    }

    public void GenerateFarm()
    {

    }
}
#region SerializationClasses
[Serializable]
public class Plant
{
    public int id;
    public int ownerId;
    public DateTime initialPlantTime;
    public int growStateInSeconds;
    public int potIndex;
    public int currentGrowState;
    public int plantType;
}
[Serializable]
public class PlantsList
{
    public Plant[] items;
}

[Serializable]
public class PlayerData
{
    public int id ;
    public int playerId ;
    public int money ;
    public int farmLevel ;
    public int currentPlayerLevel ;
    public long currentPlayerXP ;
    public DateTime lastLoginTime ;
    public string lastToken ;
}
[Serializable]
public class PlantType
{
    public int id;
    public string name;
    public string description;
    public int storePrice;
    public int harvestedPrice;
    public int secondsToGrowOneState;
    public int maxGrowState;
}
[Serializable]
public class PlantsTypesList
{
    public PlantType[] items;
}

#endregion