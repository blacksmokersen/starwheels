using System;
using UnityEngine;
using UnityEngine.UI;

public class StringInput : MonoBehaviour
{
    [SerializeField] private GameObject shadow;
    [SerializeField] private InputField input;
    [SerializeField] private Text windowTitle;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button okButton;

    private Action<string> _action;

    // CORE

    private void Awake()
    {
        cancelButton.onClick.AddListener(() => shadow.SetActive(false));
        okButton.onClick.AddListener(ValidateInput);
    }

    // PUBLIC

    public void GetStringInput(string title, Action<string> action)
    {
        shadow.SetActive(true);
        windowTitle.text = title;
        input.text = "";

        _action = action;
    }

    public void ValidateInput()
    {
        shadow.SetActive(false);
        _action.Invoke(input.text);
    }

    // PRIVATE
}
