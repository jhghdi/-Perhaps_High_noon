using UnityEngine;
using System.Collections;

public class csEnemy : csEnemyObject
{


    IEnumerator OnMoveEnd()
    {
        yield return new WaitForSeconds(aimCoolTime);
        csSoundManager.Instance().PlaySfx(aimSound);
        standard.SetActive(true);
        value.SetActive(true);
        GetComponent<Collider>().enabled = true;
        standard.GetComponent<Renderer>().sharedMaterial.color = Color.white;
        OnChangeNote(false);

        // aim을 player 기준으로 보이도록
        standard.transform.LookAt(GameObject.Find("pPlayer").transform);
       // standard.transform.LookAt(GameObject.Find("pPlayer").transform.position + Vector3.up * 1.25f);

        if (itemType != Common.ITEM_TYPE.NONE)
            transform.parent.GetComponent<csEnemyManager>().InitItem(gameObject);
        animator.SetInteger("state", 0);
    }

}
