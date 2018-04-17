using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MazeLoader : MonoBehaviour {

    //Global Vars
	private int mazeRows, mazeColumns;
	public GameObject wall;
    public GameObject eSpawner;
	public float size = 2f;

    private int MaxRow, MaxCol;

	private MazeCell[,] mazeCells;


    // Use this for initialization
    void Start () {
        //BenchMarking
        Stopwatch sw = Stopwatch.StartNew();
       
            GenerateMaze();
            MazeGen mg = new HuntAndKillMazeAlgorithm(mazeCells);
            mg.BuildMaze();
        
        sw.Stop();
        UnityEngine.Debug.Log("Debug BenchMark :" + sw.ElapsedMilliseconds+ "ms");
       

    }
	
	// Update is called once per frame
	void Update () {
	}

    private void GenerateMaze()
    {
        //Random Generation of Maze Size
        //Between 5 and 64
        int mazeRows = Random.Range(5, 64);
        MaxRow = mazeRows;
        int mazeColumns = Random.Range(5, 64);
        MaxCol = mazeColumns;
        UnityEngine.Debug.Log("Debug Rows: " + mazeRows + " Columns: " + mazeColumns);

        //Maze Cells is a new 2D Array Filled will the Random Number of Rows and collumns
        mazeCells = new MazeCell[mazeRows, mazeColumns];

        for (int r = 0; r < mazeRows; r++)
        {
            for (int c = 0; c < mazeColumns; c++)
            {
                mazeCells[r, c] = new MazeCell();

                mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].floor.name = "f " + r + "," + c;
                mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);

                if (c == 0)
                {
                    mazeCells[r, c].wWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity) as GameObject;
                    mazeCells[r, c].wWall.name = "wWall " + r + "," + c;
                }

                mazeCells[r, c].eWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity) as GameObject;
                mazeCells[r, c].eWall.name = "eWall " + r + "," + c;

                if (r == 0)
                {
                    mazeCells[r, c].nWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                    mazeCells[r, c].nWall.name = "eWall " + r + "," + c;
                    mazeCells[r, c].nWall.transform.Rotate(Vector3.up * 90f);
                }

                mazeCells[r, c].sWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity) as GameObject;
                mazeCells[r, c].sWall.name = "sWall " + r + "," + c;
                mazeCells[r, c].sWall.transform.Rotate(Vector3.up * 90f);
            }
        }

        //Place the enemy spawner on a random Tile in the maze 
        eSpawner.transform.position = mazeCells[Random.Range(1, MaxRow), Random.Range(1, MaxCol)].floor.transform.position;
    }
}
