using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    private readonly float G = 100f;
    private GameObject[] _celestials;

    private void Start()
    {
        _celestials = GameObject.FindGameObjectsWithTag("Celestial");

        InitialVelocity();
    }

    private void FixedUpdate()
    {
        Gravity();
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

                    if (a.name != "Sun" && b.name == "Sun" && a.GetComponent<Celestial>().Stats.InitialGravitationalForce == 0f)
                        a.GetComponent<Celestial>().Stats.InitialGravitationalForce = forceToApply.magnitude;

                    a.GetComponent<Rigidbody>().AddForce(forceToApply);
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
