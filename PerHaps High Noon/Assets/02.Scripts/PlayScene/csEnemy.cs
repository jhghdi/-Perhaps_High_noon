using UnityEngine;

public class csEnemy : MonoBehaviour {

    public GameObject stadardNote;
	public GameObject highnoonNote;
	GameObject sNote;
	GameObject hNote;

    public bool isLeft = true;

   // float moveSpeed = 5.0f;

    // Use this for initialization
    void Start () {
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
//        if (isLeft)
//            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
//        else
//            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
//
//        if (time > rand)
//        { 
//            moveSpeed = 0;
//          //Instantiate(note, transform.position, Quaternion.identity);
//            rand = 100000;
//           
//        }
//        else
//         ++time;
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
    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Stop")
    //    { 
            
         
    //    }
    //}
	void OnHide(){
		GameObject.Find ("EnemyManager").SendMessage ("OnEnemyDead");
		gameObject.SetActive (false);
	}
}
