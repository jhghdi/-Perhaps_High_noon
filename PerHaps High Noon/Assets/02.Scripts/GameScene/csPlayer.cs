using UnityEngine;

public class csPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag.Equals("Note"))
                {
                    csNote note = hit.transform.GetComponentInParent<csNote>();
                    if (note != null)
                        note.OnTrigger();
                }
            }
        }
    }
}
