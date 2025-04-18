using UnityEngine;
using System.Collections.Generic;

public class InventoryGridSpawner : MonoBehaviour
{
    [System.Serializable]
    public class TestSpawn
    {
        public InventoryItemSO itemSO;
        public int attempts = 10; // How many tries to find a valid position
    }

    public List<TestSpawn> testItems = new List<TestSpawn>();

    private void Start()
    {
        foreach (var spawn in testItems)
        {
            for (int i = 0; i < spawn.attempts; i++)
            {
                Vector2Int randomPos = new Vector2Int(
                    Random.Range(0, InventoryGridUI.Instance.width),
                    Random.Range(0, InventoryGridUI.Instance.height)
                );

                if (InventoryGridUI.Instance.CanPlaceItem(spawn.itemSO, randomPos))
                {
                    InventoryGridUI.Instance.SpawnItem(spawn.itemSO, randomPos);
                    Debug.Log($"✅ Spawned {spawn.itemSO.name} at {randomPos}");
                    break;
                }
            }
        }
    }
}
