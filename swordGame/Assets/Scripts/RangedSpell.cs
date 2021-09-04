using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public bool flag = false;
    public GameObject target;
    public GameObject player;
    public float delay = 0f;
    public string tag;
    public Vector3 direction;
    public GameObject player1WinUI;
    public GameObject player2WinUI;
    

    // Start is called before the first frame update
    void Start()
    {
        var targetPosition = new Vector3(target.transform.position.x , target.transform.position.y, target.transform.position.z );
        var vec = targetPosition - transform.position;
        direction = vec / vec.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            transform.position += direction * 5f * Time.deltaTime;

            if (transform.position.x > 20f || transform.position.y > 20f || transform.position.z > 20f)
            {
                Destroy(this.gameObject);
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(target))
        {
            target.GetComponent<PlayerController>().coreLogic.setTrigger("leftAttackReceived");
            target.GetComponent<PlayerController>().coreLogic.areAction = true;
            target.GetComponent<PlayerController>().coreLogic.cooldownAction =  other.GetComponent<PlayerController>().coreLogic.GetCooldown(Core.Movement.ATTACK_RECEIVED);
            target.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.ATTACK_RECEIVED;
            target.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.ATTACK_RECEIVED;
            
            float hit_damage = 10f;
            target.GetComponent<Core>().healthbar.LoseHealth(hit_damage);
            target.GetComponent<Core>().oponentLife -= hit_damage;

            if (target.GetComponent<Core>().oponentLife <= 0f)
            {
                player.GetComponent<PlayerController>().coreLogic.resetTriggers();
                target.GetComponent<PlayerController>().coreLogic.resetTriggers();
                player.GetComponent<PlayerController>().coreLogic.setBool("dance", true);
                player.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.DANCE;
                target.GetComponent<PlayerController>().coreLogic.setBool("fallen", true);
                target.GetComponent<PlayerController>().coreLogic.setTrigger("fallDown");
                target.GetComponent<PlayerController>().coreLogic.movement = Core.Movement.FALLEN;

                if (target.gameObject.CompareTag("player1"))
                {
                    Debug.Log("Player 2 win!");
                    player2WinUI.SetActive(true);
                }
                else
                {
                    Debug.Log("Player 1 win!");
                    player1WinUI.SetActive(true);
                }
            }
            
            Destroy(this.gameObject);
            
            target.GetComponent<Core>().SpellHit();
        }
        
        
        Debug.Log("Hello");
    }
    
}
    
    
