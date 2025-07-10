using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Status : HumanoidStatus
{
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsDying && (Time.time - mStatusTimer) > 0.2f)
        {
            Die();
        }
    }

    public override void GetHurt(int damage)
    {
        base.GetHurt(damage);
        if (mIsDying)
        {
            mAnimator.SetTrigger("Die");
            
        }
        else
        {
            mAnimator.SetTrigger("GetHurt");
        }
    }

    public override void Die()
    {
        base.Die();
    }
}
