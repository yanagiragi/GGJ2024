using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UI.Component;
using UnityEngine;
using UnityEngine.UI;

public sealed class UIHand : MonoBehaviour, IHand, ILogger
{
    #region Action
    public event Action OnSlapEvent;
    #endregion

    #region Properties
    public string Prefix => "<UIHand>";

    public int ChargeCount => (int)_chargeTimer;
    #endregion

    #region Variables
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _SlapSprite;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Vector3 _chargeMoveDirection = new Vector3(1, 1, 0);
    [SerializeField] private float _chargeMoveStrength = 5;
    [SerializeField] private int _slapChargeCount = 10;
    [SerializeField] private float _restoreSpeed = 1;

    [Header("Read Only")]
    [SerializeField] private float _chargeTimer = 0;
    [SerializeField] private float _chargeTarget = 0;
    [SerializeField] private bool _isSlapping;
    [SerializeField] private bool _isEnableInput;
    private Vector3 _originalPosition;

    #endregion

    #region Mono
    public void Start()
    {
        _originalPosition = transform.position;
        _progressBar.SetValue(0);

        _image.sprite = _normalSprite;
    }

    public void Update()
    {
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + _chargeMoveDirection * _chargeMoveStrength);
    }
    #endregion

    #region Apis
    #endregion

    #region IHand
    public void EnableInput() => _isEnableInput = true;

    public void Slap()
    {
        if (_isSlapping)
        {
            this.Log("Ignore slapping since we are already slapping");
            return;
        }

        _isEnableInput = false;
        StartCoroutine(_slap());
    }

    public void Resolve(bool isInputDown)
    {
        if (isInputDown && !_isEnableInput)
        {
            this.Log("Detect isInputDown but UIHand is not enable for input");
        }

        if (_isSlapping || !_isEnableInput)
        {
            return;
        }

        if (_chargeTimer >= _slapChargeCount)
        {
            Slap();
            return;
        }

        if (isInputDown)
        {
            _chargeTarget += 1;
            _chargeTimer = _chargeTarget;
            _progressBar.Plus(1.0f / _slapChargeCount);
        }
        else
        {
            if (_chargeTimer > 0)
            {
                var originalChargeTimer = _chargeTimer;
                _chargeTimer -= Time.deltaTime * _restoreSpeed;
                _chargeTimer = Mathf.Clamp(_chargeTimer, 0, _chargeTarget);
                // this.LogError($"{(originalChargeTimer - _chargeTimer) / _slapChargeCount}");
                _progressBar.Minus((originalChargeTimer - _chargeTimer) / _slapChargeCount);
            }
            else
            {
                _chargeTarget = 0;
            }
        }

        if (_chargeTarget > 0)
        {
            transform.position = Vector3.Lerp(_originalPosition, _originalPosition + _chargeMoveDirection * _chargeMoveStrength * _chargeTarget, _chargeTimer / _chargeTarget);
        }
    }
    #endregion

    #region Private Methods
    private IEnumerator _slap()
    {
        _isSlapping = true;
        yield return new WaitForSeconds(1);

        this.LogError("Slap!");
        _image.sprite = _SlapSprite;
        OnSlapEvent?.Invoke();

        yield return new WaitForSeconds(1);

        _image.sprite = _normalSprite;
        _isSlapping = false;
        _resetAll();
    }

    private void _resetAll()
    {
        transform.position = _originalPosition;
        _chargeTarget = 0;
        _chargeTimer = 0;
        _progressBar.SetValue(0);
    }
    #endregion

    #region Editor Methods
    #endregion
}
