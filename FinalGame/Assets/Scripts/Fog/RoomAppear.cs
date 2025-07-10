using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class EnemySpawnPoint
{
    public Vector2 position; // 敌人生成位置
}

public class RoomAppear : MonoBehaviour
{
    public bool isRevealed = false;
    public DoorController linkedDoor; // 拖入你想要关联的门对象

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab; // 拖入敌人预制件
    public int enemyCount = 3; // 要生成的敌人数量
    public EnemySpawnPoint[] spawnPoints; // 敌人生成位置数组

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
            SetTilemapsColor(Color.white);
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

        // Ensure tilemaps are fully visible at the end
        SetTilemapsColor(Color.white);

        // 房间显现完成后生成敌人
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
        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("敌人生成设置不完整，无法生成敌人");
            return;
        }

        // 确保不会尝试生成比可用位置更多的敌人
        int enemiesToSpawn = Mathf.Min(enemyCount, spawnPoints.Length);
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // 实例化敌人并设置位置
            Instantiate(enemyPrefab, 
                       transform.position + (Vector3)spawnPoints[i].position, 
                       Quaternion.identity, 
                       transform); // 将敌人设为房间的子对象
        }
    }
}
