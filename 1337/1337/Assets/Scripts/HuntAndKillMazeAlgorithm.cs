using UnityEngine;
using System.Collections;

public class HuntAndKillMazeAlgorithm : MazeGen {

    //Global Varible
private int currentRow = 0;
	private int currentCol = 0;

	private bool mazeDone = false;

    //Constructor
	public HuntAndKillMazeAlgorithm(MazeCell[,] mazeCells) : base(mazeCells) {
	}

    //Create Maze
	public override void BuildMaze () {
		HuntAndKill ();
	}

    //Init
	private void HuntAndKill() {
		mazeCells [currentRow, currentCol].visited = true;

		while (! mazeDone) {
			Kill(); 
			Hunt(); 
		}
	}

  
    //Kill Part of our Algorithim
	private void Kill() {
		while (AvailbleRoute(currentRow, currentCol)) {
			
			int direction = NumGen.GetNextNumber ();
            Debug.Log("Direction for KillMethod " + direction);

			if (direction == 1 && CellIsAvailable (currentRow - 1, currentCol)) {
				// North Wall
				DestroyWallIfItExists (mazeCells [currentRow, currentCol].nWall);
				DestroyWallIfItExists (mazeCells [currentRow - 1, currentCol].sWall);
				currentRow--;
			} else if (direction == 2 && CellIsAvailable (currentRow + 1, currentCol)) {
				// South Wall
				DestroyWallIfItExists (mazeCells [currentRow, currentCol].sWall);
				DestroyWallIfItExists (mazeCells [currentRow + 1, currentCol].nWall);
				currentRow++;
			} else if (direction == 3 && CellIsAvailable (currentRow, currentCol + 1)) {
				// east wall
				DestroyWallIfItExists (mazeCells [currentRow, currentCol].eWall);
				DestroyWallIfItExists (mazeCells [currentRow, currentCol + 1].wWall);
                currentCol++;
			} else if (direction == 4 && CellIsAvailable (currentRow, currentCol - 1)) {
				// west wall
				DestroyWallIfItExists (mazeCells [currentRow, currentCol].wWall);
				DestroyWallIfItExists (mazeCells [currentRow, currentCol - 1].eWall);
                currentCol--;
			}

			mazeCells [currentRow, currentCol].visited = true;
		}
	}

	private void Hunt() {
		mazeDone = true;  // We will assume the maze is complete if we fine a cell that is not, we revert the Bool back to false

		for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
				if (!mazeCells [r, c].visited && CellNextVisited(r,c)) {
					mazeDone = false; 
					currentRow = r;
                    currentCol = c;
					DestroyWall (currentRow, currentCol);
					mazeCells [currentRow, currentCol].visited = true;
					return; //break

                
				}
			}
		}
	}

//Searching for Routes
	private bool AvailbleRoute(int row, int column) {
		int availableRoutes = 0;

		if (row > 0 && !mazeCells[row-1,column].visited) {
			availableRoutes++;
		}

		if (row < mazeRows - 1 && !mazeCells [row + 1, column].visited) {
			availableRoutes++;
		}

		if (column > 0 && !mazeCells[row,column-1].visited) {
			availableRoutes++;
		}

		if (column < mazeColumns-1 && !mazeCells[row,column+1].visited) {
			availableRoutes++;
		}

		return availableRoutes > 0;
	}

	private bool CellIsAvailable(int row, int column) {
		if (row >= 0 && row < mazeRows && column >= 0 && column < mazeColumns && !mazeCells [row, column].visited) {
			return true;
		} else {
			return false;
		}
	}

    //Function to Destory GameObject Wall
	private void DestroyWallIfItExists(GameObject wall) {
		if (wall != null) {
			GameObject.Destroy (wall);
		}
	}

	private bool CellNextVisited(int row, int column) {
		int visitedCells = 0;

        //Check if we are first row look up
		if (row > 0 && mazeCells [row - 1, column].visited) {
			visitedCells++;
		}

        //Check if we are next to last row
        if (row < (mazeRows-2) && mazeCells [row + 1, column].visited) {
			visitedCells++;
		}

        //Check if we are first coll look up look left
        if (column > 0 && mazeCells [row, column - 1].visited) {
			visitedCells++;
		}

		//Check if we are next to last coll
		if (column < (mazeColumns-2) && mazeCells [row, column + 1].visited) {
			visitedCells++;
		}

		// return true if there are any adjacent visited cells to this one
		return visitedCells > 0;
	}

    //Randomly destroy a wall to make a path
	private void DestroyWall(int row, int column) {
		bool wallDestroyed = false;

		while (!wallDestroyed) {
			int direction = NumGen.GetNextNumber ();
            Debug.Log("Direction" + direction);
			if (direction == 1 && row > 0 && mazeCells [row - 1, column].visited) {
				DestroyWallIfItExists (mazeCells [row, column].nWall);
				DestroyWallIfItExists (mazeCells [row - 1, column].sWall);
				wallDestroyed = true;
			} else if (direction == 2 && row < (mazeRows-2) && mazeCells [row + 1, column].visited) {
				DestroyWallIfItExists (mazeCells [row, column].sWall);
				DestroyWallIfItExists (mazeCells [row + 1, column].nWall);
				wallDestroyed = true;
			} else if (direction == 3 && column > 0 && mazeCells [row, column-1].visited) {
				DestroyWallIfItExists (mazeCells [row, column].wWall);
				DestroyWallIfItExists (mazeCells [row, column-1].eWall);
				wallDestroyed = true;
			} else if (direction == 4 && column < (mazeColumns-2) && mazeCells [row, column+1].visited) {
				DestroyWallIfItExists (mazeCells [row, column].eWall);
                DestroyWallIfItExists(mazeCells[row, column + 1].wWall);
			}
		}

	}

}
