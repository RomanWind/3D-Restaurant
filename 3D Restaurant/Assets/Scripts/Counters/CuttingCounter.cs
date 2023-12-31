using System;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress 
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeScriptable[] cuttingRecipeScriptableArray;
    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if(HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectScriptable()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    cuttingProgress = 0;
                    CuttingRecipeScriptable cuttingRecipeScriptable = GetCuttingRecipeScriptableWithInput(GetKitchenObject().GetKitchenObjectScriptable());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeScriptable.cuttingProgressMax
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectScriptable()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectScriptable()))
        {
            cuttingProgress++;
            OnCut?.Invoke(this, EventArgs.Empty);

            CuttingRecipeScriptable cuttingRecipeScriptable = GetCuttingRecipeScriptableWithInput(GetKitchenObject().GetKitchenObjectScriptable());
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeScriptable.cuttingProgressMax
            });

            if (cuttingProgress >= cuttingRecipeScriptable.cuttingProgressMax)
            {
                KitchenObjectScriptable outputKitchenObjectScriptable = GetOutputForInput(GetKitchenObject().GetKitchenObjectScriptable());
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(outputKitchenObjectScriptable, this);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        CuttingRecipeScriptable cuttingRecipeScriptable = GetCuttingRecipeScriptableWithInput(inputKitchenObjectScriptable);
        return cuttingRecipeScriptable != null;
    }

    private KitchenObjectScriptable GetOutputForInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        CuttingRecipeScriptable cuttingRecipeScriptable = GetCuttingRecipeScriptableWithInput(inputKitchenObjectScriptable);
        if(cuttingRecipeScriptable != null)
        {
            return cuttingRecipeScriptable.output;
        }
        return null;
    }

    private CuttingRecipeScriptable GetCuttingRecipeScriptableWithInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        foreach (CuttingRecipeScriptable cuttingRecipeScriptable in cuttingRecipeScriptableArray)
        {
            if (cuttingRecipeScriptable.input == inputKitchenObjectScriptable)
            {
                return cuttingRecipeScriptable;
            }
        }
        return null;
    }
}
