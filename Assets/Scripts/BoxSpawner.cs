using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    //This is adapted from the Itemspawner script

    [SerializeField] private GameObject BoxPrefab;//stores the Box spawned by the spwaner (assign the item you want to spawn to it)
    [SerializeField] private Transform spawnPointbox;  // stores the spawnPoint set this transform to your itemspawn point


    private GameObject box; //stores the box set this to the item you want to spawn 

//TAKE NOTE items should have several properties(I figured this out the hard way) this being, a rigidbody, the Outline script, the type script(in this case BoxType) 
//and finally Holdable Object script - Serhistorybuff

    void Start()
    {
        //a box will spawn on start
        SpawnBox();
    }

    private void Update()
    {
        //boxes will spawn when there is no box in the world(barring those on spwaners)
        if (box == null)
        {
            SpawnBox();
        }
    }

    void SpawnBox()
    {
        //this spawns a box
        GameObject obj = Instantiate(BoxPrefab, spawnPointbox.position, spawnPointbox.rotation);
        box = obj;
    }
}
