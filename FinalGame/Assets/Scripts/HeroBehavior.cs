using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HeroBehavior : MonoBehaviour
{
    public float mMoveSpeed = 10f;
    private bool mIsTowardsRight;
    private bool mIsMove = false;
    private Animator mAnimator = null;
    private bool mIsShooting = false;
    private float mShootTime = 0f;

    public float mShootSpeed = 10f;

    // Start is called before the first frame update

    void Start()
    {
        mAnimator = GetComponent<Animator>();

        Debug.Assert(mAnimator != null);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = 0f, vertical = 0f;

        // get input
        if (Input.GetKey(KeyCode.D)) horizontal += 1f;
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f;
        if (Input.GetKey(KeyCode.W)) vertical += 1f;
        if (Input.GetKey(KeyCode.S)) vertical -= 1f;
        mIsShooting = Input.GetMouseButton(0);

        mIsTowardsRight = horizontal >= 0 ? true : false;

        mIsMove = Move(horizontal, vertical);

        mAnimator.SetBool("Move", mIsMove);
        gameObject.GetComponent<SpriteRenderer>().flipX = !mIsTowardsRight;

        if (mIsShooting)
        {
            Shoot();
        }
        mAnimator.SetBool("Shoot", mIsMove);
    }

    
    public bool Move(float horizontal, float vertical)
    {
        if (horizontal == 0 && vertical == 0)
            return false;
        
        // transform
        Vector3 pos = transform.localPosition;
        Vector3 mov = new Vector3(horizontal, vertical, 0).normalized;
        pos += mov * mMoveSpeed * Time.smoothDeltaTime;
        transform.localPosition = pos;

        return true;
    }

    public bool Shoot()
    {
        if (Time.time - mShootTime < mShootSpeed)
            return false;
        // Respwan
        mShootTime = Time.time;
        GameObject e = Instantiate(Resources.Load("Prefabs/Blue_Bullet") as GameObject);
        e.transform.localPosition = transform.position;
        return true;
    }


    public bool Death()
    {
        return false;
    }
}