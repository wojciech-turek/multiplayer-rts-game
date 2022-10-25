using System;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    [SerializeField]
    private UnityEvent onSelected = null;

    [SerializeField]
    private UnityEvent onDeselect = null;

    [SerializeField]
    private UnitMovement unitMovement = null;

    public static event Action<Unit> ServerOnUnitSpawned;

    public static event Action<Unit> ServerOnUnitDespawned;

    public static event Action<Unit> AuthoritytOnUnitSpawned;

    public static event Action<Unit> AuthoritytOnUnitDespawned;

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }


#region Server
    public override void OnStartServer()
    {
        base.OnStartServer();
        ServerOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        ServerOnUnitDespawned?.Invoke(this);
    }
#endregion



#region Client
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!isClientOnly || !hasAuthority) return;
        AuthoritytOnUnitSpawned?.Invoke(this);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        if (!isClientOnly || !hasAuthority) return;
        AuthoritytOnUnitDespawned?.Invoke(this);
    }

    [Client]
    public void Select()
    {
        if (!hasAuthority) return;
        onSelected?.Invoke();
    }

    [Client]
    public void Deselect()
    {
        if (!hasAuthority) return;
        onDeselect?.Invoke();
    }
#endregion
}
