using System.Linq;
using UnityEngine;

public class LookToWalk : MonoBehaviour
{
    private bool isWalking;
    private float walkSpeed = 3.0f;
    private Camera mainCamera;
    private AudioSource walkingAudio;

    private void Start()
    {
        mainCamera = Camera.main;
        walkingAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (mainCamera.transform.eulerAngles.x >= 35.0f && mainCamera.transform.eulerAngles.x < 90.0f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void FixedUpdate()
    {
        if (isWalking && !GameManager.Instance.isGamePaused)
        {
            MovePlayer();
            if (!walkingAudio.isPlaying)
            {
                walkingAudio.Play();
            }
        }
        else
        {
            walkingAudio.Stop();
        }
    }

    private void MovePlayer()
    {
        transform.Translate(Time.deltaTime * walkSpeed * mainCamera.transform.forward);
    }
}
