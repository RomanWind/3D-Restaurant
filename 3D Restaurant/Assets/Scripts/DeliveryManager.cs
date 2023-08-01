using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance {get; private set;}

    [SerializeField] private RecipeListScriptable recipeListScriptable;
    private List<RecipeScriptable> waitingRecipesScriptableList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake()
    {
        Instance = this;
        waitingRecipesScriptableList = new List<RecipeScriptable>();
    }

    private void Update()
    {
        spawnRecipeTimer  -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipesScriptableList.Count < waitingRecipesMax ) 
            {
                RecipeScriptable waitingRecipeScriptable = recipeListScriptable.recipeScriptableList[Random.Range(0, recipeListScriptable.recipeScriptableList.Count)];
                waitingRecipesScriptableList.Add(waitingRecipeScriptable);
                Debug.Log(waitingRecipeScriptable.recipeName);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipesScriptableList.Count; i++)
        {
            RecipeScriptable waitingRecipeScriptable = waitingRecipesScriptableList[i];

            if(waitingRecipeScriptable.kitchenObjectScriptableList.Count == plateKitchenObject.GetKitchenObjectScriptables().Count)
            {
                bool plateContentMatchesRecipe = true;
                foreach(KitchenObjectScriptable recipeKitchenObjectScriptable in waitingRecipeScriptable.kitchenObjectScriptableList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectScriptable plateKitchenObjectScriptable in plateKitchenObject.GetKitchenObjectScriptables())
                    {
                        if(plateKitchenObjectScriptable == recipeKitchenObjectScriptable)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if(!ingredientFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }

                if(plateContentMatchesRecipe)
                {
                    Debug.Log("CorrectRecipe");
                    waitingRecipesScriptableList.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
