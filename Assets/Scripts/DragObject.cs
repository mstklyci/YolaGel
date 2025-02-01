using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class DragObject : MonoBehaviour
{
    private bool moving;
    public bool gameStarted;
    private float startPosX;
    private float startPosY;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private Transform panelTransform;
    private GameObject lastDropZone;

    private Sprite initialSprite;
    private SpriteRenderer spriteRenderer;

    TrafficSign trafficSign;
    [SerializeField] private int boardNumber;
    AudioManager audioManager;

    private void Awake()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
        panelTransform = transform.parent;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            initialSprite = spriteRenderer.sprite;
        }
        gameStarted = false;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 0 || gameStarted) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        startPosX = mousePos.x - this.transform.position.x;
        startPosY = mousePos.y - this.transform.position.y;
        moving = true;

        GetComponent<Collider2D>().enabled = false;

        if (lastDropZone != null)
        {
            TrafficSign trafficSign = lastDropZone.GetComponent<TrafficSign>();
            if (trafficSign != null)
            {
                trafficSign.empty = true;
            }
            ToggleDropZoneVisuals(lastDropZone, true);
            lastDropZone = null;
        }
    }

    private void OnMouseUp()
    {
        if (Time.timeScale == 0 || gameStarted) return;

        moving = false;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("DropZone"))
            {
                if (hit.collider.transform.childCount == 0)
                {
                    transform.SetParent(hit.collider.transform);
                    transform.DOMove(hit.transform.position, 0.3f).SetEase(Ease.InOutCubic);
                    audioManager.PlaySFX(audioManager.trafficSign);

                    ToggleDropZoneVisuals(hit.collider.gameObject, false);
                    lastDropZone = hit.collider.gameObject;
                    trafficSign = hit.collider.GetComponent<TrafficSign>();
                    trafficSign.SelectSign(boardNumber);
                    trafficSign.empty = false;

                    UpdateSignDirection(trafficSign.rotation);
                }
                else
                {
                    ResetToPanelPosition();
                }
            }
            else
            {
                ResetToPanelPosition();
            }
        }
        else
        {
            ResetToPanelPosition();
        }

        GetComponent<Collider2D>().enabled = true;
    }

    private void Update()
    {
        if (Time.timeScale == 0 || !moving || gameStarted) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, transform.position.z);
    }

    private void ResetToPanelPosition()
    {
        transform.SetParent(panelTransform);
        transform.DOLocalMove(startPosition, 0.3f).SetEase(Ease.InOutQuad);
        transform.DOLocalRotateQuaternion(startRotation, 0.3f).SetEase(Ease.InOutQuad);

        if (spriteRenderer != null && initialSprite != null)
        {
            spriteRenderer.sprite = initialSprite;
        }

        if (lastDropZone != null)
        {
            ToggleDropZoneVisuals(lastDropZone, true);
            lastDropZone = null;
        }
    }

    public void UpdateStartPosition()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;
    }
        
    private void ToggleDropZoneVisuals(GameObject dropZone, bool isVisible)
    {
        SpriteRenderer dropZoneRenderer = dropZone.GetComponent<SpriteRenderer>();
        if (dropZoneRenderer != null)
        {
            dropZoneRenderer.enabled = isVisible;
        }

        Transform arrow = dropZone.transform.Find("Arrow");
        if (arrow != null)
        {
            arrow.gameObject.SetActive(isVisible);
        }
    }

    private void UpdateSignDirection(int rotation)
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}