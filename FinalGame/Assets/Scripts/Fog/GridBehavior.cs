using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBehavior : MonoBehaviour
{
    private Tilemap fogMap;
    [SerializeField] private float fadeSpeed = 2f;
    private bool isFading = false;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject fogMapObject = GameObject.Find("Fog");
        if (fogMapObject != null)
        {
            fogMap = fogMapObject.GetComponent<Tilemap>();
            if (fogMap == null)
            {
                Debug.LogError("Tilemap");
            }
        }
        else
        {
            Debug.LogError("FogMap");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isFading)
        {
            StartCoroutine(FadeOutFogMap());
        }
    }
    
    private IEnumerator FadeOutFogMap()
    {
        if (fogMap != null)
        {
            isFading = true;
            Color startColor = fogMap.color;
            float startAlpha = startColor.a;
            float elapsedTime = 0f;
            
            while (elapsedTime < fadeSpeed)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(startAlpha, 0f, elapsedTime / fadeSpeed);
                fogMap.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
                yield return null;
            }
            
            fogMap.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
            fogMap.gameObject.SetActive(false);
            isFading = false;
            Debug.Log("FogMap is disappearing");
        }
    }
}
