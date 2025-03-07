using System.Transactions;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    [SerializeField]
    private GameObject uiMenuPrefab;
    [SerializeField]
    private GameObject shopMenuPrefab;

    private GameObject currentContextObject;
    private GameObject currentShopObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LeftMouseClick();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RightMouseClick();
        }
    }

    public void LeftMouseClick()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (currentContextObject != null)
        {
            Destroy(currentContextObject);
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
        {
            var ObjectScript = hit.collider.gameObject.GetComponent<IClickable>();
            if (ObjectScript == null) 
            {
                return;
            }
            ObjectScript.LeftClick();
            if(ObjectScript.GetClickType() == IClickableType.PlantPot)
            {
                if (currentShopObject != null)
                {
                    Destroy(currentShopObject);
                    currentShopObject = null;
                }
                var shopUI = Instantiate(shopMenuPrefab).GetComponent<ShopUIScript>();
                currentShopObject = shopUI.gameObject;
                shopUI.Setup(hit.collider.gameObject.GetComponent<PlantBase>().PotIndex);
            }
        }
    }
    public void RightMouseClick()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        RaycastHit hit;
        if (Physics.Raycast(Camera.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
        {
            var ObjectScript = hit.collider.gameObject.GetComponent<IClickable>();
            if (ObjectScript == null)
            {
                return;
            }
            ObjectScript.RightClick();
            if (ObjectScript.GetClickType() == IClickableType.PlantPot) return;
            if(currentContextObject != null)
            {
                Destroy(currentContextObject);
            }
            var obj = Instantiate(uiMenuPrefab);
            currentContextObject = obj;
            obj.transform.position = hit.collider.gameObject.transform.position;
            obj.GetComponent<ContextMenu>().SetMenuPosition();
            obj.GetComponent<ContextMenu>().SetPlantMenuData(hit.collider.gameObject);
        }
    }
}
