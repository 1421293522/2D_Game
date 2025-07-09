using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardController : MonoBehaviour
{
    private HumanoidBehavior mBehaviorHandler = null;
    private float mStatusTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mBehaviorHandler = GetComponent<HumanoidBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) direction.y += 1f;
        if (Input.GetKey(KeyCode.S)) direction.y -= 1f;
        if (Input.GetKey(KeyCode.D)) direction.x += 1f;
        if (Input.GetKey(KeyCode.A)) direction.x -= 1f;
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mBehaviorHandler.mMoveDirection = direction;
        mBehaviorHandler.mFacingDirection = mousepos - transform.localPosition;

        if (direction != Vector3.zero)
        {
            mBehaviorHandler.Move();
        }
        else
        {
            mBehaviorHandler.Idle();
        }

        if (Input.GetMouseButton(0))
        {
            mBehaviorHandler.Shoot();
        }
    }
}
