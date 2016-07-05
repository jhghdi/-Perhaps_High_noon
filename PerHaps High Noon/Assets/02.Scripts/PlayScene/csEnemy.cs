using UnityEngine;

public class csEnemy : MonoBehaviour {

    public GameObject stadardNote;
    public GameObject lifeNote;
    public GameObject feverNote;
    public GameObject highnoonNote;
	GameObject sNote;
	GameObject hNote;

    public float itemType;

    csValueManager valueManager;

    // Use this for initialization
    void Start () {
        valueManager = new csValueManager();

        Vector3 v = transform.position + Vector3.up *2;

		sNote = Instantiate(stadardNote, v, Quaternion.identity) as GameObject;
		sNote.transform.parent = transform;
		sNote.SetActive (true);

		hNote = Instantiate(highnoonNote, v, Quaternion.identity) as GameObject;
		hNote.transform.parent = transform;
		hNote.SetActive (false);
    }
	
	// Update is called once per frame
	void Update () {

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


    void createNpte()
    {
        Vector3 v = transform.position + Vector3.up * 2;

        Common.ITEM_TYPE item = (Common.ITEM_TYPE)itemType;

        if (Common.ITEM_TYPE.NONE.Equals(itemType))
            sNote = Instantiate(stadardNote, v, Quaternion.identity) as GameObject;
        else if (Common.ITEM_TYPE.LIFE.Equals(itemType))
            sNote = Instantiate(lifeNote, v, Quaternion.identity) as GameObject;
        else if (Common.ITEM_TYPE.FEVER.Equals(itemType))
            sNote = Instantiate(feverNote, v, Quaternion.identity) as GameObject;

        sNote.transform.parent = transform;
        sNote.SetActive(true);

        hNote = Instantiate(highnoonNote, v, Quaternion.identity) as GameObject;
        hNote.transform.parent = transform;
        hNote.SetActive(false);
    }

    public void ActiveItem(float amount)
    {
        // amount == 0 -> miss! (life 깎는다).
        if (amount == 0)
        {
            valueManager.ReduceLife();
            return;
        }

        Common.ITEM_TYPE item = (Common.ITEM_TYPE)itemType;

        switch (item)
        {
            case Common.ITEM_TYPE.NONE:
                if(!hNote.activeSelf)
                    valueManager.SetRevengeGuage(amount);
                break;
            case Common.ITEM_TYPE.LIFE:
                valueManager.GainLife();
                break;
            case Common.ITEM_TYPE.FEVER:
                valueManager.SetFeverMode(true);
                break;
        }
    }

}
