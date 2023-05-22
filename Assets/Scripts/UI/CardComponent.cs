using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardComponent : MonoBehaviour
{
    [HideInInspector] public CardData cardData;

    [SerializeField] private Image cardFrontSide;
    [SerializeField] private Image cardBackSide;
    private Canvas canvas;
    private Transform previousParent;
    private CardsColumn previousColumn;
    private Vector3 positionBeforeDrag;
    private bool shownState;

    private void Start()
    {
        canvas = transform.root.GetComponent<Canvas>();
    }

    public void Set(CardData cardData)
    {
        this.cardData = cardData;
        SetGraphics(this.cardData.cardImage);
    }

    public void SetGraphics(Sprite frontSide)
    {
        cardFrontSide.sprite = frontSide;
    }

    public void SetCardActive(bool isActive)
    {
        cardFrontSide.gameObject.SetActive(isActive);
        cardBackSide.gameObject.SetActive(!isActive);
        shownState = isActive;
    }

    public void DragHandler(BaseEventData data)
    {
        if (!shownState)
        {
            return;
        }

        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, 
            pointerData.position, 
            canvas.worldCamera, 
            out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void DropHandler(BaseEventData data)
    {
        if (!shownState)
        {
            return;
        }

        PointerEventData pointerData = (PointerEventData)data;

        var eventSystem = EventSystem.current;
        List<RaycastResult> results = new List<RaycastResult>();
        eventSystem.RaycastAll(new PointerEventData(EventSystem.current) { position = pointerData.position }, results);

        CardsColumn targetColumn = null;

        foreach (var item in results)
        {
            if (item.gameObject.TryGetComponent<CardsColumn>(out targetColumn))
            {
                break;
            }
        }

        if (targetColumn == null)
        {
            ReturnCardToPreviousColumn();

            return;
        }

        PlaceCard(targetColumn);
    }

    public void BeginDragHandler(BaseEventData data)
    {
        if (!shownState)
        {
            return;
        }

        PointerEventData pointerData = (PointerEventData)data;

        previousColumn = transform.parent.GetComponent<CardsColumn>();    

        positionBeforeDrag = transform.position;
        previousParent = transform.parent;

        transform.SetParent(canvas.transform);
    }

    private void PlaceCard(CardsColumn targetColumn)
    {
        if (targetColumn.IsAbleToStack(this))
        {            
            previousColumn.RemoveCard();
            previousColumn.ShowLastCard();

            targetColumn.AddCard(this);
            transform.SetParent(targetColumn.transform); 
        }
        else
        {
            ReturnCardToPreviousColumn();
        }
    }

    private void ReturnCardToPreviousColumn()
    {
        transform.SetParent(previousParent);
        transform.position = positionBeforeDrag;
    }
}