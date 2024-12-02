using Cinemachine;
using System;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TMP_InputField _addedCelestialName;
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

    public void AddNewTarget(string newName, Transform newObj, int newHeight, int newRadius)
    {
        CameraInfo newTarget = new()
        {
            Name = newName,
            Planet = newObj,
            OrbitHeight = newHeight,
            OrbitRadius = newRadius
        };

        _listOfCameraTargets.Add(newTarget);
    }

    public void SetAddedTarget()
    {
        SetTarget(_addedCelestialName.text);
    }

    public void SetTarget(string newTarget)
    {
        foreach (var target in _listOfCameraTargets)
        {
            if (target.Name == newTarget)
            {
                SetTargetValues(target);

                if (target.Planet.TryGetComponent<Celestial>(out Celestial component))
                {
                    _statsUI.SetCurrentCelestialStats(component.Stats);
                }
                else
                {
                    _statsUI.SetCurrentCelestialStats(target.Planet.GetComponent<NewCelestial>().Stats);
                }
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
