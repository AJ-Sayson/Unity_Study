using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    ParticleSystem explosionFX;
    void Start() 
    {
        explosionFX = GetComponent<ParticleSystem>();
        Destroy(gameObject, explosionFX.main.duration); // Destroy this gameObject according to the specified duration of the Particle System.
    }
}
