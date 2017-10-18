﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class ScenePersistence : MonoBehaviour {

    private static ScenePersistence instance;
    private static GameController gameController;
    private static readonly string[] playableScenes = new string[]{ "level1room1", "level1room2", "level1room3" };

    public void Awake()
    {
        gameController = GameController.GetInstance();
        // Save positions of all objects in all scenes
        SaveScenes();
    }

    private ScenePersistence() { }

    public static ScenePersistence GetInstance()
    {
        if (instance == null)
            instance = new ScenePersistence();
        return instance;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SaveScenes();
    }

    private void SaveScenes()
    {
        foreach (string playableScene in playableScenes)
        {
            Scene scene = SceneManager.GetSceneByName(playableScene);
            GameObject[] objects = scene.GetRootGameObjects();
            gameController.SaveObjectsFor(playableScene, scene.GetRootGameObjects());
        }
    }

    public void RestoreScene(string scene)
    {
        GameObject[] savedObjects = gameController.GetObjectsFor(scene);
        GameObject[] currentObjects = SceneManager.GetSceneByName(scene).GetRootGameObjects();
       
        for (int i = 0; i < currentObjects.Length; i++)
        {
            currentObjects[i].transform.position = savedObjects[i].transform.position;
        }
    }

    /*
        p1 = GameObject.Find("Astronaut");
        p2 = GameObject.Find("Astronaut_2");
        loc = GameObject.Find("PositionBack").transform;
        p1.transform.position = loc.position;
        p2.transform.position = loc.position;
        Inventory inv = p1.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
        List<Item> l = GameController.GetInstance().GetListOfItems();
        l.ForEach(i => {
            Debug.Log("HAS ITEM " + i);
            inv.addItemToInventory(i.itemID, i.itemValue);
            inv.updateItemList();
            inv.stackableSettings();
        });
*/
}