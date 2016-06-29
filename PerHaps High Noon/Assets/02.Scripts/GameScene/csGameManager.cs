using UnityEngine;

public class csGameManager : MonoBehaviour {

    public GameObject Enemy;
    public GameObject obj1;
    public GameObject obj2;

    private float repwanTime;

    private float time;
    // Use this for initialization
    void Start () {
        repwanTime = 3.0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (repwanTime <= time)
        {
            int rand = Random.Range(1, 2);
            Vector3 position = rand == 1 ? obj1.transform.position : obj2.transform.position;
            Instantiate(Enemy, position, Quaternion.identity);

            time = 0;
        }
        else
           time += Time.deltaTime;
    }
}
