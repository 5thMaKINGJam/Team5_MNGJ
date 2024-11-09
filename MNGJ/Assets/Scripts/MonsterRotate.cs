using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRotate : MonoBehaviour
{
    [SerializeField]
    private GameObject monster;

    void Update()
    {
        Vector3 targetPosition = monster.GetComponent<MonsterMove>().targetPosition;

        Vector3 change = targetPosition - transform.position;

        ChangeRotation(change);
    }

    void ChangeRotation(Vector3 change)
    {
        float angle = Mathf.Atan2(change.y, change.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

