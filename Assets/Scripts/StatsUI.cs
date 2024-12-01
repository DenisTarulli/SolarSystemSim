using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _mass;
    [SerializeField] private TextMeshProUGUI _radius;
    [SerializeField] private TextMeshProUGUI _distance;
    [SerializeField] private TextMeshProUGUI _tanVel;
    [SerializeField] private TextMeshProUGUI _gravForce;

    public void SetCurrentCelestialStats(CelestialStats currentCelestial)
    {
        _name.text = $"Name: {currentCelestial.Name}";
        _mass.text = $"Mass: {currentCelestial.Mass}";
        _radius.text = $"Radius: {currentCelestial.Radius}";
        _distance.text = $"Distance: {currentCelestial.DistanceToSun}";
        _tanVel.text = $"TanVel: {currentCelestial.TangentialVelocity}";
        _gravForce.text = $"GravForce: {currentCelestial.InitialGravitationalForce}";
    }
}
