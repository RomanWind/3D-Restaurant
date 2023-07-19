using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectScriptable kitchenObjectScriptable;

    private ClearCounter clearCounter;

    public KitchenObjectScriptable GetKitchenObjectScriptable()
    {
        return kitchenObjectScriptable;
    }

    public void SetClearCounter(ClearCounter _clearCounter)
    {
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = _clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a KitchenObject!");
        }

        _clearCounter.SetKitchenObject(this);

        transform.parent = _clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter() => clearCounter;

}
