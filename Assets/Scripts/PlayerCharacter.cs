using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : CharacterBase
{
    [SerializeField, Header("WASDキーでのそうさ")]
    private bool _wasdControl = false;
    [SerializeField, Header("矢印キーでのそうさ")]
    private bool _arrowControl = false;
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

        _moveDirection = new Vector2(horizontal, vertical).normalized;
        transform.Translate(_moveDirection * _moveSpeed * Time.deltaTime);
    }
}
