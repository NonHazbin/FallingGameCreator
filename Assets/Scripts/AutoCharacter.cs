using UnityEngine;
/// <summary>
/// 自動でランダムな方向に移動するキャラクタークラス
/// </summary>
public class AutoCharacter : CharacterBase
{
    [SerializeField, Header("方向が変わる時間")]
    private float changeInterval = 2f;

    private float timer = 0f;

    protected override void Start()
    {
        base.Start();
        SetRandomDirection();
    }
    protected override void Move()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (changeInterval < timer)
        {
            SetRandomDirection();
            timer = 0f;
        }

        transform.Translate(moveDirection * moveSpeed_ * Time.deltaTime);
    }

    // ランダムに上下左右の方向を洗濯して設定
    public void SetRandomDirection()
    {
        int randomDirction = Random.Range(0, 4);
        moveDirection = randomDirction switch
        {
            0 => Vector2.up,
            1 => Vector2.down,
            2 => Vector2.left,
            3 => Vector2.right,
            _ => Vector2.zero
        };
    }
}
