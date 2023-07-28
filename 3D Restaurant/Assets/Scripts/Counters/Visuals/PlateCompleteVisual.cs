using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectScriptable_GameObject
    {
        public KitchenObjectScriptable kitchenObjectScriptable;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectScriptable_GameObject> kitchenObjectsOnPlate;

    private void Start()
    {
        plateKitchenObject.OnIngridientAdded += PlateKitchenObject_OnIngridientAdded;
        foreach (KitchenObjectScriptable_GameObject kitchenObjectScriptableGameObject in kitchenObjectsOnPlate)
        {
            kitchenObjectScriptableGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngridientAdded(object sender, PlateKitchenObject.OnIngridientAddedEventArgs e)
    {
        foreach(KitchenObjectScriptable_GameObject kitchenObjectScriptableGameObject in kitchenObjectsOnPlate)
        {
            if(kitchenObjectScriptableGameObject.kitchenObjectScriptable == e.kitchenObjectScriptable)
            {
                kitchenObjectScriptableGameObject.gameObject.SetActive(true);
            }
        }
    }
}
