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

        if (judgement < 0.1f) 
            Debug.Log("Perfect");
        else if(judgement < 0.2f)
            Debug.Log("Great");
        else if (judgement < 0.4f)
            Debug.Log("Good");
        else if (judgement < 0.5f)
            Debug.Log("Bad");
        else
            Debug.Log("Miss");


		transform.parent.SendMessage ("OnHide");

        
    }


}
