using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HumanoidStatus : MonoBehaviour
{
    protected Animator mAnimator = null;
    public int mHealthPoint = 100;


    protected void Init()
    {
        mAnimator = GetComponent<Animator>();
    }
    virtual public void GetHurt(int damage)
    {
        if (mHealthPoint - damage <= 0)
        {
            mHealthPoint = 0;
        }
        else
        {
            mHealthPoint -= damage;
        }
        Die();
    }

    virtual public void Die()
    {
        if (mHealthPoint <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
    
}
