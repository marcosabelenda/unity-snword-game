using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public string tag;
    public GameObject target;
    public GameObject player;

    private static readonly float DELTA_HIT_TIME = 4f;
    private float lastHitTime = 0f;
    private int hitTimes = 0;
    public GameObject player1WinUI;
    public GameObject player2WinUI;
    public GameObject canvas;
    
    private void Update()
    {

    }

    private void HitOpponent(PlayerController attacker, PlayerController attacked)
    {
        float hit_damage = 0f;
        switch (attacker.coreLogic.movement)
        {
            case Core.Movement.ATTACK_LEFT:
                hit_damage = 5f;
                break;
            case Core.Movement.ATTACK_RIGHT:
                hit_damage = 6f;
                break;
            case Core.Movement.ATTACK_TOP:
                hit_damage = 13f;
                break;
            case Core.Movement.ATTACK_BOT:
                hit_damage = 11f;
                break;

        }

        target.GetComponent<Core>().healthbar.LoseHealth(hit_damage);
        Debug.Log("Damage: -" + hit_damage);
        target.GetComponent<Core>().oponentLife -= hit_damage;
        FallDownHandler(attacked, attacker);

    }

    private void FallDownHandler(PlayerController attacked, PlayerController attacker)
    {
        Debug.Log("Time betwen hits:" + (Time.time - lastHitTime));
        attacker.coreLogic.PlaySound(player.GetComponent<PlayerController>().coreLogic.swordAttack);
        attacked.coreLogic.bloodAnimation.GetComponent<ParticleSystem>().Play();

        if (hitTimes == 0 || Time.time - lastHitTime < DELTA_HIT_TIME)
        {
            lastHitTime = Time.time;
            hitTimes++;
            Debug.Log("HitTimes:" + hitTimes);
            if (hitTimes >= 3)
            {
                Debug.Log("Playing fallDown Animation");
                attacked.coreLogic.setTrigger("leftAttackReceived");

                attacked.coreLogic.setTrigger("fallDown");
                attacked.coreLogic.areAction = true;
                attacked.coreLogic.cooldownAction = attacked.coreLogic.GetCooldown(Core.Movement.FALL_DOWN);
                attacked.coreLogic.movement = Core.Movement.FALL_DOWN;
                hitTimes = 0;
            }
            else
            {
                attacked.coreLogic.setTrigger("leftAttackReceived");
                attacked.coreLogic.areAction = true;
                attacked.coreLogic.cooldownAction = attacked.coreLogic.GetCooldown(Core.Movement.ATTACK_RECEIVED);
                attacked.coreLogic.movement = Core.Movement.ATTACK_RECEIVED;
            }
        }
        else
        {
            attacked.coreLogic.setTrigger("leftAttackReceived");
            attacked.coreLogic.areAction = true;
            attacked.coreLogic.cooldownAction = attacked.coreLogic.GetCooldown(Core.Movement.ATTACK_RECEIVED);
            attacked.coreLogic.movement = Core.Movement.ATTACK_RECEIVED;
            lastHitTime = Time.time;
            hitTimes = 0;
        }
    }

    private void GameInfoHandler(Collider other)
    {
        if (other.gameObject.CompareTag("player1"))
        {
            Debug.Log("Player 2 hit Player 1, Player 1 life: " + target.GetComponent<Core>().oponentLife);
        }
        else
        {
            Debug.Log("Player 1 hit Player 2, Player 2 life: " + target.GetComponent<Core>().oponentLife);
        }
        if (target.GetComponent<Core>().oponentLife <= 0)
        {
            player.GetComponent<PlayerController>().coreLogic.resetTriggers();
            other.GetComponent<PlayerController>().coreLogic.resetTriggers();
            player.GetComponent<PlayerController>().coreLogic.setBool("dance", true);
            player.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.DANCE;
            other.GetComponent<PlayerController>().coreLogic.setBool("fallen", true);
            other.GetComponent<PlayerController>().coreLogic.setTrigger("fallDown");
            other.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.FALLEN;

            if (other.gameObject.CompareTag("player1"))
            {
                Debug.Log("Player 2 win!");
                player2WinUI.SetActive(true);
                canvas.GetComponent<NewGame>().activateRestart();
            }
            else
            {
                Debug.Log("Player 1 win!");
                player1WinUI.SetActive(true);
                if (GameConfiguration.gameConfiguration.gameType == GameConfiguration.GameType.SINGLEPLAYER)
                {
                    canvas.GetComponent<NewGame>().activateNextGame();
                }
                else
                {
                    canvas.GetComponent<NewGame>().activateRestart();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag) && !player.GetComponent<PlayerController>().coreLogic.hasCollision && player.GetComponent<PlayerController>().coreLogic.areAction && Core.isAttack(player.GetComponent<PlayerController>().coreLogic.movement))
        {
            player.GetComponent<PlayerController>().coreLogic.setHasCollisionTrue();
            if (target.GetComponent<PlayerController>().coreLogic.areAction)
            {
                if (Core.isHit(player.GetComponent<PlayerController>().coreLogic.movement, other.GetComponent<PlayerController>().coreLogic.movement))
                {
                    HitOpponent(player.GetComponent<PlayerController>(), other.gameObject.GetComponent<PlayerController>());
                    GameInfoHandler(other);
                }
                else
                {
                    if (Core.blockAttack(player.GetComponent<PlayerController>().coreLogic.movement, other.GetComponent<PlayerController>().coreLogic.movement))
                    {
                        other.GetComponent<PlayerController>().coreLogic.incrementStamine();
                        target.GetComponent<PlayerController>().coreLogic.sparkAnimation.GetComponent<ParticleSystem>().Play();
                        player.GetComponent<PlayerController>().coreLogic.PlaySound(player.GetComponent<PlayerController>().coreLogic.swordBlock);
                    }

                    Debug.Log("Bloqueo");
                }
            }
            else
            {
                HitOpponent(player.GetComponent<PlayerController>(), other.gameObject.GetComponent<PlayerController>());
                GameInfoHandler(other);
            }

        }
    }
    
    
}

