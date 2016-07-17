using UnityEngine;
using System.Collections;

public class csBoss : MonoBehaviour {

    public GameObject missile;

    private GameObject[] missiles;

    private GameObject player;

    /// <summary>
    /// 해당 보스의 패턴 단계
    /// </summary>
    int patternStep;

    /// <summary>
    /// boss의 움직임 상태 유무 확인
    /// </summary>
    bool isMoving;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");

        patternStep = 0;

        missiles = new GameObject[12];

        for(int i=0; i<missiles.Length; ++i)
        {
            missiles[i] = Instantiate(missile, Vector3.zero, Quaternion.identity) as GameObject;
            missiles[i].SetActive(false);
        }

        // 보스 등장 에니메이션 

    }
	
	// Update is called once per frame
	void Update () {
	    // 행동 체크
        // 이동 
        // 공격
        // 맞아준다
	}

    void Move()
    {

    }
    
    void Attack()
    {
        switch (patternStep)
        {
            case 1:
                for(int i=0; i<4; ++i)
                {
                    missiles[i].GetComponent<csMissile>().player = player.transform;
                    missiles[i].GetComponent<csMissile>().isLeft = i < 2 ? true : false;
                    missiles[i].GetComponent<csMissile>().downSpeed = i % 2 > 0 ? 0.1f : 0.2f;
                    missiles[i].SetActive(true);
                }
                break;

            case 2:
                for (int i = 0; i < 6; ++i)
                {
                    missiles[i].GetComponent<csMissile>().player = player.transform;
                    missiles[i].SetActive(true);
                }
                break;

            case 3:
                for (int i = 0; i < 12; ++i)
                {
                    missiles[i].GetComponent<csMissile>().player = player.transform;
                    missiles[i].SetActive(true);
                }
                break;

            default:
                break;
        }

    }
}
