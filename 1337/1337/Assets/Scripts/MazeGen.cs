using UnityEngine;
using System.Collections;


//Abstract Class that Defines the Hunt and Kill Maze Algorthim
public abstract class MazeGen {
	protected MazeCell[,] mazeCells;
	protected int mazeRows, mazeColumns;

	protected MazeGen(MazeCell[,] mazeCells) : base() {
		this.mazeCells = mazeCells;
		mazeRows = mazeCells.GetLength(0);
		mazeColumns = mazeCells.GetLength(1);
	}

	public abstract void BuildMaze ();
}
