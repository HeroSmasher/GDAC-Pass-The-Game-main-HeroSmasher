using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    [SerializeField] private GameObject grill;
    [SerializeField] private GameObject fryer;
    [SerializeField] private GameObject mixer;

    public TMP_Text pattiesText;
    public TMP_Text potatoesText;
    public TMP_Text icecreamText;
    public TMP_Text burgerText;
    public TMP_Text friesText;
    public TMP_Text milkshakeText;

    
    public int patties = 0;
    public int potatoes = 0;
    public int icecream = 0;
    public int burgers = 0;
    public int fries = 0;
    public int milkshakes = 0;

    private void Awake()
    {
        instance = this;
    }

    // -20 offset is necessary due to unintended behavior adding 20 to the visual representation of each ingredient. subtracting 20 gives us the accurate number.
    void Start()
    {
        patties -= 20;
        potatoes -= 20;
        icecream -= 20;
    }

    // Update is called once per frame
    public void AddPatty()
    {
        patties++;
        pattiesText.text = patties.ToString();
    }
    public void RemovePatty()
    {
        patties--;
        pattiesText.text = patties.ToString();
    }
    public void AddPotato()
    {
        potatoes++;
        potatoesText.text = potatoes.ToString();
    }
    public void RemovePotato() 
    { 
        potatoes--;
        potatoesText.text = potatoes.ToString();
    }
    public void AddIcecream()
    {
        icecream++;
        icecreamText.text = icecream.ToString();
    }
    public void RemoveIcecream()
    {
        icecream--;
        icecreamText.text = icecream.ToString();
    }
    public void AddBurger()
    {
        burgers++;
        burgerText.text = burgers.ToString();
    }
    public void RemoveBurger()
    {
        burgers--;
        burgerText.text = burgers.ToString();
    }
    public void AddFries()
    {
        fries++;
        friesText.text = fries.ToString();
    }
    public void RemoveFries()
    {
        fries--;
        friesText.text = fries.ToString();
    }
    public void AddMilkshake()
    {
        milkshakes++;
        milkshakeText.text = milkshakes.ToString();
    }
    public void RemoveMilkshake()
    {
        milkshakes--;
        milkshakeText.text = milkshakes.ToString();
    }
}
