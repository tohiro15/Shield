using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public class LevelData
    {
        public int CoinsCollected = 0;
        public int FailedAttempts = 0;
    }

    public LevelData[] LevelsData;

    public void InitializeData(int totalLevels)
    {
        LevelsData = new LevelData[totalLevels];
    }

    public void CoinPickUp(int levelIndex)
    {
        if (LevelsData[levelIndex].CoinsCollected < 3)
        {
            LevelsData[levelIndex].CoinsCollected++;
            bool collectedAllCoins = LevelsData[levelIndex].CoinsCollected == 3;
            UpdateLevelData(levelIndex, collectedAllCoins, false);
        }
    }

    public void UpdateLevelData(int levelIndex, bool collectedAllCoins, bool failed)
    {
        if (collectedAllCoins)
        {
            LevelsData[levelIndex].CoinsCollected = 3;
        }

        if (failed)
        {
            LevelsData[levelIndex].FailedAttempts++;
        }
    }

    public void UpdateLevelData(int levelIndex, bool failed)
    {
        if (failed)
        {
            LevelsData[levelIndex].FailedAttempts++;
        }
    }

    public int GetLevelIndexByName(string levelName)
    {
        string[] levelOrder = new string[] { "Level_1", "Level_2", "Level_3", "Level_4", "Level_5", "Level_6" };
        for (int i = 0; i < levelOrder.Length; i++)
        {
            if (levelOrder[i] == levelName)
            {
                return i;
            }
        }

        return -1;
    }
}
