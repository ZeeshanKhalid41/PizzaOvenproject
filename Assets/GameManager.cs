using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PoolManager poolManager;
    public Transform spawnPosition;

    private void Start()
    {
        GameObject sprite = poolManager.GetSpriteFromPool(spawnPosition.position);
    }
}
