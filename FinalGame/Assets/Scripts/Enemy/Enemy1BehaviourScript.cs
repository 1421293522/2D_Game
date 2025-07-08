using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyBasic : MonoBehaviour
{
    private Animator anim;
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public Transform hero; // Ӣ�۵�Transform
    private float shootTimer = 0f;
    private const float shootInterval = 1f; // ������1��

    void Start()
    {
        anim = GetComponent<Animator>();

        // ���heroδָ�������Բ���Ӣ�۶���
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
        // ʾ�����ƴ��루ʵ����Ϸ���滻Ϊ���AI�߼���
        float speed = Input.GetKey(KeyCode.R) ? 1 : 0; // ��R�����Ա���
        anim.SetFloat("Speed", speed);

        if (Input.GetKeyDown(KeyCode.S)) // ��S���������
        {
            anim.SetBool("IsAttacking", true);
            Invoke("ResetAttack", 0.5f); // 0.5������ù���״̬
        }

        if (Input.GetKeyDown(KeyCode.D)) // ��D����������
        {
            anim.SetTrigger("IsDead");
        }

        // �Զ�����߼�
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
            Debug.LogWarning("�ӵ�Ԥ����δ���ã�");
            return;
        }

        // ������������
        anim.SetBool("IsAttacking", true);
        Invoke("ResetAttack", 0.5f); // 0.5������ù���״̬

        // �ӵ�������λ�ô����ӵ�
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // �����Ӣ�۶������ӵ�����Ӣ��
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