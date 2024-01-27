using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using UI_Component;

public sealed class UIHand : MonoBehaviour, IHand, ILogger
{
    #region Properties
    public string Prefix => "<UIHand>";

    public int ChargeCount => (int)_chargeTimer;
    #endregion

    #region Variables
    [SerializeField] private Image _image;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Vector3 _chargeMoveDirection = new Vector3(1, 1, 0);
    [SerializeField] private float _chargeMoveStrength = 5;
    [SerializeField] private int _slapChargeCount = 10;
    [SerializeField] private float _restoreSpeed = 1;

    [Header("Read Only")]
    [SerializeField] private float _chargeTimer = 0;
    [SerializeField] private float _chargeTarget = 0;
    [SerializeField] private bool isSlapping;
    private Vector3 _originalPosition;

    #endregion

    #region Mono
    public void Start()
    {
        _originalPosition = transform.position;
        _progressBar.SetValue(0);
    }

    public void Update()
    {
    }
    #endregion

    #region IHand

    public void Slap()
    {
        if (isSlapping)
        {
            this.Log("Ignore slapping since we are already slapping");
            return;
        }

        StartCoroutine(_slap());
    }

    public void Resolve(bool isInputDown)
    {
        if (isSlapping)
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
        _image.color = Color.blue;

        isSlapping = true;
        yield return new WaitForSeconds(3);

        this.LogError("Slap!");
        _image.color = Color.red;
        yield return new WaitForSeconds(1);

        _image.color = Color.white;
        isSlapping = false;
        _resetAll();
    }

    private void _resetAll()
    {
        transform.position = _originalPosition;
        _chargeTarget = 0;
        _chargeTimer = 0;
    }
    #endregion
}
