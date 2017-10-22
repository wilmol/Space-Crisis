﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using mattmc3.Common.Collections.Generic;

public class ItemSpawnManager : MonoBehaviour
{
    public GameController.PlayableScene ThisScene;
    static ItemDataBaseList inventoryItemList;
    public int[] ItemIdRange;
    public AudioClip pickUpFX;
    private List<Vector3> ItemSpawnsPositions;

    /// <summary>
    /// Spawns the given item at the given location.
    /// </summary>
    /// <param name="itemKey"></param> Items key in the database
    /// <param name="itemAudioFx"></param> Items audio pick up sound
    /// <param name="itemPos"></param> Item position
    public static void spawnItem(int itemKey, AudioClip itemAudioFx, Vector3 itemPos)
    {
        Debug.Log("Spawning item: " + itemKey);
        GameObject randomLootItemObject = (GameObject)Instantiate(inventoryItemList.itemList[itemKey].itemModel);
        PickUpItem pickUpItem = randomLootItemObject.AddComponent<PickUpItem>();
        pickUpItem.item = inventoryItemList.itemList[itemKey];
        pickUpItem.pickUpFX = itemAudioFx;
        randomLootItemObject.transform.localPosition = itemPos;
    }

    // Use this for initialization
    void Start()
    {
        // Load the item spawn positions - these are the child components with the tag "item-spawn"
        ItemSpawnsPositions = new List<Vector3>();
        foreach (Transform t in transform)
        {
            if (t.CompareTag("item-spawn"))
                ItemSpawnsPositions.Add(t.position);
        }
        // Load the item database
        inventoryItemList = (ItemDataBaseList)Resources.Load("ItemDatabase");

        Spawn();
    }

    /// <summary>
    /// Checks the GameController to see if the items have already been spawned,
    /// if they have retrieves them to ensure consistency.
    /// If not spawns the items.
    /// </summary>
    private void Spawn()
    {
        if (ItemIdRange.Length < ItemSpawnsPositions.Count)
            throw new System.Exception("Need more items to spawn in " + ThisScene.GetFileName());

        // Retrieve item Ids generated for this scene
        OrderedDictionary<int, bool> itemsToSpawn = GameController.GetItemsInScene(ThisScene);
        if (itemsToSpawn.Count < ItemSpawnsPositions.Count)
        {
            // Items not yet generated:
            RandomiseItemSpawns();
            GameController.AddGeneratedItems(ThisScene, ItemIdRange.Take(ItemSpawnsPositions.Count).ToList()); // persist
            itemsToSpawn = GameController.GetItemsInScene(ThisScene); // retrieve
        }

        // Spawn items
        int itemPosIndex = 0;
        foreach (int itemId in itemsToSpawn.Keys)
        {
            // Only spawn items not picked up
            if (!itemsToSpawn.GetValue(itemId)) // [] doesn't work with custom datastructure
            {
                spawnItem(itemId, pickUpFX, ItemSpawnsPositions[itemPosIndex++]);
            }
        }
    }

    private void RandomiseItemSpawns()
    {
        Debug.Log("Randomising items");
        System.Random r = new System.Random();
        ItemIdRange = ItemIdRange.OrderBy(x => r.Next()).ToArray();
    }
}
