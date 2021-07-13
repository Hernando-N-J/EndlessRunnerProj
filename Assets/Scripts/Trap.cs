using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public static event TrapEvent OnTrap;
    public TrapType trapType = TrapType.Impact;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnTrap?.Invoke(trapType);
    }

    private void OnDestroy()
    {
        OnTrap = null;
    }
}

public delegate void TrapEvent(TrapType trapType);

public enum TrapType
{
    Impact, Water
}
