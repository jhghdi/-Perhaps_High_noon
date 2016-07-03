using UnityEngine;

public class csEnemyManager : MonoBehaviour {
	public GameObject stageM;
	//배열로 미리 만들어 놓을 예정
	public GameObject Enemy;
	int enemyCount=-11;


	private float repwanTime;

	private float time;
	// Use this for initialization
	void Start () {
		
		repwanTime = 0.2f;
	}

	// Update is called once per frame
	void Update () {
		
		if (enemyCount == 0) {
			stageM.SendMessage ("OnStepEnded");
			enemyCount = -11;
			return;
		}
		else if ( enemyCount>0 && repwanTime <= time)
		{
			int rand = Random.Range(1, 2);


			Vector3 v = new Vector3 (Random.Range(-5,5),Random.Range(0.5f,5),Random.Range(-5,5));
			GameObject obj =  Instantiate(Enemy, v, Quaternion.identity) as GameObject;

			//obj.SendMessage ("coolTime",Random.Range(1,2));
			time = 0;

			enemyCount--;
		}
		else
			time += Time.deltaTime;
	}

	void setEnemyCount(int n){
		Debug.Log(n);
		enemyCount = n;
	}
}
