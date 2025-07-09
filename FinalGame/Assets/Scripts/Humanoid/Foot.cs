using UnityEngine;

public class FootCollision : MonoBehaviour
{
    private HeroBehavior heroBehavior;

    void Start()
    {

        heroBehavior = transform.GetComponentInParent<HeroBehavior>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (heroBehavior != null)
        {
            heroBehavior.OnFootTriggerEnter2D(collision);
            Debug.Log("Åö×²£¡");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (heroBehavior != null)
        {
            heroBehavior.OnFootTriggerExit2D(collision);
        }
    }
}