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
        public bool IsDone;
        public int LevelIndex;
        public string LevelName;
        public int CurrentCoinsCollected;
        public int CoinsCollected;
        public int CurrentCheckpoint;
        public int Attempts = 1;
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
        foreach (var levelName in levelNames)
        {
            if (IsValidLevelName(levelName))
            {
                int levelIndex = ExtractLevelIndex(levelName);

                LevelIndexMap[levelName] = levelIndex;

                LevelData levelData = CreateInstance<LevelData>();
                levelData.LevelName = levelName;
                levelData.LevelIndex = levelIndex;
                levelData.CurrentCheckpoint = 0;
                levelData.Attempts = 0;

                LevelsData[levelName] = levelData;
            }
            else
            {
                LevelIndexMap["MainMenu"] = -1;
            }
        }
    }

    private string[] GetAllLevelNames()
    {
        List<string> levelNames = new List<string>();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            levelNames.Add(Path.GetFileNameWithoutExtension(scenePath));
        }
        return levelNames.ToArray();
    }

    private bool IsValidLevelName(string levelName)
    {
        return Regex.IsMatch(levelName, @"^Level_\d+$");
    }

    private int ExtractLevelIndex(string levelName)
    {
        return int.Parse(levelName.Replace("Level_", ""));
    }

    public int GetLevelIndexByName(string levelName)
    {
        if (LevelIndexMap.TryGetValue(levelName, out int levelIndex))
        {
            return levelIndex;
        }
        Debug.LogWarning($"Level name '{levelName}' not found in index map!");
        return -1;
    }

    public void CoinPickUp(string levelName)
    {
        if (TryGetLevelData(levelName, out var levelData))
        {
            if (levelData.CoinsCollected < 3)
            {
                levelData.CurrentCoinsCollected++;

                if (levelData.CoinsCollected < levelData.CurrentCoinsCollected)
                {
                    levelData.CoinsCollected = levelData.CurrentCoinsCollected;
                }

                bool collectedAllCoins = levelData.CoinsCollected == 3;
                UpdateLevelData(levelName, collectedAllCoins, false, false);
            }
        }
        else
        {
            Debug.LogWarning($"Invalid level name: {levelName}");
        }
    }

    public void UpdateLevelData(string levelName, bool collectedAllCoins, bool failed, bool checkpointAdd)
    {
        if (TryGetLevelData(levelName, out var levelData))
        {
            if (collectedAllCoins)
            {
                levelData.CoinsCollected = 3;
            }

            if (failed)
            {
                levelData.Attempts++;
            }

            if (checkpointAdd)
            {
                levelData.CurrentCheckpoint++;
            }
        }
        else
        {
            Debug.LogWarning($"Invalid level name: {levelName}");
        }
    }

    public bool TryGetLevelData(string levelName, out LevelData levelData)
    {
        return LevelsData.TryGetValue(levelName, out levelData);
    }

    public LevelData GetLevelDataByName(string levelName)
    {
        if (TryGetLevelData(levelName, out var levelData))
        {
            return levelData;
        }
        Debug.LogWarning($"Level data not found for level: {levelName}");
        return null;
    }

    public void ResetLevelData(string levelName)
    {
        if (TryGetLevelData(levelName, out var levelData))
        {
            levelData.CoinsCollected = 0;
            levelData.CurrentCheckpoint = 0;
            levelData.Attempts = 0;
        }
        else
        {
            Debug.LogWarning($"Invalid level name: {levelName}");
        }
    }
}
