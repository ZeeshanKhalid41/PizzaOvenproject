using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject spritePrefab;
    public int poolSize = 10;
    public float delayBetweenSprites = 1f;
    public List<GameObject> spritePool = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject sprite = Instantiate(spritePrefab);
            sprite.SetActive(false);
            spritePool.Add(sprite);
        }

        StartCoroutine(SpawnSpritesWithDelay());
    }

    private IEnumerator SpawnSpritesWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenSprites);
            GameObject sprite = GetSpriteFromPool(Vector3.zero); // Replace Vector3.zero with your desired position
            // Customize or modify the spawned sprite as needed
        }
    }

    public GameObject GetSpriteFromPool(Vector3 position)
    {
        foreach (GameObject sprite in spritePool)
        {
            if (!sprite.activeInHierarchy)
            {
                sprite.transform.position = position;
                sprite.SetActive(true);
                return sprite;
            }
        }

        // If no inactive sprite is found in the pool, create a new one
        GameObject newSprite = Instantiate(spritePrefab);
        newSprite.transform.position = position;
        spritePool.Add(newSprite);
        return newSprite;
    }
}
