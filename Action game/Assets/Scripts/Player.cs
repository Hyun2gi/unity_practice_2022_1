using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;
    bool jDown;

    //지금 점프하고 있는지
    bool isJump;
    bool isDodge;

    Vector3 moveVec;
    Vector3 dodgeVec;


    Rigidbody rigid;
    Animator anim;

    //초기화는 Awake에서
    void Awake()
    {
        //제일위에 있기 때문에 자식x
        rigid = GetComponent<Rigidbody>();
        //animator를 자식 컴포넌트에 넣었기때문에 GetComponent하면 안 된다.
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
    }

    void GetInput() 
    {
        //방향
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        //shift는 누를 때만 작동되도록 GetButton() 함수 사용, walk는 project setting에서 한거.
        wDown = Input.GetButton("Walk");
        //스페이스바 누르는 즉시 받아야하니까
        jDown = Input.GetButtonDown("Jump");
    }

    void Move() 
    {
        moveVec = new Vector3(hAxis, 0 , vAxis).normalized;
        //회피중에는 움직임벡터 -> 회피방향 벡터로 바뀌도록 구현
        if(isDodge)
            moveVec = dodgeVec;
        //transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;과 같다.
        if(wDown)
            transform.position += moveVec * speed * 0.3f * Time.deltaTime;
        else
            transform.position += moveVec * speed * Time.deltaTime;

        //Run은 vector3가 0,0,0만 아니면 된다.
        anim.SetBool("isRun" , moveVec != Vector3.zero);
        anim.SetBool("isWalk",wDown);
    }

    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        //점프를 할때 isJump가 true가 된다.
        //isJump가 false여야만 이걸 시행한다.
        //점프는 움직이지 않을때
        if(jDown && moveVec == Vector3.zero && !isJump && !isDodge) {
            //15는 힘의 정도
            //ForceMode.Impulse는 즉발적인 힘
            rigid.AddForce(Vector3.up * 15, ForceMode.Impulse);
            anim.SetBool("isJump" , true);
            anim.SetTrigger("doJump");
            isJump = true;
        }
    }

    void Dodge()
    {
        //회피는 움직이고 있을때
        if(jDown && moveVec != Vector3.zero && !isJump && !isDodge) {
            //회피할때 vec 움직이지 않도록
            dodgeVec= moveVec;
            //회피는 이동속도만 2배 상승
            speed *= 2;
            anim.SetTrigger("doDodge");
            isDodge = true;

            //0.4초의 시간차로
            Invoke("DodgeOut", 0.4f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    //착지
    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            //점프뛰고 바닥에 닿으면 false가 된다.
            isJump = false;
        }
    }
}