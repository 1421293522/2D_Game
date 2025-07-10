using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab; // 敌人预制件
    public int spawnCount;        // 生成数量
}

public class RoomAppear : MonoBehaviour
{
    public bool isRevealed = false;
    public DoorController linkedDoor;

    [Header("Enemy Spawn Settings")]
    public Vector2 spawnAreaCorner1; // 矩形区域第一个角点
    public Vector2 spawnAreaCorner2; // 矩形区域对角点
    public List<EnemySpawnInfo> enemiesToSpawn = new List<EnemySpawnInfo>();
    private Tilemap[] tilemaps;

    void Start()
    {
        tilemaps = GetComponentsInChildren<Tilemap>();
        SetTilemapsColor(Color.clear);
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

            SetTilemapsColor(Color.Lerp(Color.clear, Color.white, t));
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
        foreach (var enemyInfo in enemiesToSpawn)
        {
            for (int i = 0; i < enemyInfo.spawnCount; i++)
            {
                // 计算随机位置
                Vector2 spawnPos = new Vector2(
                    Random.Range(Mathf.Min(spawnAreaCorner1.x, spawnAreaCorner2.x), 
                               Mathf.Max(spawnAreaCorner1.x, spawnAreaCorner2.x)),
                    Random.Range(Mathf.Min(spawnAreaCorner1.y, spawnAreaCorner2.y), 
                               Mathf.Max(spawnAreaCorner1.y, spawnAreaCorner2.y))
                );
                
                // 实例化敌人
                Instantiate(enemyInfo.enemyPrefab, spawnPos, Quaternion.identity, transform);
            }
        }
    }

    // 在场景视图中绘制生成区域（仅用于编辑器调试）
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 center = (spawnAreaCorner1 + spawnAreaCorner2) / 2;
        Vector3 size = new Vector3(
            Mathf.Abs(spawnAreaCorner1.x - spawnAreaCorner2.x),
            Mathf.Abs(spawnAreaCorner1.y - spawnAreaCorner2.y),
            0.1f
        );
        Gizmos.DrawWireCube(center, size);
    }
}