using UnityEngine;
using System.Collections;

public class csTurret : csEnemyObject
{
    void Awake()
    {
        hp = 1;
        scaleSpeed = scaleSpeed = new Vector3(200f, 200f, 200f);
        enemyManager = GameObject.Find("EnemyManager").GetComponent<csEnemyManager>();
        valueMethod = GameObject.Find("ValueManager").GetComponent<csValueManager>();
        animator = GetComponent<Animator>();
        judgementStandard = standard.transform.localScale.x;
        GetComponent<Collider>().enabled = false;
    }

    public void SetMove(bool isMove)
    {
        standard.SetActive(isMove);
        value.SetActive(isMove);
        GetComponent<Collider>().enabled = isMove;
    }

    new void Update()
    {
        //일시정지 시 리턴
        if (!Common.isRunning)
            return;

        //아직 이동중이여서 에임 활성 X
        if (!value.activeSelf)
            return;

        //조준중
        if (value.transform.localScale.x > 100)
            value.transform.localScale -= scaleSpeed * Time.deltaTime;
        else
        {
            //공격단계 수정요망
            OnHide();
            ActiveItem(0);

            // 다시 활성화시 처음 크기로 보이도록
            value.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
    }

}
