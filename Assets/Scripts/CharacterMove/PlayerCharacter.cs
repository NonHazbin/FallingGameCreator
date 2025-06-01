using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{
    private enum MoveDirectionType
    {
        Both,
        HorizontalOnly,
        VerticalOnly
    }
    [SerializeField, Header("WASDキーでのそうさ")]
    private bool _wasdControl = false;
    [SerializeField, Header("矢印キーでのそうさ")]
    private bool _arrowControl = false;

    [SerializeField, Header("移動可能方向の設定")]
    private MoveDirectionType _moveDirectionType = MoveDirectionType.Both;
    protected override void Move()
    {
        float horizontal = 0f;
        float vertical = 0f;

        if (Keyboard.current != null)
        {
            if (_wasdControl)
            {
                if (Keyboard.current.wKey.isPressed)
                {
                    vertical += 1;
                }
                if (Keyboard.current.sKey.isPressed)
                {
                    vertical -= 1;
                }
                if (Keyboard.current.aKey.isPressed)
                {
                    horizontal -= 1;
                }
                if (Keyboard.current.dKey.isPressed)
                {
                    horizontal += 1;
                }
            }
            if (_arrowControl)
            {
                if (Keyboard.current.upArrowKey.isPressed)
                {
                    vertical += 1;
                }
                if (Keyboard.current.downArrowKey.isPressed)
                {
                    vertical -= 1;
                }
                if (Keyboard.current.leftArrowKey.isPressed)
                {
                    horizontal -= 1;
                }
                if (Keyboard.current.rightArrowKey.isPressed)
                {
                    horizontal += 1;
                }
            }
        }
        // 移動方向の制限
        switch (_moveDirectionType)
        {
            case MoveDirectionType.HorizontalOnly:
                vertical = 0;
                break;
            case MoveDirectionType.VerticalOnly:
                horizontal = 0;
                break;
            case MoveDirectionType.Both:
                break;
        }

        _moveDirection = new Vector2(horizontal, vertical).normalized;
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }
}
