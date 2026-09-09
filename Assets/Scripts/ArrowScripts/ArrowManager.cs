using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private Arrow[] arrows;
    [SerializeField] private GridManager gridManager;

    // Each path starts beside the arrow and travels
    // toward the opposite side of the grid.
    private readonly int[,] arrowPaths =
    {
        // Top arrows: downward
        { 0, 3, 6 },
        { 1, 4, 7 },
        { 2, 5, 8 },

        // Bottom arrows: upward
        { 6, 3, 0 },
        { 7, 4, 1 },
        { 8, 5, 2 },

        // Left arrows: rightward
        { 0, 1, 2 },
        { 3, 4, 5 },
        { 6, 7, 8 },

        // Right arrows: leftward
        { 2, 1, 0 },
        { 5, 4, 3 },
        { 8, 7, 6 }
    };

    private void Awake()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetArrowID(i);
        }
    }

    public bool PushPiece(SquareState newState, int clickedArrow)
    {
        int firstSquare = arrowPaths[clickedArrow, 0];
        int middleSquare = arrowPaths[clickedArrow, 1];
        int lastSquare = arrowPaths[clickedArrow, 2];

        SquareState firstState =
            gridManager.GetSquareState(firstSquare);

        SquareState middleState =
            gridManager.GetSquareState(middleSquare);

        SquareState lastState =
            gridManager.GetSquareState(lastSquare);

        // The row or column is full.
        if (firstState != SquareState.empty &&
            middleState != SquareState.empty &&
            lastState != SquareState.empty)
        {
            return false;
        }

        // The entire line is empty.
        // Send the new piece all the way across.
        if (firstState == SquareState.empty &&
            middleState == SquareState.empty &&
            lastState == SquareState.empty)
        {
            gridManager.SetSquare(newState, lastSquare);
            return true;
        }

        // The arrow entrance is occupied.
        // Push existing pieces one square forward.
        if (firstState != SquareState.empty)
        {
            if (middleState == SquareState.empty)
            {
                gridManager.SetSquare(firstState, middleSquare);
            }
            else
            {
                gridManager.SetSquare(middleState, lastSquare);
                gridManager.SetSquare(firstState, middleSquare);
            }

            gridManager.SetSquare(newState, firstSquare);
            return true;
        }

        // The middle square is occupied.
        // Stop immediately before it.
        if (middleState != SquareState.empty)
        {
            gridManager.SetSquare(newState, firstSquare);
            return true;
        }

        // Only the opposite square is occupied.
        // Stop in the middle.
        gridManager.SetSquare(newState, middleSquare);
        return true;
    }
}