using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RespawnManager : MonoBehaviour
{
    public Vector3 respawnPoint = new Vector3(24.93f, 3.285f, -15.17098f);
    private CharacterController controller;
    private MessageUI messageUI;
    private bool hasWon = false;
    private bool isRespawning = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

#if UNITY_2023_1_OR_NEWER
        messageUI = Object.FindFirstObjectByType<MessageUI>();
#else
        messageUI = Object.FindObjectOfType<MessageUI>();
#endif
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Death") && !isRespawning)
        {
            isRespawning = true;
            Respawn();
            if (messageUI != null)
                messageUI.ShowMessage("YOU DIED!", Color.red);
            Invoke(nameof(ResetRespawnFlag), 1.5f); 
        }

        if (hit.collider.CompareTag("Victory") && !hasWon)
        {
            hasWon = true;
            if (messageUI != null)
                messageUI.ShowMessage("YOU WON!", Color.green);
            Debug.Log("🎉 You Win! 🎉");
        }
    }

    void Respawn()
    {
        controller.enabled = false;
        transform.position = respawnPoint;
        controller.enabled = true;
        Debug.Log("✅ Player respawned at: " + respawnPoint);
    }

    void ResetRespawnFlag()
    {
        isRespawning = false;
    }
}
