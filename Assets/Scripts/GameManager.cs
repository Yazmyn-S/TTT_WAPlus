using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEngine;


//Enums
public enum Turn {p1, p2};
public enum GameState {ongoing, draw, p1Win, p2Win};

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]private GridManager gridManager;
    
    [Header("Turn Objects")]
    [SerializeField] private TMPro.TextMeshProUGUI XturnText;
    [SerializeField] private TMPro.TextMeshProUGUI OturnText;
    [SerializeField] private TMPro.TextMeshProUGUI turnText;

    [Header("Result Objects")]
    [SerializeField] private string[] GameResults;
    [SerializeField] private TMPro.TextMeshProUGUI GameResult;

    //Private Variables
    private SquareState p1;
    private SquareState p2;

    private Turn curentTurn;
    private GameState currentGameState;
[SerializeField] private ArrowManager arrowManager;
      
  private void Awake()
    {
       //Set TurnText to disabled
       OturnText.enabled = false;
       XturnText.enabled = false;
       turnText.enabled = true;

       //Set GameResultText to disabled
       GameResult.enabled = false;
       
       currentGameState = GameState.ongoing;
       if (Instance == null) Instance = this;
       else Debug.LogError("Multiple GameManagers found!");
        // Start game on scene load
        StartGame();
    }
      
    private void StartGame()
    {
       //Reset grid to empty
       gridManager.ResetGrid();

        // X will always start first
        curentTurn = Turn.p1;
        
        //Assign players a character (X or O)
        p1 = SquareState.x;
        p2 = SquareState.o;

        //Display Turn
        DisplayTurn();
    }

//??? 30:00
    private void ProcessTurn(Turn turn, int clickedArrow)
{
    SquareState state;

    if (turn == Turn.p1)
    {
        state = p1;
    }
    else
    {
        state = p2;
    }

bool squareWasSet = arrowManager.PushPiece(state, clickedArrow);
    // Do not change turns if the selected path is full.
    if (!squareWasSet)
    {
        return;
    }

    bool gameEnded = isEnd();

    if (!gameEnded)
    {
        curentTurn = (curentTurn == Turn.p1) ? Turn.p2 : Turn.p1;
        DisplayTurn();
    }
    else
    {
        GameResult.enabled = true;
        XturnText.enabled = false;
        OturnText.enabled = false;
        turnText.enabled = false;
    }
}

    public void DisplayTurn()
    {
        if (curentTurn == Turn.p1)
        {
            XturnText.enabled = true;
            OturnText.enabled = false;
        }
        else
        {
            OturnText.enabled = true;
            XturnText.enabled = false;
        }
    }


    public void ArrowClicked(int clickedArrow)
{
    if (currentGameState != GameState.ongoing)
    {
        return;
    }

    ProcessTurn(curentTurn, clickedArrow);
}
    private bool isEnd()
    {
        bool gridFull = gridManager.isFull();
        //Get winner value (X, O, or empty)
        SquareState winner = checkWin();

        //Determine if you should end the game
        if (winner != SquareState.empty)
        {
            if (winner == p1)
            {
                currentGameState = GameState.p1Win;
                GameResult.text = GameResults[0];
                GameResult.color = Color.blue;
                
                
                return true;
            }
            else 
            {
                currentGameState = GameState.p2Win;
                GameResult.text = GameResults[1];
                GameResult.color = Color.red;
                return true;
            }
        }
        //No Winners
        else
        {
           if (gridFull)
            {
                currentGameState = GameState.draw;
                GameResult.text = GameResults[2];
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private SquareState checkWin()
    {
        SquareState winner = SquareState.empty;
        
        // Check horiziontal win conditions
        winner = gridManager.WinCondition(0, 1, 2);
       if (winner != SquareState.empty)
        {
            return winner;
        }
        winner = gridManager.WinCondition(3, 4, 5);
        if (winner != SquareState.empty)
        {
            return winner;
        }
        winner = gridManager.WinCondition(6, 7, 8);
        if (winner != SquareState.empty)
        {
            return winner;
        }

        // Check vertical win conditions
        winner = gridManager.WinCondition(0, 3, 6);
        if (winner != SquareState.empty)
        {
            return winner;
        }
        winner = gridManager.WinCondition(1, 4, 7);
        if (winner != SquareState.empty)
        {
            return winner;
        }
        winner = gridManager.WinCondition(2, 5, 8);
        if (winner != SquareState.empty)
        {
            return winner;
        }

        // Check diagonal win conditions
        winner = gridManager.WinCondition(0, 4, 8);
        if (winner != SquareState.empty)
        {
            return winner;
        }
        winner = gridManager.WinCondition(2, 4, 6);
        if (winner != SquareState.empty)
        {
            return winner;
        }
        return winner;
    }
}
