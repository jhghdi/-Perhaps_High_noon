using UnityEngine;

public class csNote : MonoBehaviour {

    public GameObject value;
    public GameObject standard;
    

    public Vector3 scaleSpeed;

	// Use this for initialization
	void Start () {
        scaleSpeed = new Vector3(1f, 1f, 1f);
    }

    public void SetScaleSpeed(float speed)
    {
        scaleSpeed.x = speed;
        scaleSpeed.y = speed;
        scaleSpeed.z = speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (value.transform.localScale.x > 0)
			value.transform.localScale -= scaleSpeed*Time.deltaTime;
        else
        {
			transform.parent.SendMessage ("OnHide");
        }
	}

    public void OnTrigger()
    {
        float stadardScale = standard.transform.localScale.x;

        float judgement = Mathf.Abs( (value.transform.localScale.x - stadardScale) / stadardScale);

        // fever guage 증가량 및 miss 판별
        float amount=0;

        if (judgement < 0.1f)
            amount = 4.0f;
        else if (judgement < 0.4f)
            amount = 3.0f;
        else if (judgement < 0.8f)
            amount = 2.0f;
        else if (judgement < 0.99f)
            amount = 1.0f;
        else
            amount = 0;

        transform.parent.SendMessage("ActiveItem", amount);
        transform.parent.SendMessage ("OnHide");      
    }
}
