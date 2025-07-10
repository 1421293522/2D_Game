using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatus : HumanoidStatus
{
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mIsDying)
        {
            if (Time.time - mStatusTimer > 1.15f)
            {
                Die();
            }
            else
            {
            }
        }

    }

    public override void GetHurt(int damage)
    {
        if (mHealthPoint > 0) { base.GetHurt(damage); }
        else
        {
            if (!mIsDying)
            {
                mIsDying = true;
                mStatusTimer = Time.time;
                mAnimator.SetTrigger("Die");
            }
        }
    }

    public override void Die()
    {
        base.Die();
    }
}
