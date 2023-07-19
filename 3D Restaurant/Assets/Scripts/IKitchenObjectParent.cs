using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();
    public bool HasKitchenObject();
    public KitchenObject GetKitchenObject();
    public void SetKitchenObject(KitchenObject _kitchenObject);
    public void ClearKitchenObject();
}
