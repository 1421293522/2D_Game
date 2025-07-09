using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserBullet2 : BulletBehavior
{
    private Animator mAnimator = null;
    void Awake()
    {
        mStatusTime = Time.time;
        mBulletStatus = BulletStatus.Flying;
        mDamage = 10;
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
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        GameObject e = collision.gameObject;
        if (e.name == "Hero" || e.name == "Wall")
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
