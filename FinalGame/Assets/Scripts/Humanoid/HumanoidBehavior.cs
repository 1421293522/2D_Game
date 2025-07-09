using System;
using UnityEngine;


// Humanoid Common Behavior
// 5 Behavior: Idle, Move, Shoot, Gethurt, Death
public class HumanoidBehavior : MonoBehaviour
{
    public GameObject mBullet = null;
    public int mHealthPoint = 100;
    public float mMoveSpeed = 30f;
    public float mShootRate = 0.1f;
    private float mShootTime = 0f;
    private int hit = 1;
    void Start()
    {

    }

    void Update()
    {

    }

    public void Idle()
    {
        // do nothing
        return;
    }

    // direction don't need to be normalized
    public void Move(Vector3 direction)
    {
        direction.z = 0;
        Vector3 pos = transform.localPosition;
        pos += mMoveSpeed * Time.smoothDeltaTime * direction.normalized * hit;
        transform.localPosition = pos;
    }

    public void Shoot(Vector3 direction)
    {
        direction.z = 0;
        if (Time.time - mShootTime > mShootRate)
        {
            mShootTime = Time.time;
            GameObject e = Instantiate(mBullet);
            e.transform.position = transform.position;
            e.transform.right = direction;
        }
    }

    public void GetHurt(int damage)
    {
        if (mHealthPoint - damage > 0)
        {
            mHealthPoint -= damage;
        }
        else
        {
            mHealthPoint = 0;
        }
    }

    public void Death()
    {
        return;
    }

    public void OnFootTriggerEnter2D(Collider2D collision)
    {
        hit = -1;
    }

    public void OnFootTriggerExit2D(Collider2D collision)
    {
        hit = 1;
    }
}