using UnityEngine;
using System.Collections;

public class csEnemy : MonoBehaviour {

    // value Aim
    public GameObject value;
    // judgements Aim
    public GameObject standard;

    /// <summary>
    /// value Aim의 줄어드는 속도
    /// </summary>
    public Vector3 scaleSpeed;

    // 판정의 판단기준
    float judgementStandard;

    private float aimCoolTime;

    Animator animator;


    public Common.ITEM_TYPE itemType;

    csValueManager valueMethod;

    // Use this for initialization
    void Awake () {
        scaleSpeed = scaleSpeed = new Vector3(1f, 1f, 1f);
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

        if (isRevenge) {         
            standard.GetComponent<Renderer>().material.color = Color.yellow;   
        } else {
            standard.GetComponent<Renderer>().material.color = Color.white; 
        }
	}

    void Update()
    {
        //일시정지 시 리턴
        if (!Common.isRunning)
            return;
        //아직 이동중이여서 에임 활성 X
        if (!value.activeSelf)
            return;

        //조준중
        if (value.transform.localScale.x > 0)
            value.transform.localScale -= scaleSpeed * Time.deltaTime;
        else
        {
            //공격단계 수정요망
            //Attack();
            OnHide();
            ActiveItem(0);

            // 다시 활성화시 처음 크기로 보이도록
            value.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
    }

	void OnHide(){
        transform.parent.SendMessage ("OnEnemyDead");
        tag = "Enemy";
        GetComponent<Collider>().enabled = false;
        value.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);

        standard.SetActive(false);
        value.SetActive(false);
        gameObject.SetActive (false);

        if (itemType != Common.ITEM_TYPE.NONE)
            transform.parent.GetComponent<csEnemyManager>().RemoveItem();
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

    IEnumerator OnMoveEnd()
    {
        yield return new WaitForSeconds(aimCoolTime);
        standard.SetActive(true);
        value.SetActive(true);
        GetComponent<Collider>().enabled = true;
        standard.GetComponent<Renderer>().material.color = Color.white;
        OnChangeNote(false);

        // aim을 player 기준으로 보이도록
        standard.transform.LookAt(GameObject.Find("pPlayer").transform.position + Vector3.up * 1.25f);

        if (itemType != Common.ITEM_TYPE.NONE)
            transform.parent.GetComponent<csEnemyManager>().InitItem(gameObject);
        animator.SetInteger("state", 0);
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

        
        ActiveItem( amount);
        OnHide();
        
    }

    public void ActiveItem(float amount)
    {
        if (amount == 0)
        {
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
}
