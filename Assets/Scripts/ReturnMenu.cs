using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMenu : MonoBehaviour {

	public void Returnmenu(string SceneName)
    {
        Application.LoadLevel(SceneName);
    }
}
