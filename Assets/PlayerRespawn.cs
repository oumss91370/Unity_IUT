using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector3 respawnPosition;

    void Start()
    {
        // Enregistrez la position initiale du joueur comme point de réapparition
        respawnPosition = transform.position;
    }

    public void Respawn()
    {
        // Réinitialisez la position du joueur à la position de réapparition
        transform.position = respawnPosition;
        Debug.Log("Player respawned to initial position.");
    }
}
