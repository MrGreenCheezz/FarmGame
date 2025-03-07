using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;

public class PlantBase : MonoBehaviour, IClickable
{
    public int OwnerId;
    public DateTime InitialPlantTime;
    public int GrowStateInSeconds;
    public int PotIndex;
    public int CurrentGrowState;
    public int PlantType = 0;

    [SerializeField]
    private GameObject poofEffect;



    private GameObject myPlant;


    private GameObject plantModel;
    private GameObject growedPlant;


    void Start()
    {
    
    }


    void Update()
    {
        
    }

    public IClickableType GetClickType()
    {
        if (PlantType == 0)
        {
            return IClickableType.PlantPot;
        }
        else
        {
            return IClickableType.Plant;
        }
    }

    public async void LeftClick()
    {
        if(PlantType != 0)
        {
            if (CurrentGrowState >= FarmInstance.instance.PlantsTypes.items[PlantType - 1].maxGrowState)
            {
               bool result = await FarmInstance.instance.HarvestPlant(PotIndex);
                if (result) HarvestPlant();
            }
        }
        
    }
    public void RightClick()
    {
       
    }

    public void HarvestPlant()
    {
        if(myPlant != null)
        {
            Destroy(myPlant);
        }
        myPlant = Instantiate(plantModel);
        myPlant.transform.position = gameObject.transform.position + Vector3.up * 0.05f;
        myPlant.transform.parent = transform;
        myPlant.transform.localScale = Vector3.zero;
        CurrentGrowState = 0;
        InitialPlantTime = DateTime.Now;
        Instantiate(poofEffect, transform.position, Quaternion.identity).transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        StartCoroutine(CompleteGrowState(FarmInstance.instance.PlantsTypes.items[PlantType - 1].secondsToGrowOneState));
    }

    public void SetData(Plant plantData)
    {
        OwnerId = plantData.ownerId;
        InitialPlantTime = plantData.initialPlantTime;
        GrowStateInSeconds = plantData.growStateInSeconds;
        PotIndex = plantData.potIndex;
        CurrentGrowState = plantData.currentGrowState;
        PlantType = plantData.plantType;
    }

    public void DeletePlant()
    {
        OwnerId = 0;
        InitialPlantTime = DateTime.MinValue;
        GrowStateInSeconds = 0;
        CurrentGrowState = 0;
        PlantType = 0;
        Destroy(myPlant);
    }

    public void SetupPlant()
    {
        if(myPlant != null)
        {
            Destroy(myPlant);
        }

        plantModel = Resources.Load("PlantsModels/" + FarmInstance.instance.PlantsTypes.items[PlantType - 1].name) as GameObject;
        growedPlant = Resources.Load("GrowedPlantsModels/" + FarmInstance.instance.PlantsTypes.items[PlantType - 1].name) as GameObject;

        myPlant = Instantiate(plantModel);
        myPlant.transform.position = gameObject.transform.position + Vector3.up * 0.05f;
        myPlant.transform.parent = transform;

        if (CurrentGrowState >= FarmInstance.instance.PlantsTypes.items[PlantType - 1].maxGrowState)
        {
            SetPlantReady();
        }

        float growPercentage = CurrentGrowState / (float)FarmInstance.instance.PlantsTypes.items[PlantType - 1].maxGrowState;
        StartCoroutine(ScaleOverTime(new Vector3(growPercentage, growPercentage, growPercentage), 0.1f));
        StartCoroutine(CompleteGrowState(FarmInstance.instance.PlantsTypes.items[PlantType - 1].secondsToGrowOneState));
    }

    public IEnumerator CompleteGrowState(int waitTime)
    {
        while (true)
        {
            if(PlantType != 0 && CurrentGrowState < FarmInstance.instance.PlantsTypes.items[PlantType - 1].maxGrowState)
            {
                GrowStateInSeconds += waitTime;
                CurrentGrowState += 1;
                if (CurrentGrowState >= FarmInstance.instance.PlantsTypes.items[PlantType - 1].maxGrowState)
                {
                    SetPlantReady();
                }
                float growPercentage = CurrentGrowState / (float)FarmInstance.instance.PlantsTypes.items[PlantType - 1].maxGrowState;
                StartCoroutine(ScaleOverTime(new Vector3(growPercentage, growPercentage, growPercentage), 1.5f));
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                yield break;
            }
            
        }
    }

    public void SetPlantReady()
    {
        if(myPlant != null)
        {
            Destroy(myPlant);
        }

        myPlant = Instantiate(growedPlant);
        myPlant.transform.position = gameObject.transform.position + Vector3.up * 0.05f;
        myPlant.transform.parent = transform;
    }

    private IEnumerator ScaleOverTime(Vector3 endScale, float time)
    {
        if (myPlant == null)
        {
            yield break;
        }

        Vector3 startScale = myPlant.transform.localScale;
        float elapsed = 0f;

        while (elapsed < time)
        {
            
            myPlant.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        myPlant.transform.localScale = endScale; 
    }
}
