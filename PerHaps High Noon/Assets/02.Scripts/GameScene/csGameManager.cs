using UnityEngine;

public class csGameManager : MonoBehaviour {

    public GameObject Enemy;
    public GameObject obj1;
    public GameObject obj2;

    private float repwanTime;

    private float time;
    // Use this for initialization
    void Start () {
        repwanTime = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (repwanTime <= time)
        {
            int rand = Random.Range(1, 2);
            Vector3 position = rand == 1 ? obj1.transform.position : obj2.transform.position;

			Vector3 v = new Vector3 (Random.Range(-5,5),Random.Range(0.5f,5),Random.Range(-5,5));
			GameObject obj =  Instantiate(Enemy, v, Quaternion.identity) as GameObject;

			//obj.SendMessage ("coolTime",Random.Range(1,2));
            time = 0;
        }
        else
           time += Time.deltaTime;
    }
}
