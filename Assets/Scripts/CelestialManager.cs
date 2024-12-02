using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CelestialManager : MonoBehaviour
{
    [SerializeField] private GameObject _celestialPrefab;
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _mass;
    [SerializeField] private TMP_InputField _radius;
    [SerializeField] private TMP_InputField _distance;
    [SerializeField] private CameraManager _cameraManager;
    private readonly int _cameraRadiusMultiplier = 3;
    private SolarSystem _solarSystem;

    private void Start()
    {
        _solarSystem = FindObjectOfType<SolarSystem>();
    }

    public void CreateCelestial()
    {
        float newDistance = float.Parse(_distance.text);
        Vector3 spawnPos = new(newDistance, 0f, 0f);
        GameObject newCelestial = Instantiate(_celestialPrefab, spawnPos, Quaternion.identity);

        newCelestial.GetComponent<NewCelestial>().Stats = new();
        CelestialStats stats = newCelestial.GetComponent<NewCelestial>().Stats;

        stats.DistanceToSun = newDistance;

        stats.Name = _name.text;
        newCelestial.name = _name.text;

        float newMass = float.Parse(_mass.text);
        newCelestial.GetComponent<Rigidbody>().mass = newMass;
        stats.Mass = newMass;

        float newRadius = float.Parse(_radius.text);
        newCelestial.transform.localScale = new(newRadius, newRadius, newRadius);
        stats.Radius = newRadius;

        _solarSystem.AddCelestial(newCelestial);

        _cameraManager.AddNewTarget(stats.Name, newCelestial.transform, 15, Mathf.CeilToInt(stats.Radius * _cameraRadiusMultiplier));
    }
}
