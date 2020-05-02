using UnityEngine;
using System.Collections;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
    public GameObject default_enemy;
    public GameObject default_trap;
    public GameObject default_aid;
    public GameObject default_finish;

    private GameObject[] enemy;
    private GameObject[] trap;
    private GameObject[] aid;
    private GameObject finish;
    public float size = 2f;

	private MazeCell[,] mazeCells;
    private int[,] recordItem;//0:empty 1:player and finsih 2:enemy and trap 3:addHP
	// Use this for initialization
	void Start () {
        mazeRows = Managevalue.maze_row;
        mazeColumns = Managevalue.maze_column;
        InitializeMaze ();
        InitializeEnemy();
        InitializeTrap();
        InitializeAid();
		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void InitializeMaze()
    {

        mazeCells = new MazeCell[mazeRows, mazeColumns];

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                mazeCells[r, c] = new MazeCell();

                // For now, use the same wall object for the floor!
                mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);

                if (c == 0)
                {
                    mazeCells[r, c].westWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                }

                mazeCells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;

                if (r == 0)
                {
                    mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                }

                mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);
            }
        }

        recordItem = new int[mazeRows, mazeColumns];
        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                recordItem[r, c] = new int();
            }
        }
        recordItem[0, 0] = 1;
        recordItem[mazeRows - 1, mazeColumns - 1] = 1;
        finish = Instantiate(default_finish, new Vector3((mazeRows - 1) * size, -1.5f, (mazeColumns - 1) * size), Quaternion.identity) as GameObject;
        finish.transform.Rotate(new Vector3(-90f, 0f, 0f));
    }
    private void InitializeEnemy()
    {
        enemy = new GameObject[Managevalue.enemy_count];
        for(int i = 0; i < Managevalue.enemy_count;)
        {
            int random_r = Random.Range(0, mazeRows - 1);
            int random_c = Random.Range(0, mazeColumns - 1);
            if (recordItem[random_r, random_c] == 0 && CheckAdjacentItem(random_r, random_c))
            {
                enemy[i] = Instantiate(default_enemy, new Vector3(random_r * size, -1f, random_c * size), Quaternion.identity) as GameObject;
                enemy[i].name = "Enemy" + (i + 1).ToString();
                recordItem[random_r, random_c] = 2;
                i++;
            }
            
        }
    }

    private void InitializeTrap()
    {
        trap = new GameObject[Managevalue.trap_count];
        for (int i = 0; i < Managevalue.trap_count;)
        {
            int random_r = Random.Range(0, mazeRows - 1);
            int random_c = Random.Range(0, mazeColumns - 1);
            if(recordItem[random_r, random_c] == 0 && CheckAdjacentItem(random_r , random_c))
            {
                trap[i] = Instantiate(default_trap, new Vector3(random_r * size, -1.5f, random_c * size), Quaternion.identity) as GameObject;
                trap[i].name = "Trap" + (i + 1).ToString();
                recordItem[random_r, random_c] = 2;
                i++;
            }
        }
    }

    private void InitializeAid()
    {
        aid = new GameObject[Managevalue.aid_count];
        for (int i = 0; i < Managevalue.aid_count;)
        {
            int random_r = Random.Range(0, mazeRows - 1);
            int random_c = Random.Range(0, mazeColumns - 1);
            if (recordItem[random_r, random_c] == 0)
            {
                aid[i] = Instantiate(default_aid, new Vector3(random_r * size, -1f, random_c * size), Quaternion.identity) as GameObject;
                aid[i].name = "Aid" + (i + 1).ToString();
                recordItem[random_r, random_c] = 3;
                i++;
            }
        }
    }

    private bool CheckAdjacentItem(int r , int c)
    {
        bool check = true;
        if (r > 0 && recordItem[r - 1, c] == 2)
            check = false;
        if (r < mazeRows && recordItem[r + 1, c] == 2)
            check = false;
        if (c > 0 && recordItem[r, c - 1] == 2)
            check = false;
        if (c < mazeColumns && recordItem[r, c + 1] == 2)
            check = false;
        return check;
    }
}
