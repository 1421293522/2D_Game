using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public HeroBehavior hero; // ��Ҫ�ֶ���קHero����Inspector��
    public Image healthBarImage; // UI Image�����������ʾѪ��

    private Sprite[] healthBarSprites; // �洢Ѫ��ͼƬ������
    private int lastHealth = -1; // ���ڼ��Ѫ���仯�ı��

    void Start()
    {
        // ����Ѫ��ͼƬ��Դ
        healthBarSprites = Resources.LoadAll<Sprite>("UI/HealthBar");

        if (healthBarSprites == null || healthBarSprites.Length != 27)
        {
            Debug.LogError("Ѫ����Դ����ʧ�ܣ�����·����ͼƬ����");
        }
    }

    void Update()
    {
        if (hero == null) return;

        int currentHealth = hero.getHealth();

        // ���Ѫ��û�仯����������
        if (currentHealth == lastHealth) return;

        lastHealth = currentHealth;
        UpdateHealthBar(currentHealth);
    }

    private void UpdateHealthBar(int health)
    {
        // ��Ѫ���ٷֱ�ת��ΪͼƬ���� (0-100 -> 0-26)
        float healthPercent = Mathf.Clamp01(health / 100f);
        int spriteIndex = Mathf.FloorToInt(26 * (1 - healthPercent)); // ��ת��������ѪΪ0��ͼƬ����ѪΪ26��

        // ȷ����������Ч��Χ��
        spriteIndex = Mathf.Clamp(spriteIndex, 0, 26);

        // ����Ѫ��ͼƬ
        if (healthBarSprites != null && spriteIndex < healthBarSprites.Length)
        {
            healthBarImage.sprite = healthBarSprites[spriteIndex];
        }
    }
}