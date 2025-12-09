using UnityEngine;

public class VerySimpleExtinguisher : MonoBehaviour
{
    public ParticleSystem foamParticle;
    
    private bool isHeld = false;
    private bool isNozzlePressed = false;

    public void OnGrab()
    {
        isHeld = true;
        Debug.Log("Extintor agarrado");
    }

    public void OnRelease()
    {
        isHeld = false;
        isNozzlePressed = false;
        Debug.Log("Extintor soltado");
        if (foamParticle != null) foamParticle.Stop();
    }

    public void OnNozzlePress()
    {
        if (!isHeld) return;
        isNozzlePressed = true;
        Debug.Log("Boquilla presionada");
        if (foamParticle != null) foamParticle.Play();
    }

    public void OnNozzleRelease()
    {
        isNozzlePressed = false;
        Debug.Log("Boquilla liberada");
        if (foamParticle != null) foamParticle.Stop();
    }
}