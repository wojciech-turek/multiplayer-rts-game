using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    public event Action ServerOnDie;


#region Server

    public override void OnStartServer()
    {
        base.OnStartServer();
        currentHealth = maxHealth;
    }

    [Server]
    public void DealDamage(int damage)
    {
        if (currentHealth == 0) return;
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if (currentHealth != 0) return;

        ServerOnDie?.Invoke();

        Debug.Log("We died");
    }


#endregion


#region Client
#endregion
}
