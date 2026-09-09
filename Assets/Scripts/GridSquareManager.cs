// - - - - - - - - - - - - - - 
// Title: GridSquareManager
// Description: Determines what's displayed in each individual grid swuare
// - - - - - - - - - - - - - - 

using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public enum SquareState {empty, x, o};

public class GridSquareManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI xText;
    [SerializeField] private TextMeshProUGUI oText;

    //Set default square state to empty
    private SquareState currentState = SquareState.empty;

    private int squareID;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.ArrowClicked(squareID);
    }

    // Get new state and update square accordingly
    public void SetSquare (SquareState newState)
    {
        if (newState == SquareState.empty)
        {
            oText.enabled = false;
            xText.enabled = false;
        }
        else if (newState == SquareState.x)
        {
            xText.enabled = true;
            oText.enabled = false;
        }
        else
        {
            oText.enabled = true;
            xText.enabled = false;
        }
        currentState = newState;
    }

    public void SetSquareID(int id)
    {
        squareID = id;
    }

     public SquareState GetSquareState()
    {
        return currentState;
    }
}

