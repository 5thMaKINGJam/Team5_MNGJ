using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletObj;

    [SerializeField]
    private GameObject sight;

    [SerializeField] private int force;
    [SerializeField] private float maxDelay;

    private float curDelay;

    void Update()
    {
        Attack();
        Reload();

        Vector3 position = transform.position;
        position.z = 1;
        transform.position = position;
    }

    void Attack()
    {
        if (curDelay < maxDelay)
        {
            return;
        }

        GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector3 dirVec = sight.transform.position - this.transform.position;
        rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);

        curDelay = 0;
    }

    void Reload()
    {
        curDelay += Time.deltaTime;
    }
}
