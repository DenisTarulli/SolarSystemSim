using UnityEngine;

public class Celestial : MonoBehaviour
{
    [SerializeField] public CelestialStats Stats;
    private DatabaseManager _databaseManager;
    private Rigidbody _rb;

    private void Awake()
    {
        Stats = new CelestialStats();

        _rb = GetComponent<Rigidbody>();

        _databaseManager = FindObjectOfType<DatabaseManager>();

        SetStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _databaseManager.SaveData(Stats);
    }

    private void SetStats()
    {
        Stats.Name = gameObject.name;

        if (gameObject.name == "Earth")
            Stats.Mass = 1f;       
        else
            Stats.Mass = _rb.mass;

        Stats.Radius = transform.localScale.x;
        Stats.DistanceToSun = transform.position.x;
    }
}
