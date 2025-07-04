using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WayPointBehavior : MonoBehaviour
{
    private int mNumHit = 0;
    private const int kHitsToDestroy = 4;
    private const float kWayPointOpacityLost = 0.25f;
    private const float kRespawnRange = 15f;
    private float mWayPointOpacityPercent = 1f;
    private Vector3 mInitPos = Vector3.zero;
    private float mInitOpacity = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        mInitPos = transform.localPosition;
        Color c = GetComponent<Renderer>().material.color;
        mInitOpacity = c.a;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        mNumHit = 0;
        mWayPointOpacityPercent = 1f;
    }

    #region Trigger into chase or die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Emeny OnTriggerEnter");
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (g.name == "Egg(Clone)")
        {
            mNumHit++;
            if (mNumHit < kHitsToDestroy)
            {
                Color c = GetComponent<Renderer>().material.color;
                mWayPointOpacityPercent -= kWayPointOpacityLost;
                c.a = mInitOpacity * mWayPointOpacityPercent;
                GetComponent<Renderer>().material.color = c;
            }
            else
            {
                ThisEnemyIsHit();
            }
        }
    }

    private void ThisEnemyIsHit()
    {

        Color c = GetComponent<Renderer>().material.color;
        mWayPointOpacityPercent -= kWayPointOpacityLost;
        c.a = mInitOpacity * mWayPointOpacityPercent;
        GetComponent<Renderer>().material.color = c;
        Respawn();
    }
    #endregion

    #region Respawn after die
    private void Respawn()
    {
        Vector3 pos = transform.localPosition;
        pos.x = Random.Range(mInitPos.x - kRespawnRange, mInitPos.x + kRespawnRange);
        pos.y = Random.Range(mInitPos.y - kRespawnRange, mInitPos.y + kRespawnRange);
        transform.localPosition = pos;

        Color c = GetComponent<Renderer>().material.color;
        c.a = mInitOpacity;
        GetComponent<Renderer>().material.color = c;

        Init();
    }
    #endregion
}
