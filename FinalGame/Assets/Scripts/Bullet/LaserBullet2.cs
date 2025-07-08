using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet2 : BulletBehavior
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
        //mAnimator = gameObject.GetComponent<Animator>();

        //Debug.Assert(mAnimator != null);
    }

    // Update is called once per frame
    void Update()
    {
        switch (mBulletStatus)
        {
            case BulletStatus.Flying:
                if (!Move())
                {
                    mBulletStatus = BulletStatus.Destroyed;
                    mStatusTime = Time.time;
                }
                break;
            case BulletStatus.Crash:
                if (!Hit())
                {
                    Debug.Log("Hit End");
                    mBulletStatus = BulletStatus.Destroyed;
                    mStatusTime = Time.time;
                }
                break;
            case BulletStatus.Destroyed:
                Destroy();
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject e = collision.gameObject;
        if (e.name == "Hero")
        {
            Debug.Log("Trigger");
            if (mBulletStatus != BulletStatus.Crash)
            {
                mStatusTime = Time.time;
                mBulletStatus = BulletStatus.Crash;
                //mAnimator.SetTrigger("Destroy");
            }
        }
    }
}
