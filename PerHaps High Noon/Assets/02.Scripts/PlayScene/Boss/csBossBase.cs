using UnityEngine;
using System.Collections;

public class csBossBase : MonoBehaviour {

    protected GameObject player;

    /// <summary>
    /// 해당 보스의 패턴 단계
    /// </summary>
    protected int patternStep;

    /// <summary>
    /// boss의 행동상태
    /// </summary>
    protected Common.BOSS_ACTION_TYPE actionStatus;

    protected Vector3 destination;

	// Use this for initialization
	void Start () {

        player = GameObject.Find("Player");

        patternStep = 0;

        // 보스 등장 에니메이션 

    }
	
	// Update is called once per frame
	void Update () {
	    // 행동 체크
        switch(actionStatus)
        {
            case Common.BOSS_ACTION_TYPE.MOVE:
                Move();
                break;
            case Common.BOSS_ACTION_TYPE.ATTACK:
                Attack();
                break;
            case Common.BOSS_ACTION_TYPE.DEAD:
                RunDeadAnimation();
                break;
        }
	}

    void Move()
    {

    }
    
    void Attack()
    {
      
    }

    void RunDeadAnimation()
    {

    }
}
