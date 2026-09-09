using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Get grid sqaures
    [SerializeField] private GridSquareManager[] grid;
    [SerializeField] private ArrowManager[] arrow;

    //On scene load...
    private void Awake()
    {
       //Reset the grid to empty 
        ResetGrid();
        
        //Assign arrow a ID
        for (int i = 0; i < arrow.Length; i++)
        {
            arrow[i].SetArrowID(i);
        }

        // assign each square and ID
        for (int i = 0; i < grid.Length; i++)
        {
            grid[i].SetSquare(SquareState.empty);
            grid[i].SetSquareID(i);
        }
    }
    
    //Set all squares to empty
    public void ResetGrid()
    {
        foreach (GridSquareManager square in grid)
        {   
            square.SetSquare(SquareState.empty);       
        }
    }

    //set state of square (X, O, Empty)
   // Each row contains the grid squares checked by one arrow,
// ordered from the farthest square to the nearest square.
private readonly int[,] arrowPaths =
{
    // Top arrows
    { 6, 3, 0 }, // Arrow 0
    { 7, 4, 1 }, // Arrow 1
    { 8, 5, 2 }, // Arrow 2

    // Bottom arrows
    { 0, 3, 6 }, // Arrow 3
    { 1, 4, 7 }, // Arrow 4
    { 2, 5, 8 }, // Arrow 5

    // Left arrows
    { 2, 1, 0 }, // Arrow 6
    { 5, 4, 3 }, // Arrow 7
    { 8, 7, 6 }, // Arrow 8

    // Right arrows
    { 0, 1, 2 }, // Arrow 9
    { 3, 4, 5 }, // Arrow 10
    { 6, 7, 8 }  // Arrow 11
};

public bool SetSquare(SquareState squareState, int clickedArrow)
{
    // Check each square along the selected arrow's path.
    for (int i = 0; i < 3; i++)
    {
        int squareIndex = arrowPaths[clickedArrow, i];

        if (grid[squareIndex].GetSquareState() == SquareState.empty)
        {
            grid[squareIndex].SetSquare(squareState);
            return true;
        }
    }

    // All three squares in that direction are occupied.
    return false;
}

    //get state of square (X, O, Empty)
    public SquareState GetSquareState(int squareID)
    {
        return grid[squareID].GetSquareState();
    }

    public bool isFull()
    {
        // If an empty square is found, return false
        foreach (GridSquareManager square in grid)
        {
            if (square.GetSquareState() == SquareState.empty)
            {
                return false;
            }
        }
        //Otherwise, return true
        return true;
    }

    public SquareState WinCondition(int gSquare1, int gSquare2, int gSquare3)
    {
        // Get X, O, or empty state for each square
        SquareState state1 = grid[gSquare1].GetSquareState();
        SquareState state2 = grid[gSquare2].GetSquareState();
        SquareState state3 = grid[gSquare3].GetSquareState();
        
        //If all three are the same and not empty, return winner state (X or O)
        if (state1 != SquareState.empty)
        {
            if (state1 == state2 && state1 == state3) return state1;
            else    return SquareState.empty;
        
        }
        return SquareState.empty;
    }
}



/* Note Grid
0 | 1 | 2
3 | 4 | 5
6 | 7 | 8

Arrow 
0 1 2 (Top)
3 4 5 (Bottom)
6 7 8 (Left)
9 10 11 (Right)
*/