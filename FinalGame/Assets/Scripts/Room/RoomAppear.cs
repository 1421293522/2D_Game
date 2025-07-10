using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomAppear : MonoBehaviour
{
    public bool isRevealed = false;
    public DoorController linkedDoor;
    public EnemyController enemyController; // 新增敌人控制器引用
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

        if (Input.GetKey(KeyCode.K))
        {
            GameObject canvas = GameObject.Find("Death");
            Transform panelTransform = canvas.transform.Find("Panel");
            GameObject panel = panelTransform.gameObject;
            panel.SetActive(true);
        }
    }

    private IEnumerator RevealEffect()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        // 在开始显现时生成敌人
        if (enemyController != null)
        {
            enemyController.SpawnEnemies();
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            SetTilemapsColor(Color.Lerp(Color.clear, Color.white, t));
            yield return null;
        }

        SetTilemapsColor(Color.white);
    }

    private void SetTilemapsColor(Color color)
    {
        foreach (var tilemap in tilemaps)
        {
            tilemap.color = color;
        }
    }
}