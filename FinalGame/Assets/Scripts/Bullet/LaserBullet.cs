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
        mAnimator = gameObject.GetComponent<Animator>();

        Debug.Assert(mAnimator != null);
    }

    // Update is called once per frame
    void Update()
    {
        switch (mBulletStatus)
        {
            case BulletStatus.Flying:
                if (Move()) break;
                else
                {
                    mBulletStatus = BulletStatus.Crash;
                    goto case BulletStatus.Crash;
                }
            case BulletStatus.Crash:
                if (Hit()) break;
                else
                {
                    mBulletStatus = BulletStatus.Destroyed;
                    goto case BulletStatus.Destroyed;
                }
            case BulletStatus.Destroyed:
                Destroy();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (mBulletStatus != BulletStatus.Crash)
        {
            mBulletStatus = BulletStatus.Crash;
        }
    }
}
