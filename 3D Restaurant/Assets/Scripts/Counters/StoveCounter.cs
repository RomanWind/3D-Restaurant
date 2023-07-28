using UnityEngine;
using System;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeScriptable[] fryingRecipesScriptableArray;
    [SerializeField] private BurningRecipeScriptable[] burningRecipesScriptableArray;
    private FryingRecipeScriptable fryingRecipeScriptable;
    private BurningRecipeScriptable burningRecipeScriptable;
    private float fryingTimer;
    private float burningTimer;
    private State state;

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeScriptable.fryingTimerMax
                    });
                    if (fryingTimer > fryingRecipeScriptable.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeScriptable.output, this);
                        burningRecipeScriptable = GetBurningRecipeScriptableWithInput(GetKitchenObject().GetKitchenObjectScriptable());
                        burningTimer = 0f;
                        state = State.Fried;
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = burningTimer / burningRecipeScriptable.burningTimerMax
                    });
                    if (burningTimer > burningRecipeScriptable.burningTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeScriptable.output, this);
                        state = State.Burned;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectScriptable()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeScriptable = GetFryingRecipeScriptableWithInput(GetKitchenObject().GetKitchenObjectScriptable());
                    fryingTimer = 0f;
                    state = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = fryingTimer / fryingRecipeScriptable.fryingTimerMax
                    });
                }
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
            else
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngridient(GetKitchenObject().GetKitchenObjectScriptable()))
                    {
                        GetKitchenObject().DestroySelf();

                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        FryingRecipeScriptable fryingRecipeScriptable = GetFryingRecipeScriptableWithInput(inputKitchenObjectScriptable);
        return fryingRecipeScriptable != null;
    }

    private KitchenObjectScriptable GetOutputForInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        FryingRecipeScriptable fryingRecipeScriptable = GetFryingRecipeScriptableWithInput(inputKitchenObjectScriptable);
        if (fryingRecipeScriptable != null)
        {
            return fryingRecipeScriptable.output;
        }
        return null;
    }

    private FryingRecipeScriptable GetFryingRecipeScriptableWithInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        foreach (FryingRecipeScriptable fryingRecipeScriptable in fryingRecipesScriptableArray)
        {
            if (fryingRecipeScriptable.input == inputKitchenObjectScriptable)
            {
                return fryingRecipeScriptable;
            }
        }
        return null;
    }

    private BurningRecipeScriptable GetBurningRecipeScriptableWithInput(KitchenObjectScriptable inputKitchenObjectScriptable)
    {
        foreach (BurningRecipeScriptable burningRecipeScriptable in burningRecipesScriptableArray)
        {
            if (burningRecipeScriptable.input == inputKitchenObjectScriptable)
            {
                return burningRecipeScriptable;
            }
        }
        return null;
    }
}
