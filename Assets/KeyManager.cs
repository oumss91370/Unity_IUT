using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyManager : MonoBehaviour
{
    public TMP_Text keyCountText; // Référence au composant Text pour afficher le nombre de clés collectées
    private int keyCount = 0; // Compteur de clés collectées

    void Start()
    {
        UpdateKeyCountUI(); // Met à jour l'affichage initial du compteur au démarrage
    }

    public void CollectKey()
    {
        // Incrémente le compteur de clés
        keyCount++;
        
        // Met à jour l'affichage du compteur
        UpdateKeyCountUI();

        Debug.Log("Key Collected! Total Keys: " + keyCount);
    }

    void UpdateKeyCountUI()
    {
        if (keyCountText != null)
        {
            keyCountText.text = keyCount.ToString(); // Met à jour le texte du compteur avec la nouvelle valeur de keyCount
        }
        else
        {
            Debug.LogWarning("Key Count Text not assigned in KeyManager.");
        }
    }
}
