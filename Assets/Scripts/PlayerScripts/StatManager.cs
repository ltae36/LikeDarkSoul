using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    float fullHP = 454f;
    float fullFP = 93f;
    float fullStamina = 95f;
    float playTime;

    public float HP;
    public float FP;
    public float stam;
    public bool inAction;

    public Slider hpSlider;
    public Slider fpSlider;
    public Slider stamSlider;
    public GameObject deadScene;
    public HitCheck check;
    public Animator anim;

    private bool isBeingIdle = false;

    public enum PlayerState
    {
        idle,
        dash,
        attack,
        defense,
        damaged,
        move,
        dead
    }

    public PlayerState mystate = PlayerState.idle;

    void Start()
    {
        InitializeStats();
        InitializeUI();
    }

    void Update()
    {
        HandleState();
        UpdateUI();

        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            mystate = PlayerState.dead;
        }
    }

    private void InitializeStats()
    {
        HP = fullHP;
        FP = fullFP;
        stam = fullStamina;
        inAction = false;
    }

    private void InitializeUI()
    {
        deadScene.SetActive(false);
        hpSlider.maxValue = fullHP;
        fpSlider.maxValue = fullFP;
        stamSlider.maxValue = fullStamina;
    }

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
                Dead();
                break;
        }
    }

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

        if (CheckAndSetState("WalkSprintTree", PlayerState.move) || 
            CheckAndSetState("OneHand_Up_Attack_1", PlayerState.attack) || 
            CheckAndSetState("Hit_F_1_InPlace", PlayerState.damaged) || 
            CheckAndSetState("Hit_F_2_InPlace", PlayerState.damaged) || 
            CheckAndSetState("OneHand_Up_Shield_Block_Hit_1", PlayerState.defense))
        {
            return;
        }
    }

    private void Move()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            stam -= 19;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            mystate = PlayerState.dash;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WalkSprintTree") == true)
        {
            // WalkSprintTree가 재생이 끝났다면 idle상태가 됨
            float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S) ||
                !Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.E))
            {
                if (animTime >= 1.0f) //애니메이션 플레이 끝
                {
                    mystate = PlayerState.idle;
                }
            }

        }
        else if (CheckAndSetState("OneHand_Up_Attack_1", PlayerState.attack) ||
                CheckAndSetState("Hit_F_1_InPlace", PlayerState.damaged) ||
                CheckAndSetState("Hit_F_2_InPlace", PlayerState.damaged) ||
                CheckAndSetState("OneHand_Up_Shield_Block_Hit_1", PlayerState.defense))
        {
            return;
        }

        Recovery(10);
    }

    private void Dash()
    {
        Consumption(10);
        if (!Input.GetKey(KeyCode.Space))
        {
            mystate = PlayerState.idle;
        }
    }

    private void Attack()
    {
        StartCoroutine(BeingIdle(playTime));
    }

    private void Defense()
    {
        if (!Input.GetMouseButton(1))
        {
            mystate = PlayerState.idle;
        }
        Recovery(5);
    }

    private void Damaged()
    {
        if (check.isDamaged && HP > 0)
        {
            HP -= 90;
            StartCoroutine(BeingIdle(playTime));
        }
        else if (HP <= 0)
        {
            mystate = PlayerState.dead;
        }
        else
        {
            StartCoroutine(BeingIdle(playTime));
        }
    }

    private void Dead()
    {
        deadScene.SetActive(true);
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
        if (stam == 0)
        {
            mystate = PlayerState.idle;
        }
    }

    // 마우스 왼쪽 버튼을 누르면 스태미너가 감소하고 몇 초 뒤 idle상태가 된다.
    private IEnumerator BeingIdle(float sec)
    {
        isBeingIdle = true;

        if (Input.GetMouseButtonDown(0))
        {
            stam -= 19;
        }
        yield return new WaitForSeconds(sec);

        mystate = PlayerState.idle;
        isBeingIdle = false;
    }
}
