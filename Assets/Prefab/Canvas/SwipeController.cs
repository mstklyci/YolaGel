using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] private Vector3 pageStep;
    [SerializeField] RectTransform allPagesRect;

    [SerializeField] private float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    private float dragThreshould;

    [SerializeField] Button previousPageButton, nextPageButton;
    AudioManager audioManager;

    private void Awake()
    {
        LeanTween.reset();
        currentPage = 1;
        targetPos = allPagesRect.localPosition;
        dragThreshould = Screen.width / 15;
        UpdateArrowButton();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void NextPage()
    {
        if (currentPage < maxPage)
        {
            audioManager.PlaySFX(audioManager.buttonSwoosh);
            currentPage++;
            targetPos += pageStep;
            MovePage();
            
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 1)
        {
            audioManager.PlaySFX(audioManager.buttonSwoosh);
            currentPage--;
            targetPos -= pageStep;
            MovePage();
            
        }
    }

    public void MovePage()
    {
        allPagesRect.LeanMoveLocal(targetPos, tweenTime)
            .setEase(tweenType)
            .setIgnoreTimeScale(true);
        UpdateArrowButton();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) PreviousPage();
            else NextPage();
        }
        else
        {
            MovePage();
        }
    }

    void UpdateArrowButton()
    {
        nextPageButton.interactable = true;
        previousPageButton.interactable = true;
        if (currentPage == 1)
        {
            previousPageButton.interactable = false;
        }
        else if (currentPage == maxPage)
        {
            nextPageButton.interactable = false;
        }
    }
}

