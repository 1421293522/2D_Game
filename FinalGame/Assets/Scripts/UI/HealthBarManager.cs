using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public HeroBehavior hero; // 需要手动拖拽Hero对象到Inspector中
    public Image healthBarImage; // UI Image组件，用于显示血条

    private Sprite[] healthBarSprites; // 存储血条图片的数组
    private int lastHealth = -1; // 用于检测血量变化的标记

    void Start()
    {
        // 加载血条图片资源
        healthBarSprites = Resources.LoadAll<Sprite>("UI/HealthBar");

        if (healthBarSprites == null || healthBarSprites.Length != 27)
        {
            Debug.LogError("血条资源加载失败！请检查路径和图片数量");
        }
    }

    void Update()
    {
        if (hero == null) return;

        int currentHealth = hero.getHealth();

        // 如果血量没变化则跳过更新
        if (currentHealth == lastHealth) return;

        lastHealth = currentHealth;
        UpdateHealthBar(currentHealth);
    }

    private void UpdateHealthBar(int health)
    {
        // 将血量百分比转化为图片索引 (0-100 -> 0-26)
        float healthPercent = Mathf.Clamp01(health / 100f);
        int spriteIndex = Mathf.FloorToInt(26 * (1 - healthPercent)); // 反转索引：满血为0号图片，空血为26号

        // 确保索引在有效范围内
        spriteIndex = Mathf.Clamp(spriteIndex, 0, 26);

        // 更新血条图片
        if (healthBarSprites != null && spriteIndex < healthBarSprites.Length)
        {
            healthBarImage.sprite = healthBarSprites[spriteIndex];
        }
    }
}