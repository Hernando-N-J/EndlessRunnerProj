using UnityEngine;

public class Gem : MonoBehaviour
{
  public AudioClip clip;
  
  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      Scores.Instance.current.gems++;
      AudioHolder.Instance.Play(clip);
      gameObject.SetActive(false);
    }
  }
}
