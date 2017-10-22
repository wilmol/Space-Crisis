﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameController.PlayableScene sceneToLoad;
    public Sprite[] sprites = new Sprite[3];
    private List<GameObject> colliders;
    public float gracePeriod = 2f;

    // Use this for initialization
    void Start()
    {
        colliders = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGracePeriod();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[2 - colliders.Count];
        if (colliders.Count == 2 && gracePeriod < 1)
        {
            SceneManager.LoadScene(GameController.GetFileName(sceneToLoad));
        }
    }

    private void UpdateGracePeriod()
    {
        if (gracePeriod < 1)
        {
            gracePeriod = 0;
        }
        else
        {
            gracePeriod -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsValidCollider(other))
        {
            colliders.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsValidCollider(other))
        {
            colliders.Remove(other.gameObject);
        }
    }

    private bool IsValidCollider(Collider2D col)
    {
        return col.gameObject.CompareTag("Player");
    }
}
