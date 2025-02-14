﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour, ISpawns
{
    public ItemPickUps_SO[] itemDefinitions;

    private int whichToSpawn = 0;
    private int totalSpawnWeight = 0;
    private int chosen = 0;                  // to compare to whichToSpawn to determine which item to spawn (if more than 1 item in the array)

    public Rigidbody itemSpawned { get; set; }
    public ItemPickUp itemType { get; set; }
    public Renderer itemMaterial { get; set; }

    void Start()
    {
        foreach (ItemPickUps_SO ip in itemDefinitions)
        {
            totalSpawnWeight += ip.spawnChanceWeight;
        }
    }

    public void CreateSpawn()
    {
        //Spawn with weighted possibilities
        chosen = Random.Range(0, totalSpawnWeight);

        foreach (ItemPickUps_SO ip in itemDefinitions)
        {
            whichToSpawn += ip.spawnChanceWeight;
            if (whichToSpawn >= chosen)
            {
                itemSpawned = Instantiate(ip.itemSpawnObject, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

                itemMaterial = itemSpawned.GetComponent<Renderer>();
                if(itemMaterial != null)
                    itemMaterial.material = ip.itemMaterial;

                itemType = itemSpawned.GetComponent<ItemPickUp>();
                itemType.itemDefinition = ip;
                break;
            }
        }
    }
}
