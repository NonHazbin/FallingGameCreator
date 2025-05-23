using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
/// <summary>
/// キャラクターの共通動作を定義する基底クラス（抽象クラス）
/// ・移動速度、方向
/// ・画像外部設定
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField,Header("スピード（動きの速さ）")]
    protected float moveSpeed_ = 20f;

    [SerializeField, Header("キャラクター写真")]
    protected Sprite defaultSprite; // 任意の画像をInspectorで設定
    protected SpriteRenderer characterSprite; //スプライト表示用のコンポーネント
    protected Vector2 moveDirection; //移動方向
    protected Collider2D characterCollider; //当たり判定

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

    protected virtual void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void SetSprite(Sprite newSprite_)
    {
        if (characterSprite != null && newSprite_ != null)
        {
            characterSprite.sprite = newSprite_;
        }
    }
}
