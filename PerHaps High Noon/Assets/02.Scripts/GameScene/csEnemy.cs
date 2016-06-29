using UnityEngine;

public class csEnemy : MonoBehaviour {

    public GameObject note;

    public bool isLeft = true;

    float moveSpeed = 5.0f;

    // test code
    int rand;
    float time = 0;

    // Use this for initialization
    void Start () {
        rand = 30;
    }
	
	// Update is called once per frame
	void Update () {

        if (isLeft)
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        else
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);

        if (time > rand)
        { 
            moveSpeed = 0;
          //Instantiate(note, transform.position, Quaternion.identity);
            rand = 100000;
           GameObject obj = Instantiate(note, transform.position, Quaternion.identity) as GameObject;
            obj.transform.parent = transform;
        }
        else
         ++time;
    }

    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Stop")
    //    { 
            
         
    //    }
    //}

}
