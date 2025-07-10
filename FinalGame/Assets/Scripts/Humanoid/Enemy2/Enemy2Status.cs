using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Status : HumanoidStatus
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
        Debug.Log(mHP);
        base.GetHurt(damage);
        if (mHealthPoint == 0)
        {
            mIsDying = true;
            mAnimator.SetTrigger("Die");
            mStatusTimer = Time.time;
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
