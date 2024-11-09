using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMove : MonoBehaviour
{
    NavMeshAgent agent;

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
    private Vector2 originPosition = Vector2.up;
    private bool followPlayer = false;
    private Animator monsterAnim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        moveType = points.Count;
        monsterAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (followPlayer) { // 플레이어 추격
            targetPosition = player.transform.position;
        }
        else 
        {
            targetPosition = points[curTarget].transform.position;

            if (originPosition != Vector2.up && Vector2.Distance(transform.position, originPosition) <= 0.1f)
            {
                originPosition = Vector2.up;
            }
        }

        Vector3 position = transform.position;
        position.z = 0;
        transform.position = position;

        agent.SetDestination(targetPosition);

        Vector2 change = targetPosition - transform.position;
         
        UpdateDirection(change);

        UpdateAnimation();
       
        if (!followPlayer)
            ChangeTarget();
    }

    void UpdateDirection(Vector2 change)
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

    void UpdateAnimation()
    {
        int h = (int)direction.x;
        int v = (int)direction.y;

        monsterAnim.SetBool("isFollow", followPlayer);

        if (monsterAnim.GetInteger("hAxisRaw") != h)
        {
            monsterAnim.SetBool("isChange", true);
            monsterAnim.SetInteger("hAxisRaw", h);
        }
        else if (monsterAnim.GetInteger("vAxisRaw") != v)
        {
            monsterAnim.SetBool("isChange", true);
            monsterAnim.SetInteger("vAxisRaw", v);
        }
        else
        {
            monsterAnim.SetBool("isChange", false);
        }
    }


    void ChangeTarget()
    {
        float distance = Vector2.Distance(transform.position, points[curTarget].transform.position);

        if (distance <= 0.1f)
        {
            curTarget = (curTarget + 1) % moveType;
        }
    }

    public void FollowPlayer()
    {
        followPlayer = true;
        originPosition = transform.position;
        targetPosition = player.transform.position;
    }

    public void ReturnToOrigin()
    {
        followPlayer = false;
        targetPosition = originPosition;
    }
}
