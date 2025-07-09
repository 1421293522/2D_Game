using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HumanoidStatus : MonoBehaviour
{
    public int mHealthPoint = 100;

    public void GetHurt(int damage)
    {
        if (mHealthPoint - damage < 0)
        {
            mHealthPoint = 0;
        }
        else
        {
            mHealthPoint -= damage;
        }
    }

    public void Die()
    {
        if (mHealthPoint == 0)
        {
            Destroy(transform.gameObject);
        }
    }
    
}
