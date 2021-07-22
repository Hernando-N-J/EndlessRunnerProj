using UnityEngine;

public class ScoresGUI : MonoBehaviour
{
  public TMPro.TextMeshProUGUI scoreText;

  private void OnEnable()
  {
    Scores.Instance.Load();
    scoreText.text = Scores.Instance.scoreList.ToString();
  }

}
