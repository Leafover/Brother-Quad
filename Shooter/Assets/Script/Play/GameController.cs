using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UltimateJoystick joystickMove, joystickShot;
    public static GameController gameController;

    Vector2 movePosition, shootPosition;

    private void Awake()
    {
        gameController = this;
    }

    private void JoystickMovement(UltimateJoystick joystick)
    {
        movePosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);
        if (joystick.GetJoystickState())
        {
            OnMove(movePosition);
        }
        else
        {

            StopMove();
        }
    }
    public void StopMove()
    {
      //  PlayerController.playerController.speedmove = 0;
        if (PlayerController.playerController.playerState == PlayerController.PlayerState.Jump)
            return;
        if (PlayerController.playerController.playerState != PlayerController.PlayerState.Idle)
        {
            PlayerController.playerController.playerState = PlayerController.PlayerState.Idle;
        }
    }

    public void CheckAfterJump(UltimateJoystick joystick)
    {

        movePosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);

        var angle = Mathf.Atan2(movePosition.x, movePosition.y) * Mathf.Rad2Deg;
        var h = movePosition.x;
        if (angle <= 135f && angle >= -135.5f)
        {
            switch (PlayerController.playerController.playerState)
            {
                case PlayerController.PlayerState.RunRight:
                    if (PlayerController.playerController.FlipX)
                    {
                        PlayerController.playerController.FlipX = false;
                    }
                    break;
                case PlayerController.PlayerState.RunLeft:
                    if (!PlayerController.playerController.FlipX)
                    {
                        PlayerController.playerController.FlipX = true;
                    }
                    break;
            }
            PlayerController.playerController.playerState = h > 0 ? PlayerController.PlayerState.RunRight : PlayerController.PlayerState.RunLeft;
        }
        else if ((angle > -180f && angle < -135f) || (angle > 135f && angle < 180f))
        {
            PlayerController.playerController.playerState = PlayerController.PlayerState.Sit;
        }
    }

    public void OnMove(Vector2 axis)
    {
        //if (PlayerController.playerController.isBouderJoystick && PlayerController.playerController.isShooting)
        //{
        //    var target = PlayerController.playerController.GetTargetFromDirection(axis);
        //    PlayerController.playerController.targetPos.position = Vector2.MoveTowards(PlayerController.playerController.targetPos.position, target, Time.deltaTime * 20);
        //}
        var angle = Mathf.Atan2(axis.x, axis.y) * Mathf.Rad2Deg;
        var h = axis.x;
        if (angle <= 135f && angle >= -135.5f)
        {

            switch (PlayerController.playerController.playerState)
            {
                case PlayerController.PlayerState.RunRight:
                    if (PlayerController.playerController.FlipX)
                    {
                        PlayerController.playerController.FlipX = false;
                    }
                    break;
                case PlayerController.PlayerState.RunLeft:
                    if (!PlayerController.playerController.FlipX)
                    {
                        PlayerController.playerController.FlipX = true;
                    }
                    break;
            }
           PlayerController.playerController.speedmove = h > 0 ? 1.5f : -1.5f;
            if (PlayerController.playerController.playerState == PlayerController.PlayerState.Jump)
                return;
            PlayerController.playerController.playerState = h > 0 ? PlayerController.PlayerState.RunRight : PlayerController.PlayerState.RunLeft;

        }
        else if ((angle > -180f && angle < -135f) || (angle > 135f && angle < 180f))
        {

            if (PlayerController.playerController.playerState == PlayerController.PlayerState.Jump)
                return;
            PlayerController.playerController.playerState = PlayerController.PlayerState.Sit;
        }
    }
    private void JoystickShooting(UltimateJoystick joystick)
    {
        shootPosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);

        if (joystick.GetJoystickState())
        {
            if (!PlayerController.playerController.isShooting)
            {
                PlayerController.playerController.ShootDown();
            }
            PlayerController.playerController.isBouderJoystick = joystick.GetDistance() >= 0.9f;
            if (PlayerController.playerController.isBouderJoystick)
            {
                PlayerController.playerController.FlipX = shootPosition.x < 0;

                var target = PlayerController.playerController.GetTargetFromDirection(shootPosition);
                PlayerController.playerController.targetPos.position = Vector2.MoveTowards(PlayerController.playerController.targetPos.position, target, Time.deltaTime * 20);
            }
        }
        else
        {
            if (PlayerController.playerController.isShooting)
            {
                PlayerController.playerController.ShootUp();
                Debug.LogError("stop shot");
            }
        }
    }

    private void Update()
    {
        JoystickMovement(joystickMove);
        JoystickShooting(joystickShot);
        PlayerController.playerController.OnUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerController.playerController.TryJump();
        }
    }

}
