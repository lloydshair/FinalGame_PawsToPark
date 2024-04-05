using UnityEngine;
using TMPro;

public class codePanel : MonoBehaviour
{
    [SerializeField]
    TMP_InputField codeInputField;
    string enteredCode = "";

    // Define your correct code here
    string correctCode = "1234";

    // Start is called before the first frame update
    void Start()
    {
        // Set up a listener for the input field's value changed event
        codeInputField.onValueChanged.AddListener(OnCodeValueChanged);
    }

    void OnCodeValueChanged(string newValue)
    {
        // Update the entered code
        enteredCode = newValue;

        // Check if the entered code matches the correct code
        if (enteredCode == correctCode)
        {
            // If the code is correct, perform actions like opening a door
            player_movement.isDoorOpen = true;
            gameObject.SetActive(false);
        }
    }

    public void AddDigit(string digit)
    {
        // Append the entered digit to the current code
        enteredCode += digit;

        // Limit the length of the code to avoid too many digits
        if (enteredCode.Length > 4)
        {
            enteredCode = ""; // Reset the code if it exceeds 4 digits
        }

        // Update the input field text
        codeInputField.text = enteredCode;
    }

    public void OnBackButtonClick()
    {
        Debug.Log("Back button clicked"); // Add this line for debugging
                                          // Deactivate the code panel
        gameObject.SetActive(false);

        // Resume the game by setting the time scale back to 1
        Time.timeScale = 1f;
    }

}
