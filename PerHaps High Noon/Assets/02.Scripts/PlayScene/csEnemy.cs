using UnityEngine;

public class csEnemy : MonoBehaviour {

    public GameObject stadardNote;
    public GameObject highnoonNote;
    GameObject sNote;
	GameObject hNote;

    public Common.ITEM_TYPE itemType;

    csValueManager valueMethod;

    // Use this for initialization
    void Start () {
        valueMethod = GameObject.Find("ValueManager").GetComponent<csValueManager>();
    }
	
    public void OnChangeNote(bool isHighNoon)
	{
		if (isHighNoon) {
			sNote.SetActive (false);
			hNote.SetActive (true);
		} else {
			sNote.SetActive (true);
			hNote.SetActive (false);
		}
	}

	void OnHide(){
		GameObject.Find ("EnemyManager").SendMessage ("OnEnemyDead");
		gameObject.SetActive (false);
	}

    public void CreateNote()
    {
        // Aim 생성
        Vector3 v = transform.position + Vector3.up * 2;
        sNote = Instantiate(stadardNote, v, Quaternion.identity) as GameObject;
        hNote = Instantiate(highnoonNote, v, Quaternion.identity) as GameObject;

        // itemType 지정
        switch (itemType)
        {
            case Common.ITEM_TYPE.NONE:
                sNote.GetComponent<csNote>().type = Common.ITEM_TYPE.NONE;
                break;
            case Common.ITEM_TYPE.LIFE:
                sNote.GetComponent<csNote>().type = Common.ITEM_TYPE.LIFE;
                break;
            case Common.ITEM_TYPE.FEVER:
                sNote.GetComponent<csNote>().type = Common.ITEM_TYPE.FEVER;
                break;
        }

        sNote.transform.parent = transform;
        sNote.SetActive(true);

        hNote.transform.parent = transform;
        hNote.SetActive(false);
    }

    public void ActiveItem(float amount)
    {
        // amount == 0 -> miss! (life 깎는다).
        if (amount == 0)
        {
            valueMethod.ReduceLife();
            return;
        }
        
        // ItemType에 따른 사용
        switch (itemType)
        {
            case Common.ITEM_TYPE.NONE:
                if(!hNote.activeSelf)
                    valueMethod.SetRevengeGuage(amount);
                break;
            case Common.ITEM_TYPE.LIFE:
                valueMethod.GainLife();
                break;
            case Common.ITEM_TYPE.FEVER:
                valueMethod.SetFeverMode(true);
                break;
        }

    }

}
