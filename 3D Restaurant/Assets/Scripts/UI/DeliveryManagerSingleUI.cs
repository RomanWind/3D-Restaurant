using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    public void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeScriptable(RecipeScriptable recipeScriptable)
    {
        recipeNameText.text = recipeScriptable.recipeName;

        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectScriptable kitchenObjectScriptable in recipeScriptable.kitchenObjectScriptableList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectScriptable.sprite;
        }
    }
}
