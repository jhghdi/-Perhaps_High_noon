using UnityEngine;
using System.Collections;

public class csEnemy : MonoBehaviour {

    public GameObject stadardNote;
    public GameObject highnoonNote;
    GameObject sNote;
	GameObject hNote;

<<<<<<< HEAD
    private float aimCoolTime;
=======
    Animator animator;
>>>>>>> Kim-Da-Hun

    public Common.ITEM_TYPE itemType;

    csValueManager valueMethod;

    // Use this for initialization
<<<<<<< HEAD
    void Start ()
    {
        valueMethod = GameObject.Find("ValueManager").GetComponent<csValueManager>();     
=======
    void Awake () {
        valueMethod = GameObject.Find("ValueManager").GetComponent<csValueManager>();
        animator = GetComponent<Animator>();
>>>>>>> Kim-Da-Hun
    }
	
    public void OnChangeNote(bool isRevenge)
	{
		if (isRevenge) {
			sNote.SetActive (false);
			hNote.SetActive (true);
		} else {
			sNote.SetActive (true);
			hNote.SetActive (false);
		}
	}

	void OnHide(){
        //기존 노트 초기화
        sNote.SetActive(false);
        hNote.SetActive(false);
        GameObject.Find ("EnemyManager").SendMessage ("OnEnemyDead");
       
        gameObject.SetActive (false);

        if (itemType != Common.ITEM_TYPE.NONE)
            transform.parent.GetComponent<csEnemyManager>().RemoveItem();
    }

    public void CreateNote()
    {
        

        if (sNote == null)
        { 
            sNote = Instantiate(stadardNote, Common.GetAimPosition(transform.position), Quaternion.identity) as GameObject;
            sNote.transform.parent = transform;
            sNote.SetActive(false);
        }
        if (hNote == null)
        { 
            hNote = Instantiate(highnoonNote, Common.GetAimPosition(transform.position), Quaternion.identity) as GameObject;
            hNote.transform.parent = transform;
            hNote.SetActive(false);
        }      
    }

    public void ActiveItem(float amount)
    {
        // amount == 0 -> miss! (life 깎는다).
        if (amount == 0)
        {
            valueMethod.ReduceLife();
            return;
        }
        if (!hNote.activeSelf)
            valueMethod.SetRevengeGuage(amount);

        // ItemType에 따른 사용
        switch (itemType)
        {
<<<<<<< HEAD
=======
            case Common.ITEM_TYPE.NONE:
                if(!hNote.activeSelf)
                    valueMethod.AddRevengeGuage(amount);
                break;
>>>>>>> Kim-Da-Hun
            case Common.ITEM_TYPE.LIFE:
                valueMethod.GainLife();
                break;
            case Common.ITEM_TYPE.FEVER:
                valueMethod.SetFeverMode(true);
                break;
        }

        

    }

    public void SetAimCoolTime(float coolTime)
    {
        this.aimCoolTime = coolTime;
    }
    void OnMoveStart()
    {
        animator.SetInteger("state", 1);
    }

<<<<<<< HEAD
    IEnumerator OnMoveEnded()
    {
        yield return new WaitForSeconds(aimCoolTime);
        OnChangeNote(false);

        if (itemType != Common.ITEM_TYPE.NONE)
            transform.parent.GetComponent<csEnemyManager>().InitItem(gameObject);
=======
    void OnMoveEnd()
    {
        animator.SetInteger("state", 0);
>>>>>>> Kim-Da-Hun
    }
}
