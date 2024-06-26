using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputFieldValidator : MonoBehaviour
{
    public TMP_InputField inputField;
    public string targetSceneName;

    public void ValidateInputAndRedirect()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            string playerName = inputField.text;
            PlayerPrefs.SetString("PlayerName", playerName);
            SceneManager.LoadScene(targetSceneName);
        }
        else
        {
            Debug.Log("Veuillez saisir un nom !");
        }
    }
}