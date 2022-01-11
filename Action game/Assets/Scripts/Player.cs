using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool wDown;

    Vector3 moveVec;

    Animator anim;

    //초기화는 Awake에서
    void Awake()
    {
        //animator를 자식 컴포넌트에 넣었기때문에 GetComponent하면 안 된다.
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //방향
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        //shift는 누를 때만 작동되도록 GetButton() 함수 사용, walk는 project setting에서 한거.
        wDown = Input.GetButton("Walk");

        moveVec = new Vector3(hAxis, 0 , vAxis).normalized;


        //transform.position += moveVec * speed * (wDown ? 0.3f : 1f) * Time.deltaTime;과 같다.
        if(wDown)
            transform.position += moveVec * speed * 0.3f * Time.deltaTime;
        else
            transform.position += moveVec * speed * Time.deltaTime;

        //Run은 vector3가 0,0,0만 아니면 된다.
        anim.SetBool("isRun" , moveVec != Vector3.zero);
        anim.SetBool("isWalk",wDown);

        transform.LookAt(transform.position + moveVec);
    }
}