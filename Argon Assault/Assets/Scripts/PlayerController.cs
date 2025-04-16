using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject playerCannon;

    [Header("General Setup Settings")]
    [Tooltip("Controls how fast the ship will move")][SerializeField][Range(10,50)] float controlSpeed = 10f;
    [Tooltip("Controls the duration of the player's attack cooldown")][SerializeField] float attackCooldownDuration = 0.2f;
    [Tooltip("Limits how far the ship can move from the center of the screen in the x-axis")][SerializeField] float xClampRange = 10f;
    [Tooltip("Limits how far the ship can move from the center of the screen in the y-axis")][SerializeField] float yClampRange = 4.5f;

    [Header("Screen Position-based Tuning")]
    [Tooltip("Modifies the angular direction that the nose will face based on the ship's current position")][SerializeField] float positionPitchFactor = -0.5f;
    [Tooltip("Modifies the rotation (y-axis) of the ship's body based on ship's current position")][SerializeField] float positionYawFactor = 0.5f;

    [Header("Player Input-based Tuning")]
    [Tooltip("Modifies the angular direction that the nose will face based on player input or lack thereof")][SerializeField] float controlPitchFactor = -20f;
    [Tooltip("Modifies the intensity of how the ship will tilt left/right when moving based on player input or lack thereof")][SerializeField] float controlRollFactor = 4f;

    float xInput, yInput;

    bool attackOnCooldown = false;
    float attackCooldownTimer = 0f;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToInput = yInput * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToInput;         // angular direction of the nose
        float yaw = transform.localPosition.x * positionYawFactor;  // facing direction of the nose
        float roll = transform.localPosition.x * controlRollFactor; // circular rotation of the nose

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        float xOffset = xInput * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xClampRange, xClampRange);

        float yOffset = yInput * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yClampRange, yClampRange + 3);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        CheckIfAttackOnCooldown();                                             // Sets the attackOnCooldown variable's value to true/false

        if (!Input.GetKey(KeyCode.Mouse0) || attackOnCooldown) { return; }     // Check if player is pressing the attack btn or if attack is not on cooldown

        attackCooldownTimer = attackCooldownDuration;

        playerCannon.GetComponent<ParticleSystem>().Emit(1);

        // Debug.Log("FIRE BUTTON PRESSED");
    }

    void CheckIfAttackOnCooldown()
    {
        attackOnCooldown = attackCooldownTimer > Mathf.Epsilon;
        if (attackCooldownTimer > Mathf.Epsilon)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }
}
