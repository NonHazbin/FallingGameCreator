using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [Header("スピード（動きの速さ）")]
    public float moveSpeed_ = 20f;

    [Header("キャラクター写真")]
    public Sprite defaultSprite; // 任意の画像をInspectorで設定
    protected SpriteRenderer characterSprite;
    protected Vector2 moveDirection;
    protected Collider2D characterCollider;

    protected virtual void Start()
    {
        characterSprite = GetComponent<SpriteRenderer>();
        if (characterSprite == null)
        {
            characterSprite = gameObject.AddComponent<SpriteRenderer>();
        }

        if (defaultSprite != null)
        {
            characterSprite.sprite = defaultSprite;
        }

        characterCollider = GetComponent<Collider2D>();
    }
}
