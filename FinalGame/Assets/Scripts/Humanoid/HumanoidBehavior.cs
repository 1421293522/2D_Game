using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
// Humanoid Common Behavior
// 5 Behavior: Idle, Move, Shoot, Gethurt, Death
public class HumanoidBehavior : MonoBehaviour
{
    protected Rigidbody2D mRigidBody = null;
    protected Animator mAnimator = null;

    // used by Move, include speed & direction
    public float mSpeed;
    protected Vector3 mDirection;
    public Vector3 mMoveDirection
    {
        set { mDirection = value.normalized; }
    }

    // used by others
    protected Vector3 mTowards;
    public Vector3 mFacingDirection
    {
        set { mTowards = value.normalized; }
    }

    // used by Shoot
    public GameObject mBullet = null;
    public float mShootRate = 0.1f;
    protected float mShootTimer = 0f;

    void Start()
    {
        Init();
    }

    void Update()
    {

    }

    virtual protected void Init()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mRigidBody.freezeRotation = true;
    }
    
    virtual public void Idle()
    {

    }

    virtual public void Move()
    {
        mRigidBody.AddForce(50 * mDirection);
    }

    virtual public void Shoot()
    {
        GameObject e = Instantiate(mBullet);
        e.transform.localPosition = transform.localPosition;
        float angle = Mathf.Atan2(mTowards.y, mTowards.x) * Mathf.Rad2Deg;
        e.transform.rotation = Quaternion.Euler(0, 0, angle);
    }


}