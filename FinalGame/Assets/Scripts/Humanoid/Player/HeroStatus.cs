using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatus : HumanoidStatus
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Die()
    {
        mAnimator.SetTrigger("Die");
        base.Die();
    }
}
