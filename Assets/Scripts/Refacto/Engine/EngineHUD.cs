using UnityEngine;
using UnityEngine.UI;

public class EngineHUD : MonoBehaviour
{
    private Rigidbody _rb;
    private GameObject _speedMeter;
    private Image _speedBar;
    private Text _speedText;

    private void Awake()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }

    private void Start()
    {
        _speedMeter = GameObject.Find(Constants.GameObjectName.Speedmeter);
        _speedBar = _speedMeter.GetComponentInChildren<Image>();
        _speedText = _speedMeter.GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (_speedMeter)
        {
            UpdateSpeedmeter(_rb.velocity);
        }
    }

    public void UpdateSpeedmeter(Vector3 kartVelocity)
    {
        var speed = Mathf.Round(kartVelocity.magnitude);
        _speedBar.fillAmount = speed / 70;
        _speedText.text = "" + speed * 2;
    }

    private void Show()
    {
        if (_speedMeter)
        {
            _speedMeter.SetActive(true);
        }
    }

    private void Hide()
    {
        if (_speedMeter)
        {
            _speedMeter.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Show();
    }

    private void OnDisable()
    {
        Hide();
    }
}
