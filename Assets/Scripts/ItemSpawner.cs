using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
   

    [SerializeField] private GameObject ItemPrefab; //stores the item spawned by the spwaner (assign the item you want to spawn to it)

    //TAKE NOTE items should have several properties(I figured this out the hard way) this being, a rigidbody, the Outline script, the type script(in this case FoodType) 
    //and finally Holdable Object script - Serhistorybuff

    [SerializeField] private Transform spawnPoint; // stores the spawnPoint set this transform to your itemspawn point
    private GameObject item; //stores the item set this to the item you want to spawn

    [SerializeField] private int spawnedfood;

    //these store the amount of food avaliable
    public int burgers;
    public int milkshakes;
    public int fries;

    //this string stores the type of food needed for the spawner
    [SerializeField] private string Typeoffood;

    //THIS LINE BELOW IS CRUCIAL AS IT ALLOWS ACCESS TO THE RESOURCEMANAGER SCRIPT(which can be accessed by typing the scriptname (ResourceManager) before whatever you want to access)
    public ResourceManager ResourceManager;

    //these three functions work in tandem with the Food Maker script to increase the number of food as food is made

    public void Burger()
    {
        burgers = burgers + 1;
        Debug.Log("burgers" + burgers);
    }
    public void Fries()
    {
        fries = fries + 1;
        Debug.Log("fries" + fries);
    }
    public void Milkshake()
    {
        milkshakes = milkshakes + 1;
        Debug.Log("milkshakes" + milkshakes);
    }

    void Start()
    {
        //on start a item is spwaned and the Typeoffood used in the spawner is set
        SpawnItem();
        var holder = ItemPrefab.name;
        if (holder == "Milkshake")
        {
            Typeoffood = "milkshake";
        }
        if (holder == "Burger")
        {
            Typeoffood = "burger";
        }
        if (holder == "Fries")
        {
            Typeoffood = "fries";
        }
        //on start there is also a set of for loops that trigger to update the Resource Manager on how much of each item we start with
        for (int i = 0; i < burgers; i++)
        {
            ResourceManager.AddBurger();
        }
        for (int i = 0; i < milkshakes; i++)
        {
            ResourceManager.AddMilkshake();
        }
        for (int i = 0; i < fries; i++)
        {
            ResourceManager.AddFries();
        }
    }

    private void Update()
    {
        //this long band of if statements checks if there is no item out and then decides it shalt spawn food item if there are still food items remaning
        if (item == null)
        {
            if(Typeoffood == "milkshake")
            {
                if (milkshakes > 0)
                {
                    SpawnItem();
                    spawnedfood = spawnedfood + 1;
                    milkshakes = milkshakes - 1;
                    ResourceManager.RemoveMilkshake();
                    Debug.Log("milkshakes" + milkshakes);
                }

            }
            if (Typeoffood == "burger")
            {
                if (burgers > 0)
                {
                    SpawnItem();
                    spawnedfood = spawnedfood + 1;
                    burgers = burgers - 1;
                    ResourceManager.RemoveBurger();
                    Debug.Log("burgers" + burgers);
                }

            }
            if (Typeoffood == "fries")
            {
                if (fries > 0)
                {
                    SpawnItem();
                    spawnedfood = spawnedfood + 1;
                    fries = fries - 1;
                    ResourceManager.RemoveFries();
                    Debug.Log("fries" + fries);
                }

            }

        }

    }

    void SpawnItem()
        //This function spawns items 
    {
        GameObject obj = Instantiate(ItemPrefab, spawnPoint.position, spawnPoint.rotation);
        item = obj;
    }
}
