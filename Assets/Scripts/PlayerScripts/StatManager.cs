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
    }

    // ���� ��ġ ����, �׼� ���� ����
    private void InitializeStats()
    {
        // �⺻ ���� ��ġ�� �ִ�ġ�� �����Ѵ�.
        HP = fullHP;
        FP = fullFP;
        stam = fullStamina;

        // �׼��� �����ʴ� ���·� üũ�Ѵ�.
        inAction = false;
    }

    // UI�� ���� ��ġ ����
    private void InitializeUI()
    {
        // UI������ �ִ�ġ�� �����Ѵ�.
        hpSlider.maxValue = fullHP;
        fpSlider.maxValue = fullFP;
        stamSlider.maxValue = fullStamina;
    }

    // state���� �Լ�
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

    // ���� ��ġ�� �ǽð����� UI�� �ݿ��Ѵ�.
    private void UpdateUI()
    {
        hpSlider.value = HP;
        fpSlider.value = FP;
        stamSlider.value = stam;
    }

    private void Idle()
    {
        // ���¹̳ʰ� Ǯ����°� �ƴ϶�� ȸ���Ѵ�.
        if (stam < fullStamina) Recovery(10);

        // �ִϸ��̼��� ������̶�� �ش� state���°� �ȴ�.
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

        #region �����̽��� �Է¿� ���� ���¹̳� ����

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
        //    // WalkSprintTree�� ����� �����ٸ� idle���°� ��
        //    float animTime = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //    if (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.S) ||
        //        !Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.E))
        //    {
        //        if (animTime >= 1.0f) //�ִϸ��̼� �÷��� ��
        //        {
        //            mystate = PlayerState.idle;
        //        }
        //    }

        //}
        #endregion
        // ���� moveSpeed�� sprintSpeed��ŭ �����ٸ� dash���°� ��
        if (playerMove.moveSpeed > playerMove.runSpeed) 
        {
            StartCoroutine(BeingIdle(PlayerState.dash, 1.0f));
        }
        else if (CheckAndSetState("OneHand_Up_Idle", PlayerState.idle) ||
                CheckAndSetState("OneHand_Up_Attack_1", PlayerState.attack) ||
                CheckAndSetState("Hit_F_1", PlayerState.damaged) ||
                CheckAndSetState("OneHand_Up_Shield_Block_Hit_1", PlayerState.defense))
        {
            return;
        }

        // ���¹̳ʰ� Ǯ����°� �ƴ϶�� ���������� ȸ��
        if (HP < fullHP)
        {
            Recovery(10);
        }
    }

    private void Dash()
    {
        // ���������� ���¹̳� �Ҹ�
        Consumption(10);
        // ���� moveSpeed�� sprintSpeed���� �����ٸ� move���°� ��
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
        // attack ���°� �Ǹ� ���� �ִϸ��̼� �ð� ��ŭ ��ٸ� �� idle ���°� �ȴ�.
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
        Recovery(5);
    }

    private void Damaged()
    {
        if (check.isDamaged && HP > 0)
        {
            HP -= 90;
            StartCoroutine(BeingIdle(PlayerState.idle, playTime));
        }
        else if (HP <= 0)
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
        deadScene.SetActive(true);
    }

    // ������ �ִϸ��̼� ������Ʈ�� Ȯ��
    private bool IsPlayingAnimation(string animationName)
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(animationName);
    }

    // �ش� �ִϸ��̼��� ��� ���ΰ��� Ȯ��
    private bool CheckAndSetState(string animationName, PlayerState newState)
    {
        if (IsPlayingAnimation(animationName))
        {
            playTime = anim.GetCurrentAnimatorStateInfo(0).length; // �ִϸ��̼��� ��� �ð��� playTime�� ����ȴ�.
            mystate = newState;
            return true;
        }
        return false;
    }

    // ���¹̳ʰ� ȸ���ȴ�.
    private void Recovery(float amount)
    {
        stam = Mathf.Clamp(stam + Time.deltaTime * amount, 0, fullStamina);
    }

    // ���¹̳ʰ� ���������� �Ҹ�ȴ�.
    private void Consumption(float amount)
    {
        stam = Mathf.Clamp(stam - Time.deltaTime * amount, 0, fullStamina);
        //if (stam == 0)
        //{
        //    mystate = PlayerState.idle;
        //}
    }

    private IEnumerator BeingIdle(PlayerState state, float sec)
    {
        isBeingIdle = true;
        yield return new WaitForSeconds(sec);
        mystate = state;
        isBeingIdle = false;
    }    
}
