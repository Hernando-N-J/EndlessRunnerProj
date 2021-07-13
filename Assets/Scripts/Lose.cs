using UnityEngine;

public class Lose : MonoBehaviour
{
    public TMPro.TextMeshProUGUI texter;
    public int timesDead = 0;

    private void Awake()
    {
        Trap.OnTrap += TrapOnOnTrap;
    }

    private void TrapOnOnTrap(TrapType traptype)
    {
        texter.text = $"Player died {timesDead} times";
    }
}
