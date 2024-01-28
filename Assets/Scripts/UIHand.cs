using System;
using System.Collections;
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
    [SerializeField] private float _pitchFactor;
    [SerializeField] private RectTransform _targetReference;
    [SerializeField] private bool useV2;

    [Header("Read Only")]
    [SerializeField] private float _chargeTimer = 0;
    [SerializeField] private float _chargeTarget = 0;
    [SerializeField] private bool _isSlapping;
    [SerializeField] private bool _isEnableInput;

    public bool IsEnableInput => _isEnableInput;

    private Vector3 _originalPosition;
    private Vector2 _originalPositionV2;
    private Vector2 _chargeMoveDirectionV2 = new Vector3(1, 1, 0);
    private RectTransform _rectTransform;

    #endregion

    #region Mono
    public void Start()
    {
        _originalPosition = transform.position;
        _progressBar.SetValue(0);

        _image.sprite = _normalSprite;

        _rectTransform = GetComponent<RectTransform>();

        _originalPositionV2 = _rectTransform.anchoredPosition;
        _chargeMoveDirectionV2 = (_targetReference.anchoredPosition - _rectTransform.anchoredPosition).normalized;
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

    public void Slap(int playerIndex = -1)
    {
        if (_isSlapping)
        {
            this.Log("Ignore slapping since we are already slapping");
            return;
        }

        _isEnableInput = false;
        StartCoroutine(_slap(playerIndex));
    }

    public void Resolve(bool isInputDown)
    {
        if (isInputDown && !_isEnableInput)
        {
            this.Log("Detect isInputDown but UIHand is not enable for input");
        }

        if (_isSlapping)
        {
            // exaggerate appearnace
            _progressBar.Minus(0.01f);
            _progressBar.UpdateBar(1);

            return;
        }

        if (!_isEnableInput)
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
            AudioManager.Instance.PlaySE(SE.Spring, 1 + _chargeTimer * _pitchFactor);
        }
        else
        {
            if (_chargeTimer > 0)
            {
                _chargeTimer -= Time.deltaTime * _restoreSpeed;
                _chargeTimer = Mathf.Clamp(_chargeTimer, 0, _chargeTarget);

                _progressBar.SetValue(_chargeTimer / _slapChargeCount);
            }
            else
            {
                _chargeTarget = 0;
            }
        }

        if (_chargeTarget > 0)
        {
            if (useV2)
            {
                _rectTransform.anchoredPosition = Vector2.Lerp(_originalPositionV2, _originalPositionV2 + _chargeMoveDirectionV2 * _chargeMoveStrength * _chargeTarget, _chargeTimer / _chargeTarget);
            }
            else
            {
                transform.position = Vector3.Lerp(_originalPosition, _originalPosition + _chargeMoveDirection * _chargeMoveStrength * _chargeTarget, _chargeTimer / _chargeTarget);
            }
        }
    }
    #endregion

    #region Private Methods
    private IEnumerator _slap(int playerIndex)
    {
        // for direct call from mobile
        if (useV2)
        {
            _rectTransform.anchoredPosition = _originalPositionV2 + _chargeMoveDirectionV2 * _chargeMoveStrength * _slapChargeCount;
        }
        else
        {
            transform.position = _originalPosition + _chargeMoveDirection * _chargeMoveStrength * _slapChargeCount;
        }

        var player = playerIndex < 0
            ? GameManager.Instance.PlayerManager.GetSleptPlayer()
            : GameManager.Instance.PlayerManager.GetPlayer(playerIndex);

        _isSlapping = true;
        AudioManager.Instance.PlaySE(SE.Slap);
        yield return new WaitForSeconds(0.5f);

        this.Log("Slap!");
        _image.sprite = _SlapSprite;
        if (player != null)
        {
            player.Slap();
        }

        OnSlapEvent?.Invoke();
        yield return new WaitForSeconds(1);

        if (player != null)
        {
            player.Normal();
        }
        _resetAll();
    }

    private void _resetAll()
    {
        transform.position = _originalPosition;
        _chargeTarget = 0;
        _chargeTimer = 0;
        _progressBar.SetValue(0);
        _image.sprite = _normalSprite;
        _isSlapping = false;
    }
    #endregion

    #region Editor Methods
    #endregion
}
