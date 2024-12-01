using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CameraInfo
{
    public string Name;
    public Transform Planet;
    public int OrbitHeight;
    public int OrbitRadius;
}

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLookCamera;
    [SerializeField] private List<CameraInfo> listOfCameraTargets;

    public void SetTarget(string newTarget)
    {
        foreach (var target in listOfCameraTargets)
        {
            if (target.Name == newTarget)
                SetTargetValues(target);
        }
    }

    private void SetTargetValues(CameraInfo cameraInfo)
    {
        freeLookCamera.Follow = cameraInfo.Planet;
        freeLookCamera.LookAt = cameraInfo.Planet;

        for (int i = 0; i < freeLookCamera.m_Orbits.Length; i++)
        {            
            freeLookCamera.m_Orbits[i].m_Height = cameraInfo.OrbitHeight;
            freeLookCamera.m_Orbits[i].m_Radius = cameraInfo.OrbitRadius;
        }
    }

    private void AddNewTarget(CameraInfo cameraInfo)
    {
        listOfCameraTargets.Add(cameraInfo);
    }
}
