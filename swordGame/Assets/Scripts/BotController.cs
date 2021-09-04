using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public KeyCode modifier;
    public KeyCode rightaction;
    public KeyCode leftaction;
    public KeyCode upaction;
    public KeyCode downaction;

    public Core coreLogic;
    public Transform target;

    public GameObject botSword;
        
    public float TIME_WALK_EASY = 1f;
    public float TIME_WALK_MEDIUM = 0.7f;
    public float currentTimeWalk = 0;
    public bool walkBackward = false;

    public GameConfiguration.Difficult botDifficulty = GameConfiguration.Difficult.EASY;

    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if(botDifficulty == GameConfiguration.Difficult.EASY)
        {
            botSword.GetComponent<ParticleSystem>().Stop();
            easyBot();
        }
        if (botDifficulty == GameConfiguration.Difficult.MEDIUM)
        {
            
            mediumBot();
        }
        if (botDifficulty == GameConfiguration.Difficult.HARD)
        {
            hardBot();
        }

    }

    void easyBot()
    {
        var vec = target.position - transform.position;
        var dist = vec.magnitude;
        if(walkBackward)
        {
            if(currentTimeWalk < TIME_WALK_EASY)
            {
                coreLogic.WalkBackward();
                currentTimeWalk += Time.deltaTime;
            } else
            {
                currentTimeWalk = 0;
                walkBackward = false;
            }
        }
        else if (dist > 2f)
        {
            coreLogic.WalkForward();
        }
        else
        {
            var randDefense = Random.Range(0f, 1f);
            if (Input.GetKeyDown(rightaction))
            {
                if (!Input.GetKey(modifier) && randDefense > 0.9f)
                {
                    coreLogic.RightBlock();
                }
            }
            else if (Input.GetKeyDown(leftaction))
            {
                if (!Input.GetKey(modifier) && randDefense > 0.9f)
                {
                    if (Input.GetKeyDown(leftaction))
                        coreLogic.setNextComboMovement(Core.Movement.BLOCK_LEFT);
                    coreLogic.LeftBlock();
                }

            }
            else if (Input.GetKeyDown(upaction))
            {
                if (!Input.GetKey(modifier) && randDefense > 0.9f)
                {
                    if (Input.GetKeyDown(upaction))
                        coreLogic.setNextComboMovement(Core.Movement.BLOCK_TOP);
                    coreLogic.UpBlock();
                }

            }
            else if (Input.GetKeyDown(downaction))
            {
                if (!Input.GetKey(modifier) && randDefense > 0.9f)
                {
                    if (Input.GetKeyDown(downaction))
                        coreLogic.setNextComboMovement(Core.Movement.BLOCK_BOT);
                    coreLogic.DownBlock();
                }

            }
            else
            {
                var randAttack = Random.Range(0f, 1f);
                if (randAttack < 0.01)
                {
                    /*if ()
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_RIGHT);*/
                    coreLogic.RightAttack();
                }
                else if (randAttack > 0.99)
                {
                    /*if (Input.GetKeyDown(leftaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_LEFT);*/
                    coreLogic.LeftAttack();
                }
                else if (randAttack > 0.57 && randAttack < 0.58)
                {
                    /*if (Input.GetKeyDown(upaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_TOP);*/
                    coreLogic.UpAttack();
                }
                else if (randAttack > 0.38 && randAttack < 0.39)
                {
                    /*if (Input.GetKeyDown(downaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_BOT);*/
                    coreLogic.DownAttack();
                }
                else if (randAttack > 0.1 && randAttack < 0.2)
                {
                    walkBackward = true;
                    coreLogic.WalkBackward();

                }
                else
                {
                    coreLogic.NotMoving();

                }
        }
        }
    }

    void mediumBot()
    {
        var vec = target.position - transform.position;
        var dist = vec.magnitude;
        if (walkBackward)
        {
            if (currentTimeWalk < TIME_WALK_MEDIUM)
            {
                coreLogic.WalkBackward();
                currentTimeWalk += Time.deltaTime;
            }
            else
            {
                currentTimeWalk = 0;
                walkBackward = false;
            }
        }
        else if (dist > 2f)
        {
            coreLogic.WalkForward();
        }
        else
        {
            var randDefense = Random.Range(0f, 1f);
            if (Input.GetKeyDown(rightaction))
            {
                if (!Input.GetKey(modifier) && randDefense < 0.8f)
                {
                    coreLogic.RightBlock();
                }
            }
            else if (Input.GetKeyDown(leftaction))
            {
                if (!Input.GetKey(modifier) && randDefense < 0.8f)
                {
                    if (Input.GetKeyDown(leftaction))
                        coreLogic.setNextComboMovement(Core.Movement.BLOCK_LEFT);
                    coreLogic.LeftBlock();
                }

            }
            else if (Input.GetKeyDown(upaction))
            {
                if (!Input.GetKey(modifier) && randDefense < 0.8f)
                {
                    if (Input.GetKeyDown(upaction))
                        coreLogic.setNextComboMovement(Core.Movement.BLOCK_TOP);
                    coreLogic.UpBlock();
                }

            }
            else if (Input.GetKeyDown(downaction))
            {
                if (!Input.GetKey(modifier) && randDefense < 0.8f)
                {
                    if (Input.GetKeyDown(downaction))
                        coreLogic.setNextComboMovement(Core.Movement.BLOCK_BOT);
                    coreLogic.DownBlock();
                }

            }
            else
            {
                var randAttack = Random.Range(0f, 1f);
                if (randAttack < 0.01)
                {
                    /*if ()
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_RIGHT);*/
                    coreLogic.RightAttack();
                }
                else if (randAttack > 0.99)
                {
                    /*if (Input.GetKeyDown(leftaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_LEFT);*/
                    coreLogic.LeftAttack();
                }
                else if (randAttack > 0.57 && randAttack < 0.58)
                {
                    /*if (Input.GetKeyDown(upaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_TOP);*/
                    coreLogic.UpAttack();
                }
                else if (randAttack > 0.38 && randAttack < 0.39)
                {
                    /*if (Input.GetKeyDown(downaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_BOT);*/
                    coreLogic.DownAttack();
                }
                else if (randAttack > 0.19 && randAttack < 0.2)
                {
                    walkBackward = true;
                    coreLogic.WalkBackward();

                }
                else if (randAttack > 0.26 && randAttack < 0.28)
                {
                    /*if (Input.GetKeyDown(downaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_BOT);*/
                    coreLogic.CastSpell();
                }
                else
                {
                    coreLogic.NotMoving();

                }
            }
        }
    }

    void hardBot()
    {
        var vec = target.position - transform.position;
        var dist = vec.magnitude;
        if (dist > 2f)
        {
            coreLogic.WalkForward();
        }
        else
        {
            var randDefense = Random.Range(0f, 1f);
            if (Input.GetKeyDown(rightaction))
            {
                if (!Input.GetKey(modifier) && randDefense < 0.9)
                {
                    if (coreLogic.havestamine() && randDefense > 0.75)
                    {
                        coreLogic.RightBlock();
                        coreLogic.DashRight();
                        coreLogic.RightAttack();
                    }
                    else
                    {
                        coreLogic.RightBlock();
                    }
                }
            }
            else if (Input.GetKeyDown(leftaction))
            {
                if (!Input.GetKey(modifier) && randDefense < 0.9)
                {
                    if (coreLogic.havestamine() && randDefense > 0.75)
                    {
                        coreLogic.LeftBlock();
                        coreLogic.DashLeft();
                        coreLogic.LeftAttack();
                    }
                    else
                    {
                        coreLogic.LeftBlock();
                    }
                }

            }
            else if (Input.GetKeyDown(upaction))
            {
                if (!Input.GetKey(modifier) && randDefense < 0.9)
                {
                    coreLogic.UpBlock();
                }

            }
            else if (Input.GetKeyDown(downaction))
            {
                if (!Input.GetKey(modifier) && randDefense < 0.9)
                {
                    if (coreLogic.havestamine() && randDefense > 0.75)
                    {
                        coreLogic.DownBlock();
                        coreLogic.DashBackward();
                        coreLogic.DownAttack();

                    }
                    else
                    {
                        coreLogic.DownBlock();
                    }
                }

            }
            else
            {
                var randAttack = Random.Range(0f, 1f);
                if (randAttack < 0.03)
                {
                    /*if ()
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_RIGHT);*/
                    coreLogic.RightAttack();
                }
                else if (randAttack > 0.97)
                {
                    /*if (Input.GetKeyDown(leftaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_LEFT);*/
                    coreLogic.LeftAttack();
                }
                else if (randAttack > 0.57 && randAttack < 0.6)
                {
                    /*if (Input.GetKeyDown(upaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_TOP);*/
                    coreLogic.UpAttack();
                }
                else if (randAttack > 0.37 && randAttack < 0.4)
                {
                    /*if (Input.GetKeyDown(downaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_BOT);*/
                    coreLogic.DownAttack();
                }
                else if (randAttack > 0.27 && randAttack < 0.28)
                {
                    /*if (Input.GetKeyDown(downaction))
                        coreLogic.setNextComboMovement(Core.Movement.ATTACK_BOT);*/
                    coreLogic.CastSpell();
                }
                else
                {
                    coreLogic.NotMoving();

                }
            }
        }
    }

 }
