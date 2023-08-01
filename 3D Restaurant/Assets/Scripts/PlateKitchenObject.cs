using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngridientAddedEventArgs> OnIngridientAdded;
    public class OnIngridientAddedEventArgs : EventArgs
    {
        public KitchenObjectScriptable kitchenObjectScriptable;
    }

    [SerializeField] private List<KitchenObjectScriptable> validScriptableKitchenObjects;
    private List<KitchenObjectScriptable> kitchenObjectsOnPlate;

    private void Awake()
    {
        kitchenObjectsOnPlate = new List<KitchenObjectScriptable>();
    }

    public bool TryAddIngridient(KitchenObjectScriptable kitchenObjectScriptable)
    {
        if(!validScriptableKitchenObjects.Contains(kitchenObjectScriptable))
        { 
            return false;
        }
        if(kitchenObjectsOnPlate.Contains(kitchenObjectScriptable))
        {
            return false;
        }
        else
        {
            kitchenObjectsOnPlate.Add(kitchenObjectScriptable);
            OnIngridientAdded?.Invoke(this, new OnIngridientAddedEventArgs
            {
                kitchenObjectScriptable = kitchenObjectScriptable
            });
            return true;
        }
    }

    public List<KitchenObjectScriptable> GetKitchenObjectScriptables() => kitchenObjectsOnPlate;
}
