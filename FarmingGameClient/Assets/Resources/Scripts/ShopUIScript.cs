using TMPro;
using UnityEngine;

public class ShopUIScript : MonoBehaviour
{

    [SerializeField] private int potIndex;
    [SerializeField] private GameObject contentLink;
    [SerializeField] private GameObject contentPrefab;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Setup(int index)
    {
        potIndex = index;   
        foreach(var item in FarmInstance.instance.PlantsTypes.items)
        {
            var tmpObj = Instantiate(contentPrefab, contentLink.transform);
            tmpObj.GetComponent<ShopPlantUIScript>().Setup(item.id, potIndex, item.storePrice);
        }
        upgradeCostText.text = (FarmInstance.instance.LocalPlayerData.baseFarmCost * FarmInstance.instance.LocalPlayerData.farmLevel).ToString();
    }

    public async  void UpgradeFarm()
    {
        bool updated = await FarmInstance.instance.UpgradeFarm();
        if (updated)
        {
            ExitMenu();
            FarmInstance.instance.ChangeFarmLevel();
        }
    }

    public void ExitMenu()
    {
        Destroy(gameObject);
    }

}
