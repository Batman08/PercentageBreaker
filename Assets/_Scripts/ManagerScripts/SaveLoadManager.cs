using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    public static void SaveScore(ScoreManager scoreManager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Create);

        ScoreData data = new ScoreData(scoreManager);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadScoreManager()
    {
        bool hasFoundExistingSaveFile = (File.Exists(Application.persistentDataPath + "/score.sav"));

        if (hasFoundExistingSaveFile)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/score.sav", FileMode.Open);

            ScoreData data = bf.Deserialize(stream) as ScoreData;

            stream.Close();
            return data.Score;
        }

        else
        {
            Debug.LogError("File does not exist");
            return new int { };
        }
    }
}

[Serializable]
public class ScoreData
{
    public int Score;

    public ScoreData(ScoreManager scoreManager)
    {
        Score = scoreManager.ScoreCount;
    }
}
