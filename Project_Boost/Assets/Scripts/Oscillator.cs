using UnityEngine;

public class Oscillator : MonoBehaviour
{
    // Parameters - for tuning, typically set in the editor
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    // Cache - e.g. references for readability or speed.
    Vector3 startingPosition;
    float movementFactor;

    // State - private instance (member) variables.

    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Do not compare float values; they're undpredictable (lots of decimal places)
        // Try to use Mathf.Epsilon instead -- the smallest float
        // Always compare to this rather than 0
        if(period <= Mathf.Epsilon) { return; }

        // Continually growing over time
        float cycles = Time.time / period;

        // Constant Value of 6.283
        const float tau = Mathf.PI * 2;

        // Going from -1 to 1.
        float rawSinWave = Mathf.Sin(cycles * tau);

        // rawSinWave starts from -1, with the addition of 1f, it'll be able to start at 0 then into 2.
        // So we divide it by 2 so that it becomes 0 then into 1.
        // TLDR: recalculated to go from 0 to 1.
        movementFactor = (rawSinWave + 1f) / 2f;

        // The movementFactor basically acts as a 'percentage' for the movementVector since it has a max value of 1.
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
