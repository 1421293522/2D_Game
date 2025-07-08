using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyBasic : MonoBehaviour
{
    private Animator anim;
    public GameObject bulletPrefab; // 子弹预制体
    public Transform hero; // 英雄的Transform
    private float shootTimer = 0f;
    private const float shootInterval = 1f; // 射击间隔1秒

    void Start()
    {
        anim = GetComponent<Animator>();

        // 如果hero未指定，尝试查找英雄对象
        if (hero == null)
        {
            GameObject heroObj = GameObject.FindGameObjectWithTag("Player");
            if (heroObj != null)
            {
                hero = heroObj.transform;
            }
        }
    }

    void Update()
    {
        // 示例控制代码（实际游戏中替换为你的AI逻辑）
        float speed = Input.GetKey(KeyCode.R) ? 1 : 0; // 按R键测试奔跑
        anim.SetFloat("Speed", speed);

        if (Input.GetKeyDown(KeyCode.S)) // 按S键测试射击
        {
            anim.SetBool("IsAttacking", true);
            Invoke("ResetAttack", 0.5f); // 0.5秒后重置攻击状态
        }

        if (Input.GetKeyDown(KeyCode.D)) // 按D键测试死亡
        {
            anim.SetTrigger("IsDead");
        }

        // 自动射击逻辑
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning("子弹预制体未设置！");
            return;
        }

        // 触发攻击动画
        anim.SetBool("IsAttacking", true);
        Invoke("ResetAttack", 0.5f); // 0.5秒后重置攻击状态

        // 从敌人中心位置创建子弹
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 如果有英雄对象，让子弹朝向英雄
        if (hero != null)
        {
            Vector3 direction = (hero.position - transform.position).normalized;
            bullet.transform.right = direction;
        }
    }

    void ResetAttack()
    {
        anim.SetBool("IsAttacking", false);
    }
}