using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MenuScript : MonoBehaviour {

    public Text length;
    public Text width;
    public Text warning;
    private bool easy = false;
    private bool normal = false;
    private bool hard = false;

    public void OnStartGame(string SceneName)
    {
        if(CheckInputValid(length , width))
        {
            if (easy){
                Managevalue.enemy_count = 2;
                Managevalue.trap_count = 10;
                Managevalue.aid_count = 6;
            }
            else if (normal){
                Managevalue.enemy_count = 4;
                Managevalue.trap_count = 15;
                Managevalue.aid_count = 4;
            }
            else if (hard){
                Managevalue.enemy_count = 6;
                Managevalue.trap_count = 20;
                Managevalue.aid_count = 2;
            }
            Managevalue.maze_row = Convert.ToInt32(length.text);
            Managevalue.maze_column = Convert.ToInt32(width.text);
            Application.LoadLevel(SceneName);
        }
        else
        {
            warning.text = "Invalid Input";
        }
    }

    public void SetEasy()
    {
        easy = true;
        normal = false;
        hard = false;
    }

    public void SetNormal()
    {
        easy = false;
        normal = true;
        hard = false;
    }

    public void SetHard()
    {
        easy = false;
        normal = false;
        hard = true;
    }
    private bool CheckInputValid(Text length , Text width)
    {
        int lengthi = Convert.ToInt32(length.text);
        int widthi = Convert.ToInt32(width.text);
        if (lengthi >= 10 && lengthi <= 20 && widthi >= 10 && widthi <= 20)
        {
            if (!easy && !normal && !hard)
            {
                return false;
            }
            return true;
        }
        else
            return false;
    }
}
