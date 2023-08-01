using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingle : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKitchenObjectScriptable(KitchenObjectScriptable kitchenObjectScriptable)
    {
        image.sprite = kitchenObjectScriptable.sprite;
    }
}
