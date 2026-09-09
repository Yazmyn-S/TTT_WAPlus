using UnityEngine;

public class GridManager : MonoBehaviour
{
    //Get grid sqaures
    [SerializeField] private GridSquareManager[] grid;

    //On scene load...
    private void Awake()
    {
       //Reset the grid to empty 
        ResetGrid();

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
    public void SetSquare(SquareState squareState, int square)
    {
       grid[square].SetSquare(squareState);
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



/* Note
0 | 1 | 2
3 | 4 | 5
6 | 7 | 8
*/