using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject prefab;
    public Vector2 position;
}

public class RoomAppear : MonoBehaviour
{
    public bool isRevealed = false;
    public DoorController linkedDoor;
    
    [Header("Enemy Spawning")]
    public EnemySpawnInfo[] enemySpawns; // 替换原来的 spawnPoints
    
    private Tilemap[] tilemaps;

    void Start()
    {
        tilemaps = GetComponentsInChildren<Tilemap>();
        SetTilemapsColor(Color.black);
    }

    void Update()
    {
        if (!isRevealed && linkedDoor != null && linkedDoor.IsOpen)
        {
            isRevealed = true;
            StartCoroutine(RevealEffect());
        }
    }

    private IEnumerator RevealEffect()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            SetTilemapsColor(Color.Lerp(Color.black, Color.white, t));
            yield return null;
        }
        
        SetTilemapsColor(Color.white);
        SpawnEnemies();
    }

    private void SetTilemapsColor(Color color)
    {
        foreach (var tilemap in tilemaps)
        {
            tilemap.color = color;
        }
    }

    private void SpawnEnemies()
    {
        if (enemySpawns == null || enemySpawns.Length == 0)
        {
            Debug.LogWarning("没有设置敌人生成信息");
            return;
        }

        foreach (var spawnInfo in enemySpawns)
        {
            if (spawnInfo.prefab != null)
            {
                Instantiate(spawnInfo.prefab, 
                          transform.position + (Vector3)spawnInfo.position, 
                          Quaternion.identity, 
                          transform);
            }
        }
    }
}