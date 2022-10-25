using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Networking;

public class RTSPlayer : NetworkBehaviour
{
    [SerializeField]
    private List<Unit> myUnits = new List<Unit>();

    public List<Unit> GetUnits()
    {
        return myUnits;
    }


#region Server
    public override void OnStartServer()
    {
        base.OnStartServer();
        Unit.ServerOnUnitSpawned += ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned += ServerHandleUnitDespawned;
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        Unit.ServerOnUnitSpawned -= ServerHandleUnitSpawned;
        Unit.ServerOnUnitDespawned -= ServerHandleUnitDespawned;
    }

    private void ServerHandleUnitSpawned(Unit unit)
    {
        if (
            unit.connectionToClient.connectionId !=
            connectionToClient.connectionId
        ) return;
        myUnits.Add (unit);
    }

    private void ServerHandleUnitDespawned(Unit unit)
    {
        if (
            unit.connectionToClient.connectionId !=
            connectionToClient.connectionId
        ) return;
        myUnits.Remove (unit);
    }
#endregion



#region Client

    public override void OnStartClient()
    {
        if (!isClientOnly) return;
        base.OnStartClient();
        Unit.AuthoritytOnUnitSpawned += AuthorityHandleUnitSpawned;
        Unit.AuthoritytOnUnitDespawned += AuthorityHandleUnitDespawned;
    }

    public override void OnStopClient()
    {
        if (!isClientOnly) return;
        base.OnStopClient();
        Unit.AuthoritytOnUnitSpawned -= AuthorityHandleUnitSpawned;
        Unit.AuthoritytOnUnitDespawned -= AuthorityHandleUnitDespawned;
    }

    private void AuthorityHandleUnitDespawned(Unit unit)
    {
        if (!hasAuthority) return;
        myUnits.Add (unit);
    }

    private void AuthorityHandleUnitSpawned(Unit unit)
    {
        if (!hasAuthority) return;
        myUnits.Remove (unit);
    }
#endregion
}
