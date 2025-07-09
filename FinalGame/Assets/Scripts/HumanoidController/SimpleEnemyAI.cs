using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    private HumanoidBehavior mBehaviorHandler = null;
    private float mStatusTimer = 0f;
    void Start()
    {
        mBehaviorHandler = GetComponent<HumanoidBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
