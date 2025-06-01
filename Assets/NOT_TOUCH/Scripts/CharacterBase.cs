using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
/// <summary>
/// キャラクターの共通動作を定義する基底クラス（抽象クラス）
/// ・移動速度、方向
/// ・画像外部設定
/// </summary>
public abstract class CharacterBase : MonoBehaviour
{
    protected enum ChangeAddType
    {
        Both,
        Timer,
        Point,
        Player
    }
    [System.Serializable]
    protected class IndividualitySetting
    {
        [SerializeField, Header("属性")]
        private ChangeAddType changeType = ChangeAddType.Both;
        [SerializeField, Header("時間変化(秒)")]
        private float changeTimer = 2f;
        [SerializeField, Header("点数変化")]
        private int changePoints = 10;

        // 値を外部から取得するプロパティ
        public ChangeAddType ChangeType => changeType;
        public float ChangeTimer => changeTimer;
        public int ChangePoints => changePoints;
    }

    [SerializeField, Header("キャラクターの個性設定")]
    protected IndividualitySetting _individuality;

    [SerializeField, Header("スピード（動きの速さ）")]
    protected float _moveSpeed = 20f;

    [SerializeField, Header("キャラクター写真")]
    protected Sprite _defaultSprite; // 任意の画像をInspectorで設定
    protected SpriteRenderer _characterSprite; //スプライト表示用のコンポーネント
    protected Vector2 _moveDirection; //移動方向
    protected BoxCollider2D _characterCollider; //当たり判定

    protected Rigidbody2D _rigidbody2D;

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

        _characterCollider = GetComponent<BoxCollider2D>();
        if (_characterCollider == null)
        {
            _characterCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        _characterCollider.isTrigger = true;

        _rigidbody2D = GetComponent<Rigidbody2D>();
        if (_rigidbody2D == null)
        {
            _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        }

        // Rigidbody2D の設定（必要に応じて）
        _rigidbody2D.gravityScale = 0; // 2D重力を無効化（上から落ちないように）
        _rigidbody2D.freezeRotation = true; // 回転しないように
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
    /// <summary>
    /// 衝突時の属性に応じた処理
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log($"{gameObject.name} が {other.gameObject.name} と接触しました");

        switch (_individuality.ChangeType)
        {
            case ChangeAddType.Timer:
                HandleTimerEffect();
                break;

            case ChangeAddType.Point:
                HandlePointEffect();
                break;

            case ChangeAddType.Both:
                HandleTimerEffect();
                HandlePointEffect();
                break;

            case ChangeAddType.Player:
                Debug.Log("プレイヤー属性なので、衝突処理は行いません");
                break;
        }
    }


    /// <summary>
    /// 時間ベースの処理（例：一定時間で変化）
    /// </summary>
    protected virtual void HandleTimerEffect()
    {
        Debug.Log($"Timer属性: {_individuality.ChangeTimer}秒の効果を発動（ここに処理を追加）");
        // 必要に応じて Coroutine や Invoke を使う
    }

    /// <summary>
    /// 得点加算などの処理
    /// </summary>
    protected virtual void HandlePointEffect()
    {
        Debug.Log($"Point属性: {_individuality.ChangePoints}点を加算（ここに処理を追加）");
        // 例: GameManager.Instance.AddScore(_individuality.ChangePoints);
    }
}
