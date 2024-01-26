using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Logger;

public class TestHand : MonoBehaviour, IDragHandler, IEndDragHandler, ILogger
{
    #region Properties
    public string Prefix => @"<TestHand>";
    #endregion

    #region Variables
    [SerializeField] private LayerMask _handDropAreaLayerMask;

    private RectTransform _rectTransform;
    private Vector3 _initialPosition;
    #endregion

    #region Mono
    public void Start()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        _initialPosition = _rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // reset to original position
        _rectTransform.position = _initialPosition;

        var overlapped = Physics2D.OverlapPoint(Input.mousePosition, _handDropAreaLayerMask);
        if (overlapped != null)
        {
            HandController.Instance.IncreaseHitCount();
        }
    }
    #endregion
}
