using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> points;

    [SerializeField]
    private int speed;

    private int curTarget = 0;
    private int moveType; // 1: 직선 이동, 2: 코너 이동, 3: 네모 이동

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
        
        this.transform.position
            = Vector3.MoveTowards(this.transform.position, points[curTarget].transform.position,
        speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, points[curTarget].transform.position);

        if (distance <= 0.1f)
        {
            Debug.Log("reached to target");

            curTarget = (curTarget + 1) % moveType;
            Debug.Log(curTarget);
        }
    }
}
