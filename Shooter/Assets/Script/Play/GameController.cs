﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetReferenAnimAssetForPlayer
{

}

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
            if (PlayerController.playerController.isfalldow)
            {
                PlayerController.playerController.isWaitStand = true;
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
            PlayerController.playerController.speedmove = h > 0 ? 1.5f : -1.5f;
            PlayerController.playerController.dirMove = h > 0 ? true : false;
            PlayerController.playerController.playerState = PlayerController.PlayerState.Run;
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
            PlayerController.playerController.speedmove = h > 0 ? 3f : -3f;
            PlayerController.playerController.dirMove = h > 0 ? false : true;
            if (PlayerController.playerController.playerState == PlayerController.PlayerState.Jump)
                return;
            PlayerController.playerController.playerState = PlayerController.PlayerState.Run;


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
            TryShot();
            PlayerController.playerController.isBouderJoystick = joystick.GetDistance() >= 0.9f;

            if (PlayerController.playerController.autoTarget.Count == 0)
            {
                if (PlayerController.playerController.isBouderJoystick)
                {
                    PlayerController.playerController.FlipX = shootPosition.x < 0;
                }
                PlayerController.playerController.SelectNonTarget(shootPosition);
             //   Debug.LogError("ko co target");
            }
            else
            {
                PlayerController.playerController.SelectTarget();
             //   Debug.LogError(" co target");
            }
        }
        else
        {
            StopShot();
            if (PlayerController.playerController.autoTarget.Count == 0)
            {
                PlayerController.playerController.SelectNonTarget(!PlayerController.playerController.FlipX ? Vector2.right : Vector2.left);
            }
            else
            {
                PlayerController.playerController.SelectTarget();
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
            PlayerController.playerController.TryGrenade();
        }

    }
    public void TryShot()
    {
        PlayerController.playerController.ShootDown();
    }
    public void StopShot()
    {
        PlayerController.playerController.ShootUp();
    }
    public void TryJump()
    {
        PlayerController.playerController.TryJump();
    }
    public void BtnGrenade()
    {
        PlayerController.playerController.TryGrenade();
    }
}
