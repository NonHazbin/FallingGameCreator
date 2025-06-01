using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
/// <summary>
/// キャラクターの共通動作を定義する基底クラス（抽象クラス）
/// ・移動速度、方向
/// ・画像外部設定
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField, Header("スピード（動きの速さ）")]
    protected float _moveSpeed = 20f;

    [SerializeField, Header("キャラクター写真")]
    protected Sprite _defaultSprite; // 任意の画像をInspectorで設定
    protected SpriteRenderer _characterSprite; //スプライト表示用のコンポーネント
    protected Vector2 _moveDirection; //移動方向
    protected Collider2D _characterCollider; //当たり判定

    protected virtual void Start()
    {
        _characterSprite = GetComponent<SpriteRenderer>();
        if (_characterSprite == null)
        {
            _characterSprite = gameObject.AddComponent<SpriteRenderer>();
        }

        if (_defaultSprite != null)
        {
            _characterSprite.sprite = _defaultSprite;
        }

        _characterCollider = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        Move();
        CheckOutOfCameraBounds();
    }

    protected abstract void Move();

    public void SetSprite(Sprite newSprite_)
    {
        if (_characterSprite != null && newSprite_ != null)
        {
            _characterSprite.sprite = newSprite_;
        }
    }
    /// <summary>
    /// カメラの外に出たら自動的に削除
    /// </summary>
    protected void CheckOutOfCameraBounds()
    {
        if (Camera.main == null) return;

        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
