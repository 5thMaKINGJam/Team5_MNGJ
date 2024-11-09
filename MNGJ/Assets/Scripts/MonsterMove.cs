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
    private Sprite leftSprite;

    [SerializeField]
    private Sprite rightSprite;

    [SerializeField]
    private Sprite frontSprite;

    [SerializeField]
    private Sprite backSprite;

    private int curTarget = 0;
    private int moveType;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {

    }

    void Start()
    {
        moveType = points.Count;
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.transform.position = points[curTarget].transform.position;
    }

    void Update()
    {
        Vector3 targetPosition = points[curTarget].transform.position;

        this.transform.position
            = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        Vector3 change = targetPosition - transform.position;
        
        ChangeRotation(change);

        ChangeSprite(change);

        ChangeTarget();
    }
    void ChangeRotation(Vector3 change)
    {
        float angle = Mathf.Atan2(change.y, change.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void ChangeSprite(Vector3 change)
    {
        if (Mathf.Abs(change.x) > Mathf.Abs(change.y))  // 좌우
        {
            if (change.x > 0)
                spriteRenderer.sprite = rightSprite; 
            else
                spriteRenderer.sprite = leftSprite; 
        }
        else  // 상하
        {
            if (change.y > 0)
                spriteRenderer.sprite = frontSprite; 
            else
                spriteRenderer.sprite = backSprite; 
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
