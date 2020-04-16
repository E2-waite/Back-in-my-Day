using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speaker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particle_obj;
    List<ParticleSystem> particles = new List<ParticleSystem>();
    void Start()
    {
        foreach(Transform child in particle_obj.transform)
        {
            particles.Add(child.GetComponent<ParticleSystem>());
        }
    }

   public void Emit()
   {
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
   }
}
