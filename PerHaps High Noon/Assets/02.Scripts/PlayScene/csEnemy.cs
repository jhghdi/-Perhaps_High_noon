using UnityEngine;

public class csEnemy : MonoBehaviour {

    public GameObject stadardNote;
	public GameObject highnoonNote;
	GameObject sNote;
	GameObject hNote;

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

}
