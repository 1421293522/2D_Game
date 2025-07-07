using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public partial class EnemyBehavior : MonoBehaviour
{

    // All instances of Enemy shares this one WayPoint and EnemySystem
    static private EnemySpawnSystem sEnemySystem = null;
    static public void InitializeEnemySystem(EnemySpawnSystem s) { sEnemySystem = s; }

    private int mNumHit = 0;
    private const int kHitsToDestroy = 4;
    private const float kEnemyEnergyLost = 0.8f;

    // Movement
    private enum Point { A, B, C, D, E, F };
    private Point nextPoint = Point.A;

    private enum Mode { Sequential, Random };
    private Mode movementMode = Mode.Sequential;
    private const float kEnemyRotateSpeed = 90f / 2f;
    private const float kEnemySpeed = 20f;
    private const float kTurnRate = 0.03f;  // 0.03/60 as specified in requirements
    Vector3 direction;

    private float mEnemySpeed = kEnemySpeed;

    private void Awake()
    {
    }

    void Start()
    {
    }

    void Update()
    {
        // Toggle
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (movementMode == Mode.Sequential) movementMode = Mode.Random;
            else movementMode = Mode.Sequential;
        }

        // Get target waypoint position
        Vector3 targetPosition = GetWayPointPosition(nextPoint);

        // New Rotation Logic - Use PointAtPosition function as specified
        PointAtPosition(targetPosition, kTurnRate);
        transform.position += transform.up * mEnemySpeed * Time.deltaTime;
        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget <= 25f) // Within 25 units
        {
            nextPoint = NextPoint(nextPoint);
        }
    }

    // compute next waypoint
    private Point NextPoint(Point currentPoint)
    {
        if (movementMode == Mode.Sequential)
        {
            // Use modulus to cycle through enum values
            int nextIndex = ((int)currentPoint + 1) % 6;
            return (Point)nextIndex;
        }
        else // Random
        {
            Point next;
            do
            {
                next = (Point)Random.Range(0, 6);
            } while (next == currentPoint);
            return next;
        }
    }

    // Get the position of a specific waypoint
    private Vector3 GetWayPointPosition(Point point)
    {
        // Find waypoint GameObject by name
        string wayPointName = "WayPoint" + point.ToString();
        GameObject wayPointObj = GameObject.Find(wayPointName);

        if (wayPointObj != null)
        {
            return wayPointObj.transform.position;
        }

        // Fallback positions if GameObjects not found
        switch (point)
        {
            case Point.A: return new Vector3(-50, 50, 0);
            case Point.B: return new Vector3(0, 100, 0);
            case Point.C: return new Vector3(50, 50, 0);
            case Point.D: return new Vector3(50, -50, 0);
            case Point.E: return new Vector3(0, -100, 0);
            case Point.F: return new Vector3(-50, -50, 0);
            default: return Vector3.zero;
        }
    }

    private bool AreVector3Equal(Vector3 a, Vector3 b, float tolerance = 1f)
    {
        return Mathf.Abs(a.x - b.x) <= tolerance &&
               Mathf.Abs(a.y - b.y) <= tolerance;
    }

    // Function to gradually point the enemy towards a target position
    private void PointAtPosition(Vector3 targetPosition, float turnRate)
    {
        // Calculate direction to target
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.z = 0;
        
        // Gradually turn towards the target direction
        if (direction != Vector3.zero)
        {
            transform.up = Vector3.LerpUnclamped(transform.up, direction, turnRate);
        }
    }

    #region Trigger into chase or die
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("Emeny OnTriggerEnter");
        TriggerCheck(collision.gameObject);
    }

    private void TriggerCheck(GameObject g)
    {
        if (g.name == "Hero")
        {
            ThisEnemyIsHit();
        }
        else if (g.name == "Egg(Clone)")
        {
            mNumHit++;
            if (mNumHit < kHitsToDestroy)
            {
                Color c = GetComponent<Renderer>().material.color;
                c.a = c.a * kEnemyEnergyLost;
                GetComponent<Renderer>().material.color = c;
            }
            else
            {
                ThisEnemyIsHit();
            }
        }
        else { mEnemySpeed = kEnemySpeed; }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("WayPoint"))
        {
            mEnemySpeed = kEnemySpeed;
        }
    }
    private void ThisEnemyIsHit()
    {
        sEnemySystem.OneEnemyDestroyed();
        Destroy(gameObject);
    }
    #endregion
}
