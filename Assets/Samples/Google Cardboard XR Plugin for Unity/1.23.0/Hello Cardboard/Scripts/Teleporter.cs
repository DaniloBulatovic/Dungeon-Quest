using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Teleporter Colors")]
    [SerializeField] private Color inactiveColor = Color.white;
    [SerializeField] private Color gazeColor = Color.yellow;

    [Header("Teleportation Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private float maxGazeDetectionTime = 2f;
    private float elapsedGazeDetectionTime = 0f;

    private MeshRenderer meshRenderer;
    private bool isColorChanging = false;

    private AudioSource teleportAudio;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        teleportAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        meshRenderer.material.color = inactiveColor;   
    }

    private void Update()
    {
        if (isColorChanging && !GameManager.Instance.isGamePaused)
        {
            meshRenderer.material.color = Color.Lerp(inactiveColor, gazeColor, elapsedGazeDetectionTime/maxGazeDetectionTime);
            elapsedGazeDetectionTime += Time.deltaTime;

            if(elapsedGazeDetectionTime >= maxGazeDetectionTime)
            {
                isColorChanging = false;
                TeleportPlayerToPosition(transform.position);
                teleportAudio.PlayOneShot(teleportAudio.clip);
                meshRenderer.material.color = inactiveColor;
            }
        }
    }

    private void TeleportPlayerToPosition(Vector3 targetPosition)
    {
        Vector3 teleportPosition = new Vector3(targetPosition.x, player.transform.position.y, targetPosition.z);
        player.transform.position = teleportPosition;
    }

    public void OnPointerEnter()
    {
        Gaze(true);
    }

    public void OnPointerExit()
    {
        Gaze(false);
    }

    public void Gaze(bool isGazing)
    {
        if (isGazing)
        {
            isColorChanging = true;
        }
        else
        {
            elapsedGazeDetectionTime = 0f;
            isColorChanging = false;
            meshRenderer.material.color = inactiveColor;
        }
    }
}
