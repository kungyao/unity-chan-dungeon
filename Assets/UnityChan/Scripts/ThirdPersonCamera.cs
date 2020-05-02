//
// Unityちゃん用の三人称カメラ
// 
// 2013/06/07 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

public enum CameraType
{
    First = 1,
    Back = 2,
    Top = 3
}
public class ThirdPersonCamera : MonoBehaviour
{
    public float smooth = 3f;       // カメラモーションのスムーズ化用変数

    //public Transform jumpPos;          // Jump Camera locater
    public Transform backPos;
    public Transform firstPos;
    public Transform topPos;
    public GameObject a;
    public GameObject b;
    // スムーズに繋がない時（クイック切り替え）用のブーリアンフラグ
    public bool bQuickSwitch = false;	//Change Camera Position Quickly
    private CameraType ct;

    void Start()
    {
        // 各参照の初期化
        firstPos = GameObject.FindWithTag("FirstPos").transform;
        backPos = GameObject.FindWithTag("BackPos").transform;
        topPos = GameObject.FindWithTag("TopPos").transform;
        //jumpPos = GameObject.FindWithTag("JumpPos").transform;
        ct = CameraType.First;
        a.SetActive(false);
        b.SetActive(false);

    }

    void FixedUpdate()  // このカメラ切り替えはFixedUpdate()内でないと正常に動かない
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))//Set first camera
        {
            a.SetActive(false);
            b.SetActive(false);
            ct = CameraType.First;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))//Set top camera
        {
            a.SetActive(true);
            b.SetActive(true);
            ct = CameraType.Top;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))//Set third camera
        {
            a.SetActive(true);
            b.SetActive(true);
            ct = CameraType.Back;
        }

        if (ct == CameraType.First)
        {
            // bQuickSwitch = true;
            transform.position = firstPos.position;
            transform.forward = firstPos.forward;
        }
        else if (ct == CameraType.Top)
        {
            transform.position = topPos.position;
            transform.rotation = topPos.rotation;
        }
        else if (ct == CameraType.Back)
        {
            transform.position = backPos.position;
            transform.forward = backPos.forward;
        }
    }

    /*void setCameraPositionNormalView()
	{
		if(bQuickSwitch == false){
		// the camera to standard position and direction
						transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);	
						transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
		}
		else{
			// the camera to standard position and direction / Quick Change
			transform.position = standardPos.position;	
			transform.forward = standardPos.forward;
			bQuickSwitch = false;
		}
	}

	
	void setCameraPositionFrontView()
	{
		// Change Front Camera
		bQuickSwitch = true;
		transform.position = frontPos.position;	
		transform.forward = frontPos.forward;
	}*/

    /*void setCameraPositionJumpView()
    {
        // Change Jump Camera
        transform.position = Vector3.Lerp(transform.position, jumpPos.position, Time.fixedDeltaTime * smooth);
        transform.forward = Vector3.Lerp(transform.forward, jumpPos.forward, Time.fixedDeltaTime * smooth);
    }*/
    public CameraType getCT()
    {
        return ct;
    }
}
