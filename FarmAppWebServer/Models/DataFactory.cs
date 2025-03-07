namespace FarmAppWebServer.Models
{
    public class DataFactory
    {
      
        public static UserRegistrationInfo CreateUserRegistration(string email, string username, string password)
        {
            var newUser = new UserRegistrationInfo()
            {
                Email = email,
                Name = username,
                PasswordEncrypted = password
            };
            return newUser;
        }

        public static PlayerData CreatePlayerData(int ownerId, int money = 0, int farmLevel = 1, int currentPlayerLevel = 1, int currentPlayerXp = 0, DateTime loginDate = default(DateTime), string lastToken = "none")
        {
            var playerData = new PlayerData()
            {
                PlayerId = ownerId,
                Money = money,
                FarmLevel = farmLevel,
                CurrentPlayerLevel = currentPlayerLevel,
                CurrentPlayerXP = currentPlayerXp,
                LastLoginTime = loginDate,
                LastToken = lastToken
            };
            return playerData;
        }

        public static PlayersPlantsInstance CreatePlayerPlantInstace(int ownerId, int potIndex, DateTime plantTime = default(DateTime), int growState = 0,  int currentGrowState = 0, int plantType = 1)
        {
            var plant = new PlayersPlantsInstance()
            {
                OwnerId = ownerId,
                LastClientInteraction = plantTime,
                GrowStateInSeconds = growState,
                PotIndex = potIndex,
                CurrentGrowState = currentGrowState,
                PlantType = plantType
            };
            return plant;
        }
    }
}
