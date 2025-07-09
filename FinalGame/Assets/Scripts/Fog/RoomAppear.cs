using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomAppear : MonoBehaviour
{
    public bool isRevealed = false;

    private Tilemap[] tilemaps;

    void Start()
    {
        tilemaps = GetComponentsInChildren<Tilemap>();
        SetTilemapsColor(Color.black);
    }

    void Update()
    {
        if (!isRevealed && Input.GetKeyDown(KeyCode.R))
        {
            isRevealed = true;
            SetTilemapsColor(Color.white);
            StartCoroutine(RevealEffect());
        }
        //Debug.Log("Update Called");
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
        
        // Ensure tilemaps are fully visible at the end
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
