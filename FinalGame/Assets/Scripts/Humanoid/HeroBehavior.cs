using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class HeroBehavior : HumanoidBehavior
{
    private bool mIsTowardsRight = true;
    private Animator mAnimator = null;
    private bool mIsShooting = false;


    // Start is called before the first frame update

    void Start()
    {
        mAnimator = GetComponent<Animator>();

        Debug.Assert(mBullet != null);
        Debug.Assert(mAnimator != null);
    }

    // Update is called once per frame
    void Update()
    {
        // Turn Keyboard control into value
        float horizontal = 0f, vertical = 0f;

        if (Input.GetKey(KeyCode.D)) horizontal += 1f;
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f;
        if (Input.GetKey(KeyCode.W)) vertical += 1f;
        if (Input.GetKey(KeyCode.S)) vertical -= 1f;
        mIsShooting = Input.GetMouseButton(0);


        Vector3 mov = new Vector3(horizontal, vertical, 0).normalized;
        if (mov != Vector3.zero)
        {
            mAnimator.SetFloat("MoveSpeed", 1, 0.1f, Time.smoothDeltaTime);
            Move(new Vector3(horizontal, vertical));
        }
        else
        {
            mAnimator.SetFloat("MoveSpeed", 0, 0.1f, Time.smoothDeltaTime);
        }

        if (mIsShooting)
        {
            float mouseHorizontal = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.localPosition).x;
            mIsTowardsRight = mouseHorizontal > 0 || (mouseHorizontal >= 0 && mIsTowardsRight);
        }
        else
            mIsTowardsRight = horizontal > 0 || (horizontal >= 0 && mIsTowardsRight);
            

        gameObject.GetComponent<SpriteRenderer>().flipX = !mIsTowardsRight;

        if (mIsShooting)
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Shoot(target - transform.localPosition);
        }
        mAnimator.SetBool("Shoot", mIsShooting);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}