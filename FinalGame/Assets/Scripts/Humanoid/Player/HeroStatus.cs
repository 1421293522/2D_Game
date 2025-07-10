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
        StartCoroutine(WaitForAnimationThenDie());
    }

    private IEnumerator WaitForAnimationThenDie()
    {
        // 等待动画播放完成
        yield return new WaitForSeconds(GetAnimationLength("Die"));

        // 动画播放完成后调用基类的Die方法
        base.Die();
    }

    private float GetAnimationLength(string animationName)
    {
        // 获取动画剪辑的长度
        AnimationClip[] clips = mAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0f; // 如果没有找到动画，返回0
    }
}
