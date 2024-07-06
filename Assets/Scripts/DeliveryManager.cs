using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour

    //I didn't have enough time to fully write notes out so feel free to ask me about this thing - Serhistorybuff
{
    public FoodMaker FoodMaker;

    // Start is called before the first frame update
    void Start()
    {

    }
    public bool PlayerNear { get; private set; } = false;
    [SerializeField] private SphereCollider triggerCollider;

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        if (other.tag == "Player")
        {
            PlayerNear = true;
        }
        else if (other.tag == "Box")
        {
            Debug.Log("Box Entered" + other.name);
            BoxType box = other.GetComponent<BoxType>();
            Destroy(other.gameObject);
            FoodMaker.Restock();
        }
    }
}


