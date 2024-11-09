using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletObj;

    [SerializeField] private int force;
    [SerializeField] private float maxDelay;

    private float curDelay;

    void Update()
    {
        Attack();
        Reload();
    }

    void Attack()
    {
        if (curDelay < maxDelay)
        {
            return;
        }

        GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();

        Vector2 direction = GetComponentInParent<MonsterMove>().direction;

        rigid.AddForce(direction * 10, ForceMode2D.Impulse);

        if (direction == Vector2.left)
        {
            bullet.GetComponent<SpriteRenderer>().flipY = true;
        }

        curDelay = 0;
    }

    void Reload()
    {
        curDelay += Time.deltaTime;
    }
}
