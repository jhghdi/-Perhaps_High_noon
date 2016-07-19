using System.Collections;
using UnityEngine;

public class csDrone : csEnemyObject
{
    public GameObject drone;

    // Called by CameraPath Manager
    IEnumerator OnMoveEnd()
    {
        yield return new WaitForSeconds(aimCoolTime);

        drone.GetComponent<Animator>().SetInteger("state", 0);
        csSoundManager.Instance().PlaySfx(aimSound);
        standard.SetActive(true);

        value.SetActive(true);

        GetComponent<Collider>().enabled = true;
        standard.GetComponent<Renderer>().sharedMaterial.color = Color.white;
        OnChangeNote(false);

        // aim을 player 기준으로 보이도록
        standard.transform.LookAt(GameObject.Find("pPlayer").transform.position + Vector3.up * 1.25f);

        if (itemType != Common.ITEM_TYPE.NONE)
            transform.parent.GetComponent<csEnemyManager>().InitItem(gameObject);
        
    }

    /// <summary>
    /// 카메라페스 경로 관리자가 호출, 재정의
    /// </summary>
    void OnMoveStart()
    {
        drone.GetComponent<Animator>().SetInteger("state", 1);
    }

    private new void OnTrigger()
    {
        float judgementResult = Mathf.Abs((value.transform.localScale.x - judgementStandard) / judgementStandard);

        // fever guage 증가량 및 miss 판별
        float amount = 1.0f;

        if (judgementResult < 0.1f)
            amount = 4.0f;
        else if (judgementResult < 0.4f)
            amount = 3.0f;
        else if (judgementResult < 0.8f)
            amount = 2.0f;
        else if (judgementResult < 0.99f)
            amount = 1.0f;

        // 피격판정시 체력감소
        if (amount > 0)
            --hp;

        // 체력이 다달면
        if (hp <= 0)
        {
            ActiveItem(amount);

            SetAimVisible(false);

            StartCoroutine(OnDead());
        }
    }

    IEnumerator OnDead()
    {
        // Drone 사망 animation
        drone.GetComponent<Animator>().SetBool("isDie", true);

        csSoundManager.Instance().PlaySfx(shotSound);

        //3초동안 굴러다님
        yield return new WaitForSeconds(2.0f);
        enemyManager.SendMessage("OnEnemyDead");

        drone.GetComponent<Animator>().enabled = true;
        value.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        drone.SetActive(false);
        gameObject.SetActive(false);

        if (itemType != Common.ITEM_TYPE.NONE)
            enemyManager.RemoveItem();
    }
}
