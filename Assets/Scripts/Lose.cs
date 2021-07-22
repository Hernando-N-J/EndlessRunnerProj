using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject loseScreen;
    public Generator generator;
    public TMPro.TextMeshProUGUI scoreText;
    public GameObject player;
    
    private void Awake()
    {
        Trap.OnTrap += TrapOnTrap;
    }

    private void TrapOnTrap(TrapType traptype)
    {
        player.SetActive(false); 
        scoreText.text = Scores.Instance.current.ToString();
        Scores.Instance.Save();
        loseScreen.SetActive(true);
        generator.enabled = false;
    }
}