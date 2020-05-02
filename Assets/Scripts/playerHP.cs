using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHP : MonoBehaviour {
    public float totalHP = 150.0f;
    public int changeHP;
    public Text ratioText;
    private float nowpos;
	// Use this for initialization
	void Start () {
        totalHP = 150.0f;
        changeHP = 0;
	}
	
	// Update is called once per frame
	void Update () {
        nowpos = -200 + 200 * ((totalHP + changeHP) / totalHP);
        transform.localPosition = new Vector3(nowpos, 0f, 0f);
        ratioText.text = (totalHP + changeHP).ToString();
	}

    public void lessHP()
    {
        if (nowpos > -200){
            changeHP -= 10;
        }
    }

    public void addHP()
    {
        if (nowpos < 0){
            changeHP += 30;
            if (changeHP > 0)
                changeHP = 0;
        }
    }

    public bool IsDeath()
    {
        return nowpos <= -200 ? true : false;
    }
}
