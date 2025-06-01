using UnityEngine;

public class AutoCharacter : CharacterBase
{
    [SerializeField, Header("自動反転を有効にするか（☑️ = 方向転換あり, ◻️ = 一方向のみ）")]
    private bool _enableAutoReverse = true;

    [SerializeField, Header("移動方法（☑️ = タテ, ◻️ = ヨコ）")]
    private bool _isVertical = true;

    [SerializeField, Header("進み方（☑️ = 正方向, ◻️ = 逆方向）")]
    private bool _isPositiveDirection = true;

    [SerializeField, Header("方向転換の周期（秒）")]
    private float _directionChangeInterval = 2f;

    private float _directionChangeTimer = 0f;

    protected override void Start()
    {
        base.Start();

        // 自動反転する場合だけタイマーを初期化
        if (_enableAutoReverse)
        {
            _directionChangeTimer = _directionChangeInterval / 2f;
        }
    }

    protected override void Move()
    {
        // 方向転換処理
        if (_enableAutoReverse)
        {
            _directionChangeTimer += Time.deltaTime;
            if (_directionChangeTimer >= _directionChangeInterval)
            {
                _isPositiveDirection = !_isPositiveDirection;
                _directionChangeTimer = 0f;
            }
        }

        // 移動方向の設定
        float direction = _isPositiveDirection ? 1f : -1f;

        if (_isVertical)
        {
            _moveDirection = Vector2.up * direction;
        }
        else
        {
            _moveDirection = Vector2.right * direction;
        }

        // 移動処理
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }
}
