using UnityEngine;

public class Lose : MonoBehaviour
{
    public GameObject loseScreen;
    public Generator generator;

    private void Awake()
    {
        Trap.OnTrap += TrapOnOnTrap;
    }

    private void TrapOnOnTrap(TrapType traptype)
    {
        loseScreen.SetActive(true);
        generator.enabled = false;
    }
}