using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet Behavior
// Two status: Flying and Hit
// Three behavior: Move Forward, Hit something, Destroyed
public class BulletBehavior : MonoBehaviour
{
    // Status
    public enum BulletStatus
    {
        Flying, Crash, Destroyed
    };

    // information about Bullet should include its speed
    public float mMoveSpeed = 50f;
    public float mFlyingDuration = 3f;
    public float mHitDuration = 1f;
    protected BulletStatus mBulletStatus = BulletStatus.Flying;
    protected float mStatusTime = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Moving forward at a constant speed
    public bool Move()
    {
        if (Time.time - mStatusTime < mFlyingDuration)
        {
            Vector3 pos = transform.localPosition;
            pos += transform.right * mMoveSpeed * Time.smoothDeltaTime;
            transform.localPosition = pos;
            return true;
        }
        else return false;
    }

    // Hit something, the wall or humanoid
    public bool Hit()
    {
        Debug.Log("Hit");
        if (Time.time - mStatusTime < mHitDuration)
        {
            return true;
        }
        else return false;
    }

    // Destroyed it self
    public void Destroy()
    {
        Destroy(transform.gameObject);
    }
    
}
