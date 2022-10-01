using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour
{
    [SerializeField]
    private Sprite stopSprite;

    [SerializeField]
    private Sprite goSprite;


    private SpriteRenderer m_spriteRend;

    public void Awake()
    {
        m_spriteRend = GetComponent<SpriteRenderer>();
    }

    public void SwitchSprites(bool isGreen)
    {
        if (!isGreen) m_spriteRend.sprite = stopSprite;
        else if (isGreen) m_spriteRend.sprite = goSprite;
    }
}