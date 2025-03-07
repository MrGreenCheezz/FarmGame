using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ContextMenu : MonoBehaviour
{
    [SerializeField] private RectTransform targetObject;
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private TextMeshProUGUI plantNameUI;
    [SerializeField] private TextMeshProUGUI statusUI;
    [SerializeField] private GameObject deleteButton;

    private int _plantType;
    private string _plantName;
    private int _currentGrowState;
    private int _maxGrowState;

    private PlantBase plantScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPlantMenuData(GameObject plantBase)
    {
        plantScript = plantBase.GetComponent<PlantBase>();
        _plantType = plantScript.PlantType;
        _plantName = FarmInstance.instance.PlantsTypes.items[_plantType - 1].name;
        _currentGrowState = plantScript.CurrentGrowState;
        _maxGrowState = FarmInstance.instance.PlantsTypes.items[_plantType - 1].maxGrowState;
        plantNameUI.text = _plantName;
        statusUI.text = _currentGrowState + "/" + _maxGrowState;
       
    }

    public async void DeletePlant()
    {
        await  FarmInstance.instance.DeletePlant(plantScript.PotIndex);
        Destroy(gameObject);
    }

    public void SetMenuPosition()
    {
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition,
            parentCanvas.worldCamera,
            out localPosition);


        targetObject.anchoredPosition = localPosition + new Vector2(55,55);
    }
}
