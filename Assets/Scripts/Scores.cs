using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Scores : MonoBehaviour
{
  public ScoreList scoreList;
  public Score current;
  public static Scores Instance{ get; private set; }
  static string PATH => Application.persistentDataPath + "scores.bt";

  private void Awake()
  {
    Instance = this;
    Load();
  }

  public void Save()
  {
    scoreList.Add(current);
    File.WriteAllBytes(PATH, Encoding.UTF8.GetBytes(JsonUtility.ToJson(scoreList, false))); // Save file as bytes
    current = new Score();
  }

  public void Load()
  {
    if (!File.Exists(PATH)) Save();
    scoreList = JsonUtility.FromJson<ScoreList>(Encoding.UTF8.GetString(File.ReadAllBytes(PATH)));
  }

  // Clean singleton's memory
  private void OnDestroy() => Instance = null;
  private void OnApplicationQuit() => OnDestroy();
}

[System.Serializable]
public class Score
{
  public float time;
  public float km;
  public float gems;

  public float Compute() => Mathf.Clamp(km * (gems / 10f + 1) - time, 0f, 10000f);

  public override string ToString()
  {
    return $"Minutes: {time:0:00} --- Km: {(km / 10f):00} --- Gems:{gems:0}";
  }
}

[System.Serializable]
public class ScoreList
{
  public List<Score> scores = new List<Score>();
  private static Comparison<Score> comparisor = (s0, s1) => -s0.Compute().CompareTo(s1.Compute());

  public void Add(Score score)
  {
    scores.Add(score);
    scores.Sort(comparisor);
    if (scores.Count > 10) scores.RemoveAt(scores.Count - 1); // keep only 10 items
  }

  public override string ToString()
  {
    // loop through all scores, get its strings (public override ToString())
    // join them with a line break {a}\n__________\n{b}
    return scores.Select(s => s.km > 0 ? s.ToString() : "").Aggregate((a, b) => $"{a}\n \n{b}");
    //return scores.Select(s => s.ToString()).Aggregate((a, b) => $"{a}\n__________\n{b}");
  }
}
