using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> points;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private int speed;

    [SerializeField]
    private GameObject sight;

    public Vector2 direction;
    public Vector3 targetPosition;

    private int curTarget = 0;
    private int moveType;
    private Vector3 originPosition = Vector3.up;
    private bool followPlayer = false;

    void Start()
    {
        moveType = points.Count;
        this.transform.position = points[curTarget].transform.position;
    }

    void Update()
    {
        // 원래 위치에 도달하면 플레이어 추격 종료
        if (followPlayer) {
            targetPosition = transform.position + new Vector3(direction.x, direction.y, 0) * 10f;
        }
        else if (originPosition != Vector3.up && Vector3.Distance(transform.position, originPosition) <= 0.1f)
        {
            originPosition = Vector3.up;
            targetPosition = points[curTarget].transform.position;
        }
        else
            targetPosition = points[curTarget].transform.position;

        this.transform.position
            = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 change = targetPosition - transform.position;

        SetDirection(change);

        if (!followPlayer)
            ChangeTarget();
    }

    void SetDirection(Vector3 change)
    {
        if (Mathf.Abs(change.x) > Mathf.Abs(change.y))  // 좌우
        {
            if (change.x > 0)
                direction = Vector2.right;
            else
                direction = Vector2.left;
        }
        else  // 상하
        {
            if (change.y > 0)
                direction = Vector2.up;
            else
                direction = Vector2.down;
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

    public void FollowPlayer()
    {
        followPlayer = true;
        Vector3 playerPosition = player.transform.position;
        originPosition = transform.position;

        targetPosition = transform.position + new Vector3(direction.x, direction.y, 0) * 10f; 
    }

    public void ReturnToOrigin()
    {
        followPlayer = false;
        targetPosition = originPosition;
    }
}
