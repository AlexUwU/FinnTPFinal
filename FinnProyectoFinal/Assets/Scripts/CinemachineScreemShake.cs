using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineScreemShake : MonoBehaviour
{
    public static CinemachineScreemShake Instance;

    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    private float timeMove;
    private float totalMoveInit;
    private float amplitudeInit;
    
    private void Awake() {
        if(Instance == null){
            Instance=this;
        }else{
            Destroy(gameObject);
        }
        cinemachineVirtualCamera= GetComponent<CinemachineVirtualCamera>();
        cinemachineBasicMultiChannelPerlin=cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void moveCamera(float amplitude, float frecuency, float time){
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain=amplitude;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain=frecuency;
        amplitudeInit =  amplitude;
        totalMoveInit = time;
        timeMove = time;
    }

    private void Update() {
        if(timeMove>0){
            timeMove-=Time.deltaTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain=Mathf.Lerp(amplitudeInit,0,1 - (timeMove/totalMoveInit));
        }
    }
}
