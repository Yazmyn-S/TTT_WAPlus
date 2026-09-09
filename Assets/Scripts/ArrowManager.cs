using UnityEngine;
using UnityEngine.EventSystems;

public class ArrowManager : MonoBehaviour, IPointerClickHandler
{
    private int arrowID;

    public void SetArrowID(int id)
    {
        arrowID = id;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.ArrowClicked(arrowID);
    }
}