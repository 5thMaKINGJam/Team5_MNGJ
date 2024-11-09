using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> points;

    [SerializeField]
    private int speed;

    [SerializeField]
    private GameObject sight;

    public Vector2 direction;
    public Vector3 targetPosition;

    private int curTarget = 0;
    private int moveType;

    private void Awake()
    {

    }

    void Start()
    {
        moveType = points.Count;
        this.transform.position = points[curTarget].transform.position;
    }

    void Update()
    {
        targetPosition = points[curTarget].transform.position;

        this.transform.position
            = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 change = targetPosition - transform.position;

        SetDirection(change);

        ChangeTarget();
    }

    void SetDirection(Vector3 change)
    {
        if (Mathf.Abs(change.x) > Mathf.Abs(change.y))  // 좌우
        {
            if (change.x > 0)
            {
                direction = Vector2.right;
            }
            else
            {
                direction = Vector2.left;
            }
        }
        else  // 상하
        {
            if (change.y > 0)
            {
                direction = Vector2.up;
            }               
            else
            {
                direction = Vector2.down;
            }
        }
    }

    void ChangeTarget()
    {
        float distance = Vector3.Distance(transform.position, points[curTarget].transform.position);

        if (distance <= 0.1f)
        {
            curTarget = (curTarget + 1) % moveType;
        }
    }
}
