using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellSpawner : MonoBehaviour
{
    public int numer_of_columns;
    public float speed;
    public Sprite texture;
    public Color color;
    public float lifeTime;
    public float firerate;
    public float size;
    public float angle;
    public Material material;
    public float spinSpeed;
    private float time;

    public ParticleSystem system;

    private void Awake()
    {
        Summon();
    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, time * spinSpeed);
    }

    void Summon()
    {
        angle = 360f / numer_of_columns;

        for(int i = 0; i < numer_of_columns; i++)
        {
            // A simple particle material with no texture.
            Material particleMaterial = material;

            // Create a green Particle System.
            var go = new GameObject("Particle System");
            go.transform.Rotate(angle * i, 90, 0); // Rotate so the system emits upwards.
            go.transform.parent = this.transform;
            go.transform.position = this.transform.position;
            system = go.AddComponent<ParticleSystem>();
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            mainModule.startColor = Color.green;
            mainModule.startSize = 0.5f;

            // Ãß
            mainModule.startSpeed = speed;
            mainModule.maxParticles = 10000;
            //mainModule.duration = 0f;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;
            var emission = system.emission;
            emission.enabled = false;

            var shape = system.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Sprite;
            shape.sprite = null;

            var texturnAnim = system.textureSheetAnimation;
            texturnAnim.mode = ParticleSystemAnimationMode.Sprites;
            texturnAnim.AddSprite(texture);
        }
        
        // Every 2 secs we will emit.
        InvokeRepeating("DoEmit", 0f, firerate);
    }

    void DoEmit()
    {
        foreach(Transform child in transform)
        {
            system = child.GetComponent<ParticleSystem>();
            // Any parameters we assign in emitParams will override the current system's when we call Emit.
            // Here we will override the start color and size.
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = color;
            emitParams.startSize = size;
            emitParams.startLifetime = lifeTime;
            system.Emit(emitParams, 10);
            //system.Play(); // Continue normal emissions
        }
    }
}
