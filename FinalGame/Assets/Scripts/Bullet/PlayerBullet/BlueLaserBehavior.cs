using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : BulletBehavior
{
    private Animator mAnimator = null;
    void Awake()
    {
        mStatusTime = Time.time;
        mBulletStatus = BulletStatus.Flying;
    }
    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject e = collision.gameObject;
        if (e.name == "Enemy1" || e.name == "Wall")
        {
            Debug.Log("Trigger");
            if (mBulletStatus != BulletStatus.Crash)
            {
                mStatusTime = Time.time;
                mBulletStatus = BulletStatus.Crash;
                mAnimator.SetTrigger("Destroy");
            }
        }
    }
}
