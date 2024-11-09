using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private List<Image> heartObjs;

    [SerializeField]
    private List<Sprite> heartSprites;

    public void DrawHearts(int heart)
    {
        for (int i = 0; i < heartObjs.Count; i++)
        {
            if (i < heart)
            {
                heartObjs[i].sprite = heartSprites[0];
            }
            else
            {
                heartObjs[i].sprite = heartSprites[1];
            }
        }
    }
}
