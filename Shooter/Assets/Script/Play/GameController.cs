using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{

    public UltimateJoystick joystickMove, joystickShot;
    public static GameController instance;

    Vector2 movePosition, shootPosition;



    private void Awake()
    {
        instance = this;
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
        if (PlayerController.instance.playerState == PlayerController.PlayerState.Jump)
            return;
        if (PlayerController.instance.playerState != PlayerController.PlayerState.Idle)
        {
            PlayerController.instance.playerState = PlayerController.PlayerState.Idle;
            if (PlayerController.instance.isfalldow)
            {
                PlayerController.instance.isWaitStand = true;
            }
        }
    }

    public void CheckAfterJump(UltimateJoystick joystick)
    {

        movePosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);

        var angle = Mathf.Atan2(movePosition.x, movePosition.y) * Mathf.Rad2Deg;
        var h = movePosition.x;
        if (angle <= 135f && angle >= -135.5f)
        {
            PlayerController.instance.speedmove = h > 0 ? 1.5f : -1.5f;
            PlayerController.instance.dirMove = h > 0 ? true : false;
            PlayerController.instance.playerState = PlayerController.PlayerState.Run;
        }
        else if ((angle > -180f && angle < -135f) || (angle > 135f && angle < 180f))
        {
            PlayerController.instance.playerState = PlayerController.PlayerState.Sit;
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
     //   PlayerController.instance.FlipX = h > 0 ? false : true;
        if (angle <= 135f && angle >= -135.5f)
        {
            PlayerController.instance.speedmove = h > 0 ? 3f : -3f;
            PlayerController.instance.dirMove = h > 0 ? false : true;
            if (PlayerController.instance.playerState == PlayerController.PlayerState.Jump)
                return;
            PlayerController.instance.playerState = PlayerController.PlayerState.Run;
        }
        else if ((angle > -180f && angle < -135f) || (angle > 135f && angle < 180f))
        {
            if (PlayerController.instance.playerState == PlayerController.PlayerState.Jump)
                return;
            PlayerController.instance.playerState = PlayerController.PlayerState.Sit;
        }
    }
    private void JoystickShooting(UltimateJoystick joystick)
    {
        shootPosition = new Vector3(joystick.GetHorizontalAxis(), joystick.GetVerticalAxis(), 0);

        if (joystick.GetJoystickState())
        {
            TryShot();
            PlayerController.instance.isBouderJoystick = joystick.GetDistance() >= 0.9f;

            if (PlayerController.instance.autoTarget.Count == 0)
            {
                if (PlayerController.instance.isBouderJoystick)
                {
                    PlayerController.instance.FlipX = shootPosition.x < 0;
                    PlayerController.instance.SelectNonTarget(shootPosition);
                }

                //   Debug.LogError("ko co target");
            }
            else
            {
                PlayerController.instance.SelectTarget();
                //   Debug.LogError(" co target");
            }
        }
        else
        {
            StopShot();
            if (PlayerController.instance.autoTarget.Count == 0)
            {
                PlayerController.instance.SelectNonTarget(!PlayerController.instance.FlipX ? Vector2.right : Vector2.left);
            }
            else
            {
                PlayerController.instance.SelectTarget();
            }
        }

        //if (PlayerController.instance.autoTarget.Count == 0)
        //{
        //    PlayerController.instance.SelectNonTarget(!PlayerController.instance.FlipX ? Vector2.right : Vector2.left);
        //}
        //else
        //{
        //    PlayerController.instance.SelectTarget();
        //}
    }

    private void Update()
    {

        JoystickMovement(joystickMove);
        JoystickShooting(joystickShot);
        PlayerController.instance.OnUpdate();
        EnemyManager.instance.OnUpdate();

    }
    public void TryShot()
    {
        PlayerController.instance.ShootDown();
    }
    public void StopShot()
    {
        PlayerController.instance.ShootUp();
    }
    public void TryJump()
    {
        PlayerController.instance.TryJump();
    }
    public void BtnGrenade()
    {
        PlayerController.instance.TryGrenade();
    }
}
