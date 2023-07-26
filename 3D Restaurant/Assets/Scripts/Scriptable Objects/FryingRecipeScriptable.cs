using UnityEngine;

[CreateAssetMenu()]
public class FryingRecipeScriptable : ScriptableObject
{
    public KitchenObjectScriptable input;
    public KitchenObjectScriptable output;
    public int fryingTimerMax;
}
