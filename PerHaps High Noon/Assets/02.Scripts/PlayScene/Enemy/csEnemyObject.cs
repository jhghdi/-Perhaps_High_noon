using UnityEngine;
using System.Collections;

public class csEnemyObject : MonoBehaviour {

    // value Aim
    public GameObject value;

    // judgements Aim
    public GameObject standard;

    /// value Aim의 줄어드는 속도
    public Vector3 scaleSpeed;

    public Common.ITEM_TYPE itemType;

    // 판정의 판단기준
    protected float judgementStandard;

    // targeting전 대기시간
    protected float aimCoolTime;

    protected Animator animator;

    protected csValueManager valueMethod;

    protected csEnemyManager enemyManager;

    /// <summary>
    /// aim 등장시 나오는 sound
    /// </summary>
    public AudioClip aimSound;

    /// <summary>
    /// aim 사망시 나오는 sound
    /// </summary>
    public AudioClip destroySound;

    /// <summary>
    /// player에게 공격시 나오는 sound
    /// </summary>
    public AudioClip shotSound;

    // hp
    protected int hp ;

    // Use this for initialization
    void Awake()
    {
        hp = 1;
        scaleSpeed = scaleSpeed = new Vector3(1f, 1f, 1f);
        enemyManager = GameObject.Find("EnemyManager").GetComponent<csEnemyManager>();
        valueMethod = GameObject.Find("ValueManager").GetComponent<csValueManager>();
        animator = GetComponent<Animator>();
        judgementStandard = standard.transform.localScale.x;
        GetComponent<Collider>().enabled = false;
    }

    public void OnChangeNote(bool isRevenge)
    {
        if (!standard.activeSelf)
            return;

        value.SetActive(!isRevenge);

        if (isRevenge)
            standard.GetComponent<Renderer>().sharedMaterial.color = Color.yellow;
        else 
            standard.GetComponent<Renderer>().sharedMaterial.color = Color.white;
    }

    protected void Update()
    {
        //일시정지 시 리턴
        if (!Common.isRunning)
            return;

        //아직 이동중이여서 에임 활성 X
        if (!value.activeSelf)
            return;

        //조준중
        if (value.transform.localScale.x > 0.4)
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

    protected void OnHide()
    {
//        if(gameObject.tag != "BOSS")
        //transform.parent.SendMessage("OnEnemyDead");
        enemyManager.SendMessage("OnEnemyDead");

        animator.enabled = true;

        value.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);

        SetAimVisible(false);

        gameObject.SetActive(false);

        if (itemType != Common.ITEM_TYPE.NONE)
            //transform.parent.GetComponent<csEnemyManager>().RemoveItem();
            enemyManager.RemoveItem();
    }

    //카메라페스 경로 관리자가 호출
    void OnMoveStart()
    {
        animator.SetInteger("state", 1);
    }

    public void SetAimCoolTime(float coolTime)
    {
        aimCoolTime = coolTime;
    }

    public void OnTrigger()
    {
        float judgementResult = Mathf.Abs((value.transform.localScale.x - judgementStandard) / judgementStandard);

        // fever guage 증가량 및 miss 판별
        float amount = 1.0f;

        if (judgementResult < 0.1f)
            amount = 4.0f;
        else if (judgementResult < 0.4f)
            amount = 3.0f;
        else if (judgementResult < 0.8f)
            amount = 2.0f;
        else if (judgementResult < 0.99f)
            amount = 1.0f;

        // 피격판정시 체력감소
        if (amount > 0)
            --hp;

        // 체력이 다달면
        if (hp <= 0)
        {
            ActiveItem(amount);

            // sound 출력
            csSoundManager.Instance().PlaySfx(destroySound);

            SetAimVisible(false);
            //레그돌 애니메이션
            animator.enabled = false;
            //1초동안 굴러다님
            Invoke("OnHide", 1);
        }
    }

    public void ActiveItem(float amount)
    {
        if (amount == 0)
        {
            csSoundManager.Instance().PlaySfx(shotSound);
            valueMethod.ReduceLife();
            return;
        }

        // ItemType에 따른 사용
        switch (itemType)
        {
            case Common.ITEM_TYPE.LIFE:
                valueMethod.GainLife();
                break;
            case Common.ITEM_TYPE.FEVER:
                valueMethod.SetFeverMode(true);
                break;
        }
    }

    protected void SetAimVisible(bool v)
    {
        GetComponent<Collider>().enabled = v;
        //조준선 가림
        standard.SetActive(v);
        value.SetActive(v);
    }
}
