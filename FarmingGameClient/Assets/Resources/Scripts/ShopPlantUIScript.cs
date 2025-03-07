using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPlantUIScript : MonoBehaviour
{
    [SerializeField] private int plantType;
    [SerializeField] private int potIndex;
    [SerializeField] private GameObject plantIcon;
    [SerializeField] private TextMeshProUGUI priceText;
     
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Setup(int type, int index, int price)
    {
        plantType = type;
        potIndex = index;
        var iconPath = "PlantsIcons/" + FarmInstance.instance.PlantsTypes.items[type - 1].name;
        print(iconPath);
        plantIcon.GetComponent<RawImage>().texture = Resources.Load(iconPath) as Texture;
        plantIcon.SetActive(true);
        priceText.text = price.ToString();
    }

    public async void IconsClicked()
    {
      bool res = await FarmInstance.instance.BuyPlant(potIndex, plantType);
      if (res)
        {
            gameObject.GetComponentInParent<ShopUIScript>().ExitMenu();
        }
    }

  
}
