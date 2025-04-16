using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioSource mainThrusterSFX;
    [SerializeField] AudioSource leftThrusterSFX;
    [SerializeField] AudioSource rightThrusterSFX;
    [SerializeField] ParticleSystem mainThrusterVFX;
    [SerializeField] ParticleSystem leftThrusterVFX;
    [SerializeField] ParticleSystem rightThrusterVFX;

    // Cache - e.g. references for readability or speed.
    Rigidbody playerBody;

    // State - private instance (member) variables.

    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        // As much as possible, do not use string references:
        // Input.GetKey("Space")

        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else if(mainThrusterSFX.isPlaying)
        {
            StopMainThrusterEffects();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else if(leftThrusterVFX.isPlaying || rightThrusterVFX.isPlaying)
        {
            StopSideThrusterEffects();
        }
    }

    private void StartThrusting()
    {
        playerBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!mainThrusterSFX.isPlaying)
        {
            mainThrusterSFX.Play();
        }
        if (!mainThrusterVFX.isPlaying)
        {
            mainThrusterVFX.Play();
        }
    }

    private void StopMainThrusterEffects()
    {
        mainThrusterSFX.Stop();
        mainThrusterVFX.Stop();
    }

    private void RotateLeft()
    {
        if (!leftThrusterSFX.isPlaying)
        {
            leftThrusterSFX.Play();
        }
        if (!leftThrusterVFX.isPlaying)
        {
            leftThrusterVFX.Play();
        }

        ApplyRotation(rotationThrust);
    }

    private void RotateRight()
    {
        if (!rightThrusterSFX.isPlaying)
        {
            rightThrusterSFX.Play();
        }
        if (!rightThrusterVFX.isPlaying)
        {
            rightThrusterVFX.Play();
        }

        ApplyRotation(-rotationThrust);
    }

    private void StopSideThrusterEffects()
    {
        leftThrusterSFX.Stop();
        rightThrusterSFX.Stop();
        leftThrusterVFX.Stop();
        rightThrusterVFX.Stop();
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        // Freezing rotation so we can manually rotate.
        playerBody.freezeRotation = true;

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        // Unfreezing the rotation so the physics system can take over.
        playerBody.freezeRotation = false;
    }

    // The player's movement script is disabled on-death, triggering this method.
    void OnDisable()
    {
        OnDeath_StopSFX();
        OnDeath_StopVFX();
    }

    private void OnDeath_StopSFX()
    {
        if (mainThrusterSFX && mainThrusterSFX.isPlaying)
        {
            mainThrusterSFX.Stop();
        }

        if (leftThrusterSFX && leftThrusterSFX.isPlaying)
        {
            leftThrusterSFX.Stop();
        }

        if (rightThrusterSFX && rightThrusterSFX.isPlaying)
        {
            rightThrusterSFX.Stop();
        }
    }

    private void OnDeath_StopVFX()
    {
        if (mainThrusterVFX && mainThrusterVFX.isPlaying)
        {
            mainThrusterVFX.Stop();
        }

        if (leftThrusterVFX && leftThrusterVFX.isPlaying)
        {
            leftThrusterVFX.Stop();
        }

        if (rightThrusterVFX && rightThrusterVFX.isPlaying)
        {
            rightThrusterVFX.Stop();
        }
    }
}
