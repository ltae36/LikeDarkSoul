using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    int secondHitAnimation;

    public float moveSpeed;

    public float runSpeed = 6f;
    public float turnSpeed = 8f;
    public float sprintSpeed = 8f;
    public float walkSpeed = 4f;

    Vector3 dir;

    public bool isSprint = false;
    public bool isWalking = false;
    public bool isrun;
    public bool fallDeath = false;

    float onSpace;

    public GameObject blood;
    public Vector3 camDir;
    public Camera cam;
    public StatManager stat;

    GameObject playerModel;
    Animator animator;
    CharacterController cc;
    PlayerAttack attack;
    HitCheck hit;

    void Start()
    {
        attack = GetComponent<PlayerAttack>();
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        hit = GetComponent<HitCheck>();
        moveSpeed = runSpeed;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        // 카메라의 Transform을 가져온다.
        Transform cameraTransform = cam.transform;

        // 카메라의 로컬 로테이션y값을 추출한다.
        Quaternion cameraRotationY = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);

        // 액션이 작동 중일 때는 WASD이동이 작동하지 않는다.
        if (!stat.inAction)
        {
            dir = new Vector3(h, 0, v);
        }
        else
        {
            dir = Vector3.zero;
        }
        //dir = new Vector3(h, 0, v);

        // 지면에서 떨어져 y값이 줄어들면 추락한 것으로 판단하고 사망한다.
        if(transform.position.y < -8) 
        {
            fallDeath = true;
        }


        camDir = cameraRotationY * dir; // 카메라의 로컬 로테이션y 값을 dir에 적용한다.

        // CharacterController를 이용한 이동
        cc.Move(camDir * moveSpeed * Time.deltaTime);

        // 이동 상태가 아닐 경우(키 WASD 키입력이 없을 경우)
        if (dir != Vector3.zero)
        {
            // 방향전환
            Quaternion rot = Quaternion.LookRotation(camDir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);

            // dir이 zero가 아닌 상태(이동 상태)에서 LeftAlt를 누르면 걷고, SpaceBar를 누르면 달린다.        
            // WASD를 누른 채로 LeftAlt를 누르면 속도가 느려지고, walk애니메이션이 재생된다.
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                Walk(10f);
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                onSpace += Time.deltaTime;
                if (onSpace > 1.5f)
                {
                    Dash(10f);
                }
            }
            else if (Input.GetKeyUp(KeyCode.Space) && stat.mystate == StatManager.PlayerState.move) 
            {
                animator.SetTrigger("Roll 1");
            }
            else 
            {
                Run(3.0f);
                Run(3.0f);
            }

        }
        else 
        {
            // 제자리라면 스페이스바를 눌렀을 때 뒤로 물러난다.
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                stat.stam -= stat.useStam;
                animator.SetTrigger("Jump_Back");
            }
        }

        // WASD를 누르면 해당 방향으로 달리는 애니메이션이 재생된다.
        animator.SetBool("Run", dir != Vector3.zero);

        #region 애니메이션 재생



        #region 구르기 애니메이션
        //if (stat.mystate == StatManager.PlayerState.move)
        //{

        //    if (Input.GetKey(KeyCode.W))
        //    {
        //        animator.SetFloat("Roll", 0);
        //    }
        //    else if (Input.GetKey(KeyCode.A))
        //    {
        //        animator.SetFloat("Roll", 1);
        //    }
        //    else if (Input.GetKey(KeyCode.S))
        //    {
        //        animator.SetFloat("Roll", 3);
        //    }
        //    else if (Input.GetKey(KeyCode.D))
        //    {
        //        animator.SetFloat("Roll", 4);
        //    }

        //    if (Input.GetKeyUp(KeyCode.Space))
        //    {
        //        animator.SetTrigger("Roll 1");
        //    }
        //}


        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    // 움직이지 않는 상태에서 스페이스바를 눌렀다 떼면 뒤로 점프한다.
        //    if (dir == Vector3.zero)
        //    {
        //        animator.SetTrigger("Jump_Back");
        //    }

        //    // 움직이는 상태에서 스페이스바를 누르면 해당 방향으로 구른다.
        //    else if (stat.mystate == StatManager.PlayerState.move)
        //    {
        //        animator.SetFloat("Roll", 3);
        //        animator.SetFloat("Roll", 0);
        //        animator.SetFloat("Roll", 1);
        //        animator.SetFloat("Roll", 4);

        //    }
        //}
        #endregion

        #endregion

    }

    void Walk(float sec) 
    {
        isWalking = true;
        moveSpeed -= sec * Time.deltaTime;
        moveSpeed = Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed);
        animator.SetFloat("MoveSpeed", moveSpeed);
    }

    void Dash(float sec) 
    {
        isSprint = true;
        moveSpeed += sec * Time.deltaTime;
        moveSpeed = Mathf.Clamp(moveSpeed, walkSpeed, sprintSpeed);
        animator.SetFloat("MoveSpeed", moveSpeed);
    }

    void Run(float sec) 
    {
        isWalking = false;
        isSprint = false;

        //if (isWalking) 
        //{
        //    moveSpeed += runSpeed * Time.deltaTime;
        //    moveSpeed = Mathf.Clamp(moveSpeed, 0, runSpeed);
        //}
        //else if (isSprint) 
        //{
        //    moveSpeed -= runSpeed * Time.deltaTime;
        //    moveSpeed = Mathf.Clamp(moveSpeed, runSpeed, 20);
        //}
        moveSpeed = Mathf.Lerp(moveSpeed, runSpeed, sec);
        animator.SetFloat("MoveSpeed", moveSpeed);
    }

    public void PlayerHit(float damage) 
    {
        // 데미지가 들어오면 피격 애니메이션 재생
        animator.SetTrigger("Hit");
        stat.HP -= damage;
        Instantiate(blood); // 피 이펙트 생성
        blood.transform.position = transform.position;
        StartCoroutine(waitFrame()); // 이펙트 제거
        return;
    }

    IEnumerator waitFrame() 
    {
        yield return null;
        Destroy(blood);
        print("공격!!");
        yield return null;
    }

    //void HandleMovementInput() 
    //{
    //    // 기본적으로 Roll 값을 -1로 설정하여 입력이 없을 때의 상태를 정의합니다.
    //    int rollDirection = -1;

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        rollDirection = 0; // 앞방향
    //    }
    //    else if (Input.GetKey(KeyCode.A))
    //    {
    //        rollDirection = 1; // 왼쪽방향
    //    }
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //        rollDirection = 3; // 뒤쪽방향
    //    }
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        rollDirection = 4; // 오른쪽방향
    //    }

    //    // Roll 애니메이션 값 설정
    //    animator.SetInteger("Roll", rollDirection);

    //    // 스페이스바를 눌렀다 떼는 경우 롤 애니메이션을 트리거
    //    if (rollDirection >= 0 && Input.GetKeyUp(KeyCode.Space))
    //    {
    //        animator.SetTrigger("Roll 1");
    //    }
    //}

}
