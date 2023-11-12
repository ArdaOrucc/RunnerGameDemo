using UnityEngine;

public static class DataHandler
{
    public const string LevelIndexKey = "LevelIndexKey";
    public const string CurrencyAmountKey = "CurrencyAmountKey";

    public static int LevelIndex
    {
        get => PlayerPrefs.GetInt(LevelIndexKey, 0);
        set => PlayerPrefs.SetInt(LevelIndexKey, value);
    }

    public static int CurrencyAmount
    {
        get => PlayerPrefs.GetInt(CurrencyAmountKey, 20);
        set => PlayerPrefs.SetInt(CurrencyAmountKey, value);
    }
    
    public static void Save(object data, string key)
    {
        var jsonData = JsonUtility.ToJson(data);
        
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
        
        Debug.Log($"<color=green> Save complete for {data} with key {key} </color>");
    }
    
    public static T Load<T>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            var jsonData = PlayerPrefs.GetString(key);
            var data = JsonUtility.FromJson<T>(jsonData);

            if (data != null)
            {
                Debug.Log($"<color=green> Load complete for {data} with key {key} </color>");
            }
            else
            {
                Debug.LogWarning($"<color=yellow> Failed to load data with key {key} </color>");
            }

            return data;
        }
        else
        {
            Debug.LogWarning($"<color=red> No save data found with key {key} </color>");
            return default(T);
        }
    }

    public static void Clear(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
            
            Debug.Log($"<color=green> Clear data with key {key} is completed successfully </color>");
        }
        else
        {
            Debug.LogWarning($"<color=yellow> No save data found with key {key} to clear! </color>");
        }
    }

    public static void ClearAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public static string GetJsonData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            var dataJson = PlayerPrefs.GetString(key);
            Debug.Log($"<color=green> Data json with key {key} returned </color>");
            return dataJson;
        }
        else
        {
            Debug.LogWarning($"<color=yellow> No data found with key {key}, no json data returned </color>");
            return null;
        }
    }
}
