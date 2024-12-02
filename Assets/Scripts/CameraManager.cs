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
    [SerializeField] private CinemachineFreeLook _freeLookCamera;
    [SerializeField] private List<CameraInfo> _listOfCameraTargets;
    [SerializeField] private string _initialTarget;
    [SerializeField] private float _rotationMultiplier;
    private StatsUI _statsUI;

    private void Awake()
    {
        _statsUI = FindObjectOfType<StatsUI>();
    }

    private void Start()
    {
        SetTarget(_initialTarget);
    }

    private void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
        _freeLookCamera.m_XAxis.Value += _rotationMultiplier * Time.deltaTime;
    }

    public void SetTarget(string newTarget)
    {
        foreach (var target in _listOfCameraTargets)
        {
            if (target.Name == newTarget)
            {
                SetTargetValues(target);
                _statsUI.SetCurrentCelestialStats(target.Planet.GetComponent<Celestial>().Stats);
            }
        }
    }

    private void SetTargetValues(CameraInfo cameraInfo)
    {
        _freeLookCamera.Follow = cameraInfo.Planet;
        _freeLookCamera.LookAt = cameraInfo.Planet;

        for (int i = 0; i < _freeLookCamera.m_Orbits.Length; i++)
        {            
            _freeLookCamera.m_Orbits[i].m_Height = cameraInfo.OrbitHeight;
            _freeLookCamera.m_Orbits[i].m_Radius = cameraInfo.OrbitRadius;
        }
    }

    private void AddNewTarget(CameraInfo cameraInfo)
    {
        _listOfCameraTargets.Add(cameraInfo);
    }
}
