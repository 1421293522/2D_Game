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
        // �ȴ������������
        yield return new WaitForSeconds(GetAnimationLength("Die"));

        // ����������ɺ���û����Die����
        base.Die();
    }

    private float GetAnimationLength(string animationName)
    {
        // ��ȡ���������ĳ���
        AnimationClip[] clips = mAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0f; // ���û���ҵ�����������0
    }
}
