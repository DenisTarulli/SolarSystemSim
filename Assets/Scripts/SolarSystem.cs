using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    private readonly float G = 100f;
    private List<GameObject> _celestials = new List<GameObject>();
    [SerializeField] private Celestial _initialCelestial;

    private void Start()
    {
        GameObject[] celestialsToAdd = GameObject.FindGameObjectsWithTag("Celestial");

        foreach (GameObject cel in celestialsToAdd)
        {
            _celestials.Add(cel);
        }

        InitialVelocity();
        Invoke(nameof(SetInitialStats), 0.2f);
    }

    private void FixedUpdate()
    {
        Gravity();
    }

    public void AddCelestial(GameObject celestialToAdd)
    {
        _celestials.Add(celestialToAdd);
        NewCelestialForce(celestialToAdd);
    }

    private void SetInitialStats()
    {
        FindObjectOfType<StatsUI>().SetCurrentCelestialStats(_initialCelestial.Stats);
    }

    private void Gravity()
    {
        foreach (GameObject a in _celestials)
        {
            foreach (GameObject b in _celestials)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    Vector3 forceToApply = (b.transform.position - a.transform.position).normalized *
                        (G * (m1 * m2) / (r * r));

                    if (a.TryGetComponent<Celestial>(out Celestial component))
                    {
                        if (a.name != "Sun" && b.name == "Sun" && component.Stats.InitialGravitationalForce == 0f)
                            a.GetComponent<Celestial>().Stats.InitialGravitationalForce = forceToApply.magnitude;
                    }
                    else
                    {
                        if (a.name != "Sun" && b.name == "Sun" && a.GetComponent<NewCelestial>().Stats.InitialGravitationalForce == 0f)
                            a.GetComponent<NewCelestial>().Stats.InitialGravitationalForce = forceToApply.magnitude;
                    }                   

                    a.GetComponent<Rigidbody>().AddForce(forceToApply);
                }
            }
        }
    }

    private void NewCelestialForce(GameObject newCelestial)
    {
        foreach (GameObject a in _celestials)
        {
            foreach (GameObject b in _celestials)
            {
                if (!a.Equals(b) && a.Equals(newCelestial))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    Vector3 newVelocity = a.transform.right * Mathf.Sqrt((G * m2) / r);

                    if (a.name != "Sun" && b.name == "Sun")
                        a.GetComponent<NewCelestial>().Stats.TangentialVelocity = newVelocity.magnitude;

                    a.GetComponent<Rigidbody>().velocity += newVelocity;
                }
            }
        }
    }

    private void InitialVelocity()
    {
        foreach (GameObject a in _celestials)
        {
            foreach (GameObject b in _celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);
                    a.transform.GetChild(1).LookAt(b.transform);

                    Vector3 newVelocity = a.transform.right * Mathf.Sqrt((G * m2) / r);

                    if (a.name != "Sun" && b.name == "Sun")
                        a.GetComponent<Celestial>().Stats.TangentialVelocity = newVelocity.magnitude;

                    a.GetComponent<Rigidbody>().velocity += newVelocity;
                }
            }
        }
    }
}
