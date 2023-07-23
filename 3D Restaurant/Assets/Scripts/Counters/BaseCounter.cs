using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {
        Debug.LogError("BaseCounter.Interract();");
    }
    public virtual void InteractAlternate(Player player)
    {
        Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;
    public bool HasKitchenObject() => kitchenObject != null;
    public KitchenObject GetKitchenObject() => kitchenObject;
    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        kitchenObject = _kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
