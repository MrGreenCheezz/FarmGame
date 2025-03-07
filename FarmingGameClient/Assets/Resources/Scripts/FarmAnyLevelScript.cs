
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FarmAnyLevelScript : MonoBehaviour
{
    public List<PlantBase> PlantsOnFarm = new List<PlantBase>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void initializePlants()
    {
        for(int i = 0; i < FarmInstance.instance.PlayersPlants.items.Count(); i++)
        {
            var plantData = FarmInstance.instance.PlayersPlants.items[i];
            var currentPlantIndex = plantData.potIndex;
            PlantsOnFarm[currentPlantIndex].OwnerId = plantData.ownerId;
            PlantsOnFarm[currentPlantIndex].InitialPlantTime = plantData.initialPlantTime;
            PlantsOnFarm[currentPlantIndex].GrowStateInSeconds = plantData.growStateInSeconds;
            PlantsOnFarm[currentPlantIndex].CurrentGrowState = plantData.currentGrowState;
            PlantsOnFarm[currentPlantIndex].PlantType = plantData.plantType;
            PlantsOnFarm[currentPlantIndex].PotIndex = plantData.potIndex;
            PlantsOnFarm[currentPlantIndex].SetupPlant();
        }
    }
}
