using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PasswordSreenHandler : MonoBehaviour
{
    [SerializeField] TMP_InputField passwordField;
    private string password = "123CAT";
    private string inputText;
    [SerializeField] TMP_Text instructions;
    [SerializeField] GameObject passwordScreen;
    [SerializeField] GameObject computerIcons;
    [SerializeField] GameObject changePasswordScreen;
    [SerializeField] TMP_InputField changePasswordField;
    [SerializeField] PatrolPoints devicePoint;

    private void OnEnable()
    {
        passwordField.Select();
        passwordField.ActivateInputField();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // Check if either Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (passwordScreen.gameObject.activeSelf)
            {
                inputText = passwordField.text;

                // Compare input text with the predefined password
                if (inputText == password)
                {
                    Debug.Log("Correct password entered.");
                    passwordScreen.SetActive(false);
                    computerIcons.SetActive(true);
                }
                else
                {
                    instructions.text = "Incorrect password";
                    Debug.Log("Incorrect password.");
                    passwordField.Select();
                    passwordField.ActivateInputField();
                }

                // Clear the password field after checking
                passwordField.text = "";
                passwordField.ActivateInputField(); // Optionally refocus the input field
            }
            else
            {
                if (changePasswordField.text != "" || changePasswordField.text != password)
                {
                    changePasswordScreen.SetActive(false);
                    devicePoint.isAngry = true;
                }
            }


        }
    }

    
}
