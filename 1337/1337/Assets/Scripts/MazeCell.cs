using UnityEngine;


//Class Interface Object for Each cell, each cell has a boolean to see if it was checked, and has GameObjects Connected to it, that represent 4 walls and a floor
public class MazeCell {
	public bool visited = false;
    public GameObject nWall, sWall, eWall, wWall, floor;
}
