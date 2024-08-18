using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    float fullHP = 454f;
    float fullFP = 93f;
    float fullStamina = 95f;
    float playTime;

    float damageCoolDown = 1f;
    float lastDamageTime;

    public float HP;
    public float FP;
    public float stam;
    public float useStam = 15f;
    public float damage = 90f;

    public bool inAction;

    public Slider hpSlider;
    public Slider fpSlider;
    public Slider stamSlider;
    public GameObject deadScene;
    public HitCheck check;
    public Animator anim;
    public PlayerMove playerMove;

    private bool isBeingIdle = false;

    public enum PlayerState
    {
        idle,
        dash,
        attack,
        defense,
        damaged,
        move,
        dead,
        groggy
    }

    public PlayerState mystate = PlayerState.idle;

    void Start()
    {
        InitializeStats();
        InitializeUI();

        lastDamageTime = -damageCoolDown;
    }

    void Update()
    {
        HandleState();
        UpdateUI();
    }

    // 스탯 수치 설정, 액션 상태 설정
    private void InitializeStats()
    {
        // 기본 스탯 수치를 최대치로 설정한다.
        HP = fullHP;
        FP = fullFP;
        stam = fullStamina;

        // 액션을 하지않는 상태로 체크한다.
        inAction = false;
    }

    // UI에 스탯 수치 적용
    private void InitializeUI()
    {
        // UI스탯의 최대치를 설정한다.
        hpSlider.maxValue = fullHP;
        fpSlider.maxValue = fullFP;
        stamSlider.maxValue = fullStamina;
    }

    // state설정 함수
    private void HandleState()
    {
        switch (mystate)
        {
            case PlayerState.idle:
                inAction = false;
                Idle();
                break;
            case PlayerState.move:
                inAction = false;
                Move();
                break;
            case PlayerState.dash:
                Dash();
                break;
            case PlayerState.attack:
                inAction = true;
                Attack();
                break;
            case PlayerState.defense:
                inAction = true;
                Defense();
                break;
            case PlayerState.damaged:
                inAction = true;
                Damaged();
                break;
            case PlayerState.dead:
                Invoke("Dead", 1f);
                //Dead();
                break;
        }
    }

    // 스탯 수치를 실시간으로 UI에 반영한다.
    private void UpdateUI()
    {
        hpSlider.value = HP;
        fpSlider.value = FP;
        stamSlider.value = stam;
    }

    private void Idle()
    {
        // 스태미너가 풀충상태가 아니라면 회복한다.
        if (stam < fullStamina) Recovery(10);

        // 애니메이션이 재생중이라면 해당 state상태가 된다.
        if (CheckAndSetState("WalkSprintTree", PlayerState.move) || 
            CheckAndSetState("OneHand_Up_Attack_1", PlayerState.attack) || 
            CheckAndSetState("Hit_F_1", PlayerState.damaged) || 
            CheckAndSetState("OneHand_Up_Shield_Block_Hit_1", PlayerState.defense))
        {
            return;
        }
    }

    private void Move()
    {

        #region 스페이스바 입력에 따라 스태미너 조절

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    stam -= 19;
        //}
        //else if (Input.GetKey(KeyCode.Space))
        //{
        //    mystate = PlayerState.dash;
        //}


        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true)
        //{
        //    // WalkSprintTree가 재생이 끝났다면 idle상태가 됨
        //    float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //    if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S) ||
        //        !Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.E))
        //    {
        //        if (animTime >= 1.0f) //애니메이션 플레이 끝
        //        {
        //            mystate = PlayerState.idle;
        //        }
        //    }

        //}
        #endregion

        // 스태미너가 풀충상태가 아니라면 회복한다.
        if (stam < fullStamina) Recovery(10);

        // 현재 moveSpeed가 sprintSpeed만큼 빠르다면 dash상태가 됨
        if (playerMove.moveSpeed > playerMove.runSpeed) 
        {
            StartCoroutine(BeingIdle(PlayerState.dash, 2.0f));
        }
        else if (CheckAndSetState("OneHand_Up_Idle", PlayerState.idle) ||
                CheckAndSetState("OneHand_Up_Attack_1", PlayerState.attack) ||
                CheckAndSetState("Hit_F_1", PlayerState.damaged) ||
                CheckAndSetState("OneHand_Up_Shield_Block_Hit_1", PlayerState.defense))
        {
            return;
        }

        // 구르기 애니메이션이 재생중일 때도 inAction이 작동
        if (IsPlayingAnimation("RollFront")) 
        {
            inAction = true;
        }

        // 추락사가 체크되면 Dead상태가 된다.
        if (playerMove.fallDeath) 
        {
            mystate = PlayerState.dead;
        }
    }

    private void Dash()
    {
        // 지속적으로 스태미너 소모
        Consumption(10);
        // 현재 moveSpeed가 sprintSpeed보다 느리다면 move상태가 됨
        if (playerMove.moveSpeed < playerMove.sprintSpeed)
        {
            mystate = PlayerState.move;
        }
        //if (!Input.GetKey(KeyCode.Space))
        //{
        //    mystate = PlayerState.idle;
        //}
    }

    private void Attack()
    {
        // attack 상태가 되면 공격 애니메이션 시간 만큼 기다린 뒤 idle 상태가 된다.
        if (CheckAndSetState("OneHand_Up_Idle", PlayerState.idle)) 
        {
            return;
        }
    }

    private void Defense()
    {
        if (!Input.GetMouseButton(1))
        {
            mystate = PlayerState.idle;
        }
        //if (stam < fullStamina) Recovery(5);
    }

    private void Damaged()
    {
        if (HP <= 0)
        {
            mystate = PlayerState.dead;
        }
        else
        {
            StartCoroutine(BeingIdle(PlayerState.idle, playTime));
        }
    }

    private void Dead()
    {
        HP = 0;
        fullHP = 0;
    }

    // 현재의 애니메이션 스테이트를 확인
    private bool IsPlayingAnimation(string animationName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    // 해당 애니메이션이 재생 중인가를 확인
    private bool CheckAndSetState(string animationName, PlayerState newState)
    {
        if (IsPlayingAnimation(animationName))
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length; // 애니메이션의 재생 시간이 playTime에 저장된다.
            mystate = newState;
            return true;
        }
        return false;
    }

    // 스태미너가 회복된다.
    private void Recovery(float amount)
    {
        stam = Mathf.Clamp(stam + Time.deltaTime * amount, 0, fullStamina);
    }

    // 스태미너가 지속적으로 소모된다.
    private void Consumption(float amount)
    {
        stam = Mathf.Clamp(stam - Time.deltaTime * amount, 0, fullStamina);
        //if (stam == 0)
        //{
        //    mystate = PlayerState.idle;
        //}
    }

    public void UsingStat(float stat, float amount) 
    {
        print(stat + amount);
        stat -= amount;
    }

    private IEnumerator BeingIdle(PlayerState state, float sec)
    {
        // sec만큼 기다린 뒤 다음 state상태가 된다.
        isBeingIdle = true;
        yield return new WaitForSeconds(sec);
        mystate = state;
        isBeingIdle = false;
    }

    public void SubtractStamia (float amount)
    {
        stam -= amount;

        if(stam < 0)
        {
            mystate = PlayerState.groggy;
            inAction = false;
        }
    }

}
