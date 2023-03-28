using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProParticleManager : MonoBehaviour
{
    public enum PARTICLE { Coin, Get, Water }

    public static ProParticleManager instance;

    [SerializeField]
    private ParticleSystem[] p_Prefab;

    private List<ParticleSystem>[] particles = new List<ParticleSystem>[3];

    private int makeCount = 5;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        for(int i = 0; i < particles.Length; i++)
        {
            particles[i] = new List<ParticleSystem>();

            GameObject parent = new GameObject(p_Prefab[i].name + "_Holder");
            parent.transform.SetParent(transform);

            for(int j = 0; j < makeCount; j++)
            {
                ParticleSystem p = Instantiate(p_Prefab[i], parent.transform);
                particles[i].Add(p);
            }
        }
    }

    public void PlayParticle(PARTICLE index, Vector2 target)
    {
        ParticleSystem p = new ParticleSystem();

        for(int i = 0; i < particles[(int)index].Count; i++)
        {
            if (!particles[(int)index][i].isPlaying)
            {
                p = particles[(int)index][i];
                break;
            }
        }
       
        if(p == null)
        {
            p = Instantiate(p_Prefab[(int)index]);
            p.transform.SetParent(transform.Find(p_Prefab[(int)index].name + "_Holder"));
            particles[(int)index].Add(p);
        }

        p.transform.position = target;
        p.Play();
    }
}
