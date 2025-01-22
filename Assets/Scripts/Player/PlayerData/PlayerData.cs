using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.IO;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
    [System.Serializable]
    public class LevelData : ScriptableObject
    {
        public int LevelIndex;
        public string LevelName;
        public int CoinsCollected;
        public int FailedAttempts;
    }

    public Dictionary<string, int> LevelIndexMap = new Dictionary<string, int>();
    public Dictionary<string, LevelData> LevelsData = new Dictionary<string, LevelData>();

    private void OnEnable()
    {
        InitializeData();
    }

    public void InitializeData()
    {
        string[] levelNames = GetAllLevelNames();
        for (int i = 0; i < levelNames.Length; i++)
        {
            if (Regex.IsMatch(levelNames[i], @"^Level_\d+$"))
            {
                int levelIndex = int.Parse(levelNames[i].Replace("Level_", ""));

                LevelIndexMap[levelNames[i]] = levelIndex;

                LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
                levelData.LevelName = levelNames[i];
                levelData.LevelIndex = levelIndex;

                LevelsData[levelNames[i]] = levelData;

            }
            else LevelIndexMap["MainMenu"] = -1;
        }
    }

    public string[] GetAllLevelNames()
    {
        List<string> levelNames = new List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            levelNames.Add(Path.GetFileNameWithoutExtension(scenePath));
        }
        return levelNames.ToArray();
    }

    public int GetLevelIndexByName(string levelName)
    {
        if (LevelIndexMap.ContainsKey(levelName))
        {
            return LevelIndexMap[levelName];
        }
        Debug.LogWarning($"Level name '{levelName}' not found in index map!");
        return -1;
    }

    public void CoinPickUp(string levelName)
    {
        if (LevelsData.ContainsKey(levelName))
        {
            LevelData levelData = LevelsData[levelName];
            if (levelData.CoinsCollected < 3)
            {
                levelData.CoinsCollected++;
                bool collectedAllCoins = levelData.CoinsCollected == 3;
                UpdateLevelData(levelName, collectedAllCoins, false);
            }
        }
        else
        {
            Debug.LogWarning($"Invalid level name: {levelName}");
        }
    }

    public void UpdateLevelData(string levelName, bool collectedAllCoins, bool failed)
    {
        if (LevelsData.ContainsKey(levelName))
        {
            LevelData levelData = LevelsData[levelName];

            if (collectedAllCoins)
            {
                levelData.CoinsCollected = 3;
            }

            if (failed)
            {
                levelData.FailedAttempts++;
            }
        }
        else
        {
            Debug.LogWarning($"Invalid level name: {levelName}");
        }
    }
    public LevelData GetLevelDataByName(string levelName)
    {
        if (LevelsData.ContainsKey(levelName))
        {
            return LevelsData[levelName];
        }
        Debug.LogWarning($"Level data not found for level: {levelName}");
        return null;
    }
}
