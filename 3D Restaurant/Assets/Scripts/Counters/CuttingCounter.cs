using UnityEngine;

public class CuttingCounter : BaseCounter 
{
    [SerializeField] private CuttingRecipeScriptable[] cuttingRecipeScriptableArray;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectScriptable()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectScriptable()))
        {
            KitchenObjectScriptable outputKitchenObjectScriptable = GetOutputForInput(GetKitchenObject().GetKitchenObjectScriptable());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectScriptable, this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        foreach (CuttingRecipeScriptable cuttingRecipeScriptable in cuttingRecipeScriptableArray)
        {
            if (cuttingRecipeScriptable.input == inputKitchenObjectScriptable)
            {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectScriptable GetOutputForInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        foreach(CuttingRecipeScriptable cuttingRecipeScriptable in cuttingRecipeScriptableArray)
        {
            if(cuttingRecipeScriptable.input == inputKitchenObjectScriptable)
            {
                return cuttingRecipeScriptable.output;
            }
        }
        return null;
    }
}
