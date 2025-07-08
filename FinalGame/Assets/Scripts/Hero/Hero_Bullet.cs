using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Bullet : MonoBehaviour
{
    private float kMoveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        // transfrom
        Vector3 pos = transform.localPosition;
        pos += transform.right * kMoveSpeed * Time.smoothDeltaTime * 30;
        transform.localPosition = pos;
    }

    public void Destroy()
    {
        
    }
}
