using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraBehavior : MonoBehaviour
{
    public GameObject mFocus = null;
    private float mFollowRate = 0.007f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(mFocus != null);
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 pos = mFocus.transform.localPosition;
        pos = Vector3.LerpUnclamped(transform.localPosition, pos, mFollowRate);
        pos.z = -10;
        transform.localPosition = pos;
    }
}
