using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public enum Movement { ATTACK_LEFT, ATTACK_RIGHT, ATTACK_TOP, ATTACK_BOT, BLOCK_LEFT,
        BLOCK_RIGHT, BLOCK_TOP, BLOCK_BOT, DASH_RIGHT, DASH_LEFT, DASH_BACK,
        COMBO_ATTACK1, COMBO_ATTACK2, COMBO_ATTACK3, COMBO_ATTACK4,
        FALL_DOWN, ATTACK_RECEIVED, NO_ACTION, DANCE, FALLEN, END_GAME, SPELL_ATTACK };

    public float MAX_STAMINE = 2f;

    public float moveSpeed;

    public Vector3 defenseSwordRotation;
    public Vector3 attackSwordRotation;

    public CharacterController controller;
    public Animator anmCtrl;
    public GameObject bloodAnimation;
    public GameObject sparkAnimation;

    private Vector3 moveDirection;

    public GameObject player;
    public GameObject targetobj;
    
    public Animator animator;
    public Transform target;
    public Transform weapon;

    public float cooldownAction = 0f;
    public bool areAction = false;
    private float cooldownActionProgress = 0f;
    public Movement movement;
    private Movement[] comboMovements = new Movement[MAX_COMBO];
    public static int MAX_COMBO = 3;
    private int cantCombo = 0;

    public bool hasCollision = false;

    private AudioSource audioSource;

    public AudioClip swordAttack;
    public AudioClip fall;
    public AudioClip swordBlock;
    public AudioClip dash;
    public AudioClip music;
    public AudioClip spellCast;
    public AudioClip spellHit;
    
    [SerializeField] private AudioClip[] walkingclips;
    
    public float oponentLife = 100f;
    public HealthBar healthbar;
    
    public ManaBar manaBar;
 
    public float stamine;
    public StaminaBar staminaBar;

    public GameObject rangedSpellPrefab;

    public GameConfiguration.Difficult botDifficulty; 

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anmCtrl = GetComponent<Animator>();
        stamine = 0f;
        
    }

    void Update()
    {

        if (areAction && cooldownActionProgress < cooldownAction)
        {
            cooldownActionProgress += Time.deltaTime;

            //dashManager
            dashManager();
        }
        else if (areAction && cooldownActionProgress >= cooldownAction && movement != Movement.DANCE && movement != Movement.FALLEN && movement != Movement.END_GAME)
        {
            if (isCombo() && cantCombo > 0)
            {
                if (cantCombo == MAX_COMBO)
                {
                    comboAnimation(GetCombo());
                }
                cantCombo -= 1;
                movement = comboMovements[cantCombo];
                cooldownAction = GetCooldown(GetCombo());
                transform.LookAt(target);

            }
            else
            {
                areAction = false;
                cantCombo = 0;
                anmCtrl.SetBool("noCombo", true);
            }

            cooldownActionProgress = 0;
            hasCollision = false;
        }
        else
        {
            transform.LookAt(target);

        }
    }


    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        setStartConfiguration();

    }

    void setStartConfiguration()
    {

        if (gameObject.CompareTag("player1"))
        {
            if (GameConfiguration.gameConfiguration.isPlayer1Bot)
            {
                gameObject.GetComponent<PlayerController>().enabled = false;
                gameObject.GetComponent<BotController>().enabled = true;
                if (GameConfiguration.gameConfiguration.bot1Difficult == GameConfiguration.Difficult.EASY)
                {
                    gameObject.GetComponent<BotController>().botDifficulty = GameConfiguration.Difficult.EASY;
                }
                else if (GameConfiguration.gameConfiguration.bot1Difficult == GameConfiguration.Difficult.MEDIUM)
                {
                    gameObject.GetComponent<BotController>().botDifficulty = GameConfiguration.Difficult.MEDIUM;
                }
                else if (GameConfiguration.gameConfiguration.bot1Difficult == GameConfiguration.Difficult.HARD)
                {
                    gameObject.GetComponent<BotController>().botDifficulty = GameConfiguration.Difficult.HARD;
                }
            } else
            {
                gameObject.GetComponent<PlayerController>().enabled = true;
                gameObject.GetComponent<BotController>().enabled = false;
            }
        }
        else if (gameObject.CompareTag("player2"))
        {
            if (GameConfiguration.gameConfiguration.isPlayer2Bot)
            {
                gameObject.GetComponent<PlayerController>().enabled = false;
                gameObject.GetComponent<BotController>().enabled = true;

                if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.EASY)
                {
                    gameObject.GetComponent<BotController>().botDifficulty = GameConfiguration.Difficult.EASY;
                }
                else if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.MEDIUM)
                {
                    gameObject.GetComponent<BotController>().botDifficulty = GameConfiguration.Difficult.MEDIUM;
                }
                else if (GameConfiguration.gameConfiguration.bot2Difficult == GameConfiguration.Difficult.HARD)
                {
                    gameObject.GetComponent<BotController>().botDifficulty = GameConfiguration.Difficult.HARD;
                    GameObject ken = gameObject.transform.Find("Ken").gameObject;
                    Renderer rend = ken.GetComponent<Renderer>();
                    rend.material.shader = Shader.Find("Specular");
                    rend.material.SetColor("_SpecColor", Color.red);
                }
            }
            else
            {
                gameObject.GetComponent<PlayerController>().enabled = true;
                gameObject.GetComponent<BotController>().enabled = false;
            }
        }
    }

    public void comboAnimation(Movement mov)
    {
        switch (mov)
        {
            case Movement.COMBO_ATTACK1:
                anmCtrl.SetTrigger("comboAttack1");
                anmCtrl.SetBool("noCombo", false);
                break;
            case Movement.COMBO_ATTACK2:
                anmCtrl.SetTrigger("comboAttack2");
                anmCtrl.SetBool("noCombo", false);
                break;
            case Movement.COMBO_ATTACK3:
                anmCtrl.SetTrigger("comboAttack3");
                anmCtrl.SetBool("noCombo", false);
                break;
            case Movement.COMBO_ATTACK4:
                anmCtrl.SetTrigger("comboAttack4");
                anmCtrl.SetBool("noCombo", false);
                break;
        }
    }

    public bool isCombo()
    {
        if (GetCombo() != Movement.NO_ACTION)
        {
            return true;
        }

        return false;
    }


    public Movement GetCombo()
    {
        if (comboMovements[0] == Movement.ATTACK_TOP && comboMovements[1] == Movement.ATTACK_BOT && comboMovements[2] == Movement.ATTACK_BOT)
        {
            return Movement.COMBO_ATTACK1;
        }
        if (comboMovements[0] == Movement.ATTACK_RIGHT && comboMovements[1] == Movement.ATTACK_LEFT && comboMovements[2] == Movement.ATTACK_RIGHT)
        {
            return Movement.COMBO_ATTACK2;
        }
        if (comboMovements[0] == Movement.DASH_RIGHT && comboMovements[1] == Movement.ATTACK_RIGHT && comboMovements[2] == Movement.ATTACK_RIGHT)
        {
            return Movement.COMBO_ATTACK3;
        }
        if (comboMovements[0] == Movement.DASH_LEFT && comboMovements[1] == Movement.ATTACK_LEFT && comboMovements[2] == Movement.ATTACK_LEFT)
        {
            return Movement.COMBO_ATTACK4;
        }
        return Movement.NO_ACTION;
    }

    public void setNextComboMovement(Movement mov)
    {
        if (cantCombo < MAX_COMBO)
        {
            comboMovements[cantCombo] = mov;
            cantCombo += 1;
        }
    }


    public void dashManager()
    {
        if (movement == Movement.DASH_RIGHT)
        {
            DashRightAction();
        }
        else if (movement == Movement.DASH_LEFT)
        {
            DashLeftAction();
        }
        else if (movement == Movement.DASH_BACK)
        {
            DashBackwardAction();
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }



    public void LeftBlock()
    {
        NotMoving();
        if (!areAction)
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.BLOCK_LEFT);
            movement = Movement.BLOCK_LEFT;
            anmCtrl.SetTrigger("blockLeft");
            weapon.localRotation = Quaternion.Euler(defenseSwordRotation);
        }
    }

    public void LeftAttack()
    {
        NotMoving();
        if (!areAction)
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.ATTACK_LEFT);
            movement = Movement.ATTACK_LEFT;
            anmCtrl.SetTrigger("attackLeft");
            weapon.localRotation = Quaternion.Euler(attackSwordRotation);
        }

    }

    public void RightBlock()
    {
        NotMoving();
        if (!areAction)
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.BLOCK_RIGHT);
            movement = Movement.BLOCK_RIGHT;
            anmCtrl.SetTrigger("blockRight");
            weapon.localRotation = Quaternion.Euler(defenseSwordRotation);
        }
    }

    public void RightAttack()
    {
        NotMoving();
        if (!areAction)
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.ATTACK_RIGHT);
            movement = Movement.ATTACK_RIGHT;
            anmCtrl.SetTrigger("attackRight");
            weapon.localRotation = Quaternion.Euler(attackSwordRotation);
        }

    }

    public void DownBlock()
    {
        NotMoving();
        if (!areAction)
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.BLOCK_BOT);
            movement = Movement.BLOCK_BOT;
            anmCtrl.SetTrigger("blockBottom");
            weapon.localRotation = Quaternion.Euler(defenseSwordRotation);
        }
    }

    public void DownAttack()
    {
        NotMoving();
        if (!areAction)
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.ATTACK_BOT);
            movement = Movement.ATTACK_BOT;
            anmCtrl.SetTrigger("attackBottom");
            weapon.localRotation = Quaternion.Euler(attackSwordRotation);
        }
    }

    public void UpBlock()
    {
        NotMoving();
        if (!areAction)
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.BLOCK_TOP);
            movement = Movement.BLOCK_TOP;
            anmCtrl.SetTrigger("blockTop");
            weapon.localRotation = Quaternion.Euler(defenseSwordRotation);
        }
    }


    public void UpAttack()
    {
        NotMoving();
        if (!areAction)
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.ATTACK_TOP);
            movement = Movement.ATTACK_TOP;
            anmCtrl.SetTrigger("attackTop");
            weapon.localRotation = Quaternion.Euler(attackSwordRotation);
        }
    }

    public void DashRight()
    {
        if (!areAction)
        {
            anmCtrl.SetTrigger("dashRight");
            decrementStamine();

            areAction = true;
            cooldownAction = GetCooldown(Movement.DASH_RIGHT);
            movement = Movement.DASH_RIGHT;
            PlaySound(dash);
        }
    }

    public void DashRightAction()
    {
        var vec = target.position - transform.position;
        var direction = vec / vec.magnitude;

        Vector3 orthVec = Vector3.Cross(direction, Vector3.up).normalized;
        controller.Move(orthVec * 4.0f * -Time.deltaTime);

        var newVec = target.position - transform.position;

        var dif = newVec.magnitude - vec.magnitude;

        direction = newVec / newVec.magnitude;

        controller.Move(direction * dif);
    }

    public void WalkRight()
    {
        if (!areAction)
        {

            var vec = target.position - transform.position;
            var dist = vec.magnitude;
            var direction = vec / dist;
            Vector3 orthVec = Vector3.Cross(direction, Vector3.up).normalized;
            var auxMoveSpeed = (dist < 5f) ? moveSpeed * 0.2f : moveSpeed;

            controller.Move(orthVec * -auxMoveSpeed * Time.deltaTime);

            var newVec = target.position - transform.position;

            var dif = newVec.magnitude - dist;

            direction = newVec / newVec.magnitude;

            controller.Move(direction * dif);

            anmCtrl.SetBool("walkRight", true);
            anmCtrl.SetBool("walkLeft", false);
            anmCtrl.SetBool("walkForward", false);
            anmCtrl.SetBool("walkBackward", false);
            anmCtrl.SetBool("runForward", false);
        }

    }

    public void DashLeft()
    {

        if (!areAction)
        {
            anmCtrl.SetTrigger("dashLeft");
            decrementStamine();

            areAction = true;
            cooldownAction = GetCooldown(Movement.DASH_LEFT);
            movement = Movement.DASH_LEFT;
            PlaySound(dash);

        }
    }

    public void DashLeftAction()
    {
        var vec = target.position - transform.position;
        var direction = vec / vec.magnitude;

        Vector3 orthVec = Vector3.Cross(direction, Vector3.up).normalized;
        controller.Move(orthVec * 4.0f * Time.deltaTime);

        var newVec = target.position - transform.position;

        var dif = newVec.magnitude - vec.magnitude;

        direction = newVec / newVec.magnitude;

        controller.Move(direction * dif);
    }

    public void WalkLeft()
    {
        if (!areAction)
        {
            var vec = target.position - transform.position;
            var dist = vec.magnitude;
            var direction = vec / dist;

            Vector3 orthVec = Vector3.Cross(direction, Vector3.up).normalized;
            var auxMoveSpeed = (dist < 5f) ? moveSpeed * 0.2f : moveSpeed;

            controller.Move(orthVec * auxMoveSpeed * Time.deltaTime);

            var newVec = target.position - transform.position;

            var dif = newVec.magnitude - dist;

            direction = newVec / newVec.magnitude;

            controller.Move(direction * dif);

            anmCtrl.SetBool("walkLeft", true);
            anmCtrl.SetBool("walkRight", false);
            anmCtrl.SetBool("walkForward", false);
            anmCtrl.SetBool("walkBackward", false);
            anmCtrl.SetBool("runForward", false);
        }

    }

    public void DashBackward()
    {
        if (!areAction)
        {
            anmCtrl.SetTrigger("dashBackward");
            decrementStamine();

            areAction = true;
            cooldownAction = GetCooldown(Movement.DASH_BACK);
            movement = Movement.DASH_BACK;
            PlaySound(dash);

        }
    }

    public void DashBackwardAction()
    {
        var vec = target.position - transform.position;
        var direction = vec / vec.magnitude;

        controller.Move(direction * 4.0f * -Time.deltaTime);
    }

    public void WalkBackward()
    {
        if (!areAction)
        {
            var vec = target.position - transform.position;
            var dist = vec.magnitude;
            var direction = vec / dist;
            var auxMoveSpeed = (dist < 5f) ? moveSpeed * 0.1f : moveSpeed;

            controller.Move(direction * -auxMoveSpeed * Time.deltaTime);

            anmCtrl.SetBool("walkBackward", true);
            anmCtrl.SetBool("walkRight", false);
            anmCtrl.SetBool("walkLeft", false);
            anmCtrl.SetBool("walkForward", false);
            anmCtrl.SetBool("runForward", false);

        }
    }

    public void WalkForward()
    {
        if (!areAction)
        {
            var vec = target.position - transform.position;
            var direction = vec / vec.magnitude;

            controller.Move(direction * moveSpeed * Time.deltaTime);

            anmCtrl.SetBool("walkForward", true);
            anmCtrl.SetBool("walkRight", false);
            anmCtrl.SetBool("walkLeft", false);
            anmCtrl.SetBool("walkBackward", false);
            anmCtrl.SetBool("runForward", false);
        }


    }

    public void RunForward()
    {

        var vec = target.position - transform.position;
        var direction = vec / vec.magnitude;


        controller.Move(direction * moveSpeed * 2 * Time.deltaTime);
        anmCtrl.SetBool("runForward", true);
        anmCtrl.SetBool("walkRight", false);
        anmCtrl.SetBool("walkLeft", false);
        anmCtrl.SetBool("walkForward", false);
        anmCtrl.SetBool("walkBackward", false);

    }

    public void NotMoving()
    {
        anmCtrl.SetBool("walkRight", false);
        anmCtrl.SetBool("walkLeft", false);
        anmCtrl.SetBool("walkForward", false);
        anmCtrl.SetBool("walkBackward", false);
        anmCtrl.SetBool("runForward", false);

    }

    public void setHasCollisionTrue()
    {
        hasCollision = true;
    }

    public void setTrigger(string trigger)
    {
        anmCtrl.SetTrigger(trigger);
    }

    public void setBool(string trigger, bool val)
    {
        anmCtrl.SetBool(trigger, val);
    }

    public static bool isAttack(Movement player_mov)
    {
        switch (player_mov)
        {
            case Movement.ATTACK_LEFT:
            case Movement.ATTACK_RIGHT:
            case Movement.ATTACK_TOP:
            case Movement.ATTACK_BOT:
                return true;
        }
        return false;
    }
    public static bool isHit(Movement player_mov, Movement target_mov)
    {
        if(target_mov == Movement.FALL_DOWN || target_mov == Movement.ATTACK_RECEIVED)
        {
            return false;
        }

        switch (player_mov)
        {
            case Movement.ATTACK_LEFT:
                if (target_mov != Movement.BLOCK_LEFT && target_mov != Movement.DASH_LEFT && target_mov != Movement.DASH_RIGHT && target_mov != Movement.DASH_BACK)
                {
                    return true;
                }
                break;
            case Movement.ATTACK_RIGHT:
                if (target_mov != Movement.BLOCK_RIGHT && target_mov != Movement.DASH_LEFT && target_mov != Movement.DASH_RIGHT && target_mov != Movement.DASH_BACK)
                {
                    return true;
                }
                break;
            case Movement.ATTACK_TOP:
                if (target_mov != Movement.BLOCK_TOP && target_mov != Movement.DASH_LEFT && target_mov != Movement.DASH_RIGHT && target_mov != Movement.DASH_BACK)
                {
                    return true;
                }
                break;
            case Movement.ATTACK_BOT:
                if (target_mov != Movement.BLOCK_BOT && target_mov != Movement.DASH_LEFT && target_mov != Movement.DASH_RIGHT && target_mov != Movement.DASH_BACK)
                {
                    return true;
                }
                break;
        }

        return false;
    }

    public static bool blockAttack(Movement player_mov, Movement target_mov)
    {
        switch (player_mov)
        {
            case Movement.ATTACK_LEFT:
                if (target_mov == Movement.BLOCK_LEFT)
                {
                    return true;
                }
                break;
            case Movement.ATTACK_RIGHT:
                if (target_mov == Movement.BLOCK_RIGHT)
                {
                    return true;
                }
                break;
            case Movement.ATTACK_TOP:
                if (target_mov == Movement.BLOCK_TOP)
                {
                    return true;
                }
                break;
            case Movement.ATTACK_BOT:
                if (target_mov == Movement.BLOCK_BOT)
                {
                    return true;
                }
                break;
        }

        return false;
    }

   
    public void BlockSpell()
    {
        
    }

    public void CastSpell()
    {
        NotMoving();
        if (!areAction)
        {
            StartCoroutine(PauseCastSpell());
        }
        
    }

    IEnumerator PauseCastSpell()
    {
        if (manaBar.mana.SpendMana(50))
        {
            areAction = true;
            cooldownAction = GetCooldown(Movement.SPELL_ATTACK);
            movement = Movement.SPELL_ATTACK;
            anmCtrl.SetTrigger("castSpell");
            SpellCast();
            
            yield return  new WaitForSeconds(0.5f);
            SpawnSpell();
        }
        
        
    }
    
    void SpawnSpell()
    {
        Vector3 SpawnSpellLoc = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        
        GameObject instance = Instantiate(rangedSpellPrefab, SpawnSpellLoc, Quaternion.identity);
        
        
        instance.GetComponent<RangedSpell>().target = targetobj;
        instance.GetComponent<RangedSpell>().player = player;
        
        
        instance.SetActive(true);
      
        instance.GetComponent<Collider>().enabled = true;
        instance.GetComponent<Collider>().isTrigger = true;
        
        instance.GetComponent<RangedSpell>().flag = true;
    }
    
    public void incrementStamine()
    {
        if (!fullstamine())
        {
            float amount = 1f;
            stamine += amount;
            staminaBar.RegenStamina(amount);
        }
    }

    public void decrementStamine()
    {
        if (havestamine())
        {
            float amount = 1f;
            stamine -= amount;
            staminaBar.SpendStamina(amount);
        }
    }

    public bool havestamine()
    {
        return stamine > 0f;
    }

    public bool fullstamine()
    {
        return stamine == MAX_STAMINE;
    }

    public float GetCooldown(Movement mov)
    {
        switch (mov)
        {
            case Movement.ATTACK_RECEIVED:
                return 0.31f;
            case Movement.FALL_DOWN:
                return 4.0f;
            case Movement.COMBO_ATTACK1:
                return 0.4f;
            case Movement.COMBO_ATTACK2:
                return 0.4f;
            case Movement.COMBO_ATTACK3:
                return 0.4f;
            case Movement.COMBO_ATTACK4:
                return 0.4f;
            case Movement.ATTACK_LEFT:
                return 0.55f;
                //return 0.52f;
            case Movement.ATTACK_RIGHT:
                return 0.55f;
            case Movement.ATTACK_TOP:
                return 0.55f;
            case Movement.ATTACK_BOT:
                return 0.55f;
            case Movement.BLOCK_LEFT:
                return 0.4f;
                //return 0.38f;
            case Movement.BLOCK_RIGHT:
                return 0.4f;
            case Movement.BLOCK_TOP:
                return 0.4f;
            case Movement.BLOCK_BOT:
                return 0.4f;
            case Movement.DASH_RIGHT:
                return 0.4f;
                //return 0.39f;
            case Movement.DASH_LEFT:
                return 0.4f;
            case Movement.DASH_BACK:
                return 0.4f;
            case Movement.SPELL_ATTACK:
                return 1.04f;
        }
        return 0;
    }


    public void SpellCast()
    {
        audioSource.PlayOneShot(spellCast);
    }
    
    public void SpellHit()
    {
        audioSource.PlayOneShot(spellHit);
    }
    
    public void Step()
    {
        AudioClip clip = GetRandomClip();
        audioSource.PlayOneShot(clip);
    }

    public void Music()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(music);
        }

    }

    public void Fall()
    {
        audioSource.PlayOneShot(fall);

    }

    public AudioClip GetRandomClip()
    {
        return walkingclips[UnityEngine.Random.Range(0, walkingclips.Length)];
    }


    public void resetTriggers()
    {
        anmCtrl.ResetTrigger("attackRight");
        anmCtrl.ResetTrigger("attackLeft");
        anmCtrl.ResetTrigger("attackTop");
        anmCtrl.ResetTrigger("attackBottom");
        anmCtrl.ResetTrigger("blockRight");
        anmCtrl.ResetTrigger("blockLeft");
        anmCtrl.ResetTrigger("blockTop");
        anmCtrl.ResetTrigger("blockBottom");
        anmCtrl.ResetTrigger("leftAttackReceived");
        anmCtrl.ResetTrigger("dashLeft");
        anmCtrl.ResetTrigger("dashRight");
        anmCtrl.ResetTrigger("dashBackward");
        anmCtrl.ResetTrigger("comboAttack1");
        anmCtrl.ResetTrigger("comboAttack2");
        anmCtrl.SetBool("walkRight", false);
        anmCtrl.SetBool("walkLeft", false);
        anmCtrl.SetBool("walkForward", false);
        anmCtrl.SetBool("walkBackward", false);
        anmCtrl.SetBool("runForward", false);
    }
}
