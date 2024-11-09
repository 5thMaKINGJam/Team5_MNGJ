using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUI : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;
    private SpriteRenderer spriteRenderer;
    private MonsterMove monsterMove;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        monsterMove = GetComponentInChildren<MonsterMove>();
    }

    void Update()
    {
        this.transform.position = monsterMove.transform.position;
        Vector2 direction = monsterMove.direction;
        ChangeSprite(direction);
    }

    public void ChangeSprite(Vector2 direction)
    {
        int spriteNum = 0;
        if (direction == Vector2.right)
        {
            spriteNum = 0;
        }
        else if(direction == Vector2.left) {
            spriteNum = 1;
        }
        else if (direction == Vector2.up)
        {
            spriteNum = 2;
        }
        else if (direction == Vector2.down)
        {
            spriteNum = 3;
        }

        spriteRenderer.sprite = sprites[spriteNum];
    }
}
