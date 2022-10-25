using Mirror;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour
{
    [SerializeField]
    private UnityEvent onSelected = null;

    [SerializeField]
    private UnityEvent onDeselect = null;

    [SerializeField]
    private UnitMovement unitMovement = null;

    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }


#region Client
    [Client]
    public void Select()
    {
        print("unit selected");
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
