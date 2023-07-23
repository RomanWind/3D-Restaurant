using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabObject;

    [SerializeField] private KitchenObjectScriptable kitchenObjectScriptable;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectScriptable, player);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
    }
}
