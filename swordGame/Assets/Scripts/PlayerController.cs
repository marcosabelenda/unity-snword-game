using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public KeyCode forward;
    public KeyCode backward;
    public KeyCode right;
    public KeyCode left;
    public KeyCode modifier;
    public KeyCode rightaction;
    public KeyCode leftaction;
    public KeyCode upaction;
    public KeyCode downaction;
    public KeyCode spell;

    public Core coreLogic;

    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(rightaction))
        {
            if (Input.GetKey(modifier))
            {
                if (Input.GetKeyDown(modifier))
                    coreLogic.setNextComboMovement(Core.Movement.BLOCK_RIGHT);
                coreLogic.RightBlock();
            }
            else
            {
                if (Input.GetKeyDown(rightaction))
                    coreLogic.setNextComboMovement(Core.Movement.ATTACK_RIGHT);
                coreLogic.RightAttack();
            }
        }
        else if (Input.GetKey(leftaction))
        {
            if (Input.GetKey(modifier))
            {
                if (Input.GetKeyDown(modifier))
                    coreLogic.setNextComboMovement(Core.Movement.BLOCK_LEFT);
                coreLogic.LeftBlock();
            }
            else
            {
                if (Input.GetKeyDown(leftaction))
                    coreLogic.setNextComboMovement(Core.Movement.ATTACK_LEFT);
                coreLogic.LeftAttack();
            }
        }
        else if (Input.GetKey(upaction))
        {
            if (Input.GetKey(modifier))
            {
                if (Input.GetKeyDown(modifier))
                    coreLogic.setNextComboMovement(Core.Movement.BLOCK_TOP);
                coreLogic.UpBlock();
            }
            else
            {
                if (Input.GetKeyDown(upaction))
                    coreLogic.setNextComboMovement(Core.Movement.ATTACK_TOP);
                coreLogic.UpAttack();
            }

        }
        else if (Input.GetKey(downaction))
        {
            if (Input.GetKey(modifier))
            {
                if (Input.GetKeyDown(modifier))
                    coreLogic.setNextComboMovement(Core.Movement.BLOCK_BOT);
                coreLogic.DownBlock();
            }
            else
            {
                if (Input.GetKeyDown(downaction))
                    coreLogic.setNextComboMovement(Core.Movement.ATTACK_BOT);
                coreLogic.DownAttack();
            }

        }
        else if (Input.GetKey(spell))
        {
            if (Input.GetKey(modifier))
            {
                //coreLogic.BlockSpell();
            }
            else
            {
                if (Input.GetKeyDown(spell))
                    coreLogic.setNextComboMovement(Core.Movement.SPELL_ATTACK);
                coreLogic.CastSpell();
            }

        }
        else if (Input.GetKey(forward))
        {
            if (Input.GetKey(modifier))
            {
                coreLogic.RunForward();
            }
            else
                coreLogic.WalkForward();
        }
        else if (Input.GetKey(backward))
        {
            if (Input.GetKeyDown(modifier) && coreLogic.havestamine())
            {
                if (Input.GetKeyDown(modifier))
                    coreLogic.setNextComboMovement(Core.Movement.DASH_BACK);
                coreLogic.DashBackward();
            }
            else
                coreLogic.WalkBackward();

        }
        else if (Input.GetKey(right))
        {
            if (Input.GetKeyDown(modifier) && coreLogic.havestamine())
            {
                if (Input.GetKeyDown(modifier))
                    coreLogic.setNextComboMovement(Core.Movement.DASH_RIGHT);
                coreLogic.DashRight();

            }
            else
                coreLogic.WalkRight();

        }
        else if (Input.GetKey(left))
        {
            if (Input.GetKeyDown(modifier) && coreLogic.havestamine())
            {
                if (Input.GetKeyDown(modifier))
                    coreLogic.setNextComboMovement(Core.Movement.DASH_LEFT);
                coreLogic.DashLeft();
            }
            else
                coreLogic.WalkLeft();

        }
        else {
            coreLogic.NotMoving();
        }

    }



}
