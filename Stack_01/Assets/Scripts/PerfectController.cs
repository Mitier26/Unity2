using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectController : MonoBehaviour
{
    [SerializeField]
    private CubeSpawner cubeSpawner;
    [SerializeField]
    private Transform perfectEffect;

    [SerializeField]
    private Transform perfectComboEffect;
    [SerializeField]
    private Transform perfectRecoveryEffect;


    private AudioSource audioSource;                // 사운드 출력용

    private float perfectCorrection = 0.01f;        // 퍼팩트 보정 값
    private float addedSize = 0.1f;                 // 퍼팩트시 커지는 값
    private int perfectCombo = 0;

    [SerializeField]
    private int recoveryCombo = 5;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsPerfect(float hangOver)
    {
        if(Mathf.Abs(hangOver) < perfectCorrection)
        {
            EffectProcess();
            SFXProcess();

            perfectCombo++;

            return true;
        }
        else
        {
            perfectCombo = 0;
            return false;
        }
    }

    private void EffectProcess()
    {
        // 이펙트 생성 위치
        Vector3 position = cubeSpawner.LastCube.position;
        position.y = cubeSpawner.CurrentCube.transform.position.y - cubeSpawner.CurrentCube.transform.localScale.y * 0.5f;

        // 이펙트 크기
        Vector3 scale = cubeSpawner.CurrentCube.transform.localScale;
        scale = new Vector3(scale.x + addedSize, perfectEffect.localScale.y, scale.z + addedSize);


        // 이펙트 생성
        OnPerfectEffect(position, scale);

        if(perfectCombo > 0 && perfectCombo < recoveryCombo)
        {
            StartCoroutine(OnPerfectComboEffect(position, scale));
        }
        else if(perfectCombo >= recoveryCombo)
        {
            OnPerfectRecoveryEffect();
        }
    }

    private void OnPerfectEffect(Vector3 position, Vector3 scale)
    {
        // 이펙트 생성 위치
        //Vector3 position = cubeSpawner.LastCube.position;
        //position.y = cubeSpawner.CurrentCube.transform.position.y - cubeSpawner.CurrentCube.transform.localScale.y * 0.5f;

        //// 이펙트 크기
        //Vector3 scale = cubeSpawner.CurrentCube.transform.localScale;
        //scale = new Vector3(scale.x + addedSize, perfectEffect.localScale.y, scale.z + addedSize);

        Transform effect = Instantiate(perfectEffect);
        effect.position = position;
        effect.localScale = scale;
    }

    private void SFXProcess()
    {
        int maxCombo = 5;
        float volumeMin = 0.3f;
        float volumeAdditive = 0.15f;
        float pitchMin = 0.7f;
        float pitchAdditive = 0.15f;

        if(perfectCombo < maxCombo)
        {
            audioSource.volume = volumeMin + perfectCombo * volumeAdditive;
            audioSource.pitch = pitchMin + perfectCombo * pitchAdditive;
        }

        audioSource.Play();
        
    }

    private IEnumerator OnPerfectComboEffect(Vector3 position, Vector3 scale)
    {
        int currentCombo = 0;
        float beginTime = Time.time;
        float duration = 0.15f;

        while(currentCombo < perfectCombo)
        {
            float t = (Time.time - beginTime) / duration;

            if(t >= 1)
            {
                Transform effect = Instantiate(perfectComboEffect);
                effect.position = position;
                effect.localScale = scale;

                beginTime= Time.time;

                currentCombo++;
            }

            yield return null;
        }
    }

    public void OnPerfectRecoveryEffect()
    {
        Transform effect = Instantiate(perfectRecoveryEffect);

        effect.position = cubeSpawner.CurrentCube.transform.position;

        // 이펙트의 생성 반경
        var shape = effect.GetComponent<ParticleSystem>().shape;
        float radius = cubeSpawner.CurrentCube.transform.localScale.x > cubeSpawner.CurrentCube.transform.localScale.z ?
            cubeSpawner.CurrentCube.transform.localScale.x : cubeSpawner.CurrentCube.transform.localScale.z;

        shape.radius = radius;
        shape.radiusThickness = radius * 0.5f;

        cubeSpawner.CurrentCube.RecoveryCube();
    }
}
