using UnityEngine;

public class Lose : MonoBehaviour
{
    public TMPro.TextMeshProUGUI texter;
    public int timesDead = 0;

    private void Awake()
    {
        Trap.OnTrap += TrapOnOnTrap;
        texter.text = $"Player died {timesDead} times";
    }

    private void TrapOnOnTrap(TrapType traptype)
    {
        timesDead++;
        texter.text = $"Player died {timesDead} times";
    }
}
