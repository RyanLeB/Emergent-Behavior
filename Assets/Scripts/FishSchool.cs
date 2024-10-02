using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class FishSchool : MonoBehaviour
{
    // ---- Defining prefab for the agents ----
    public SchoolingAgent agentPrefab;
    List<SchoolingAgent> agents = new List<SchoolingAgent>();
    public SchoolBehavior behavior;

    // ---- Defining the number of agents ----
    [Range(10, 600)]
    public int startingSize = 300;
    const float schoolDensity = 0.08f;

    // ---- Speed of the agents ----
    [Range(1f, 120f)]
    public float speedFactor = 10f;
    [Range(1f, 120f)]
    public float maxSpeed = 5f;

    // ---- Radius of the agents ----
    [Range(1f, 10f)]
    public float fishRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidMultiplier = 0.5f;

    // ---- UI Elements ----
    public Slider fishCountSlider;
    public TMP_Text fishCountText;

    // ---- Squares to avoid step of math ----
    float squareMaxSpeed;
    float squareFishRadius;
    float squareAvoidRadius;
    public float SquareAvoidRadius { get { return squareAvoidRadius; } }

    // ---- Start is called before the first frame update ----
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareFishRadius = fishRadius * fishRadius;
        squareAvoidRadius = squareFishRadius * avoidMultiplier * avoidMultiplier;

        // ---- Initialize the birds ----
        for (int i = 0; i < startingSize; i++)
        {
            SchoolingAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingSize * schoolDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );
            newAgent.name = "Seagull " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }

        // ---- Initialize UI elements ----
        if (fishCountSlider != null)
        {
            fishCountSlider.minValue = 10;
            fishCountSlider.maxValue = 600;
            fishCountSlider.value = startingSize;
            fishCountSlider.onValueChanged.AddListener(UpdateFishCount);
        }
    }

    // ---- Update is called once per frame ----
    void Update()
    {
        foreach (SchoolingAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            // For testing purposes
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            // ---- Behavior takes over to direct the move direction ----
            Vector2 move = behavior.CalculateMove(agent, context, this);

            move *= speedFactor;
            // ---- Limits the speed ----
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }

        // ---- Update UI text ----
        if (fishCountText != null)
        {
            fishCountText.text = "Fish Count: " + agents.Count;
        }
    }

    // ---- Get the nearby objects -----
    List<Transform> GetNearbyObjects(SchoolingAgent agent)
    {
        List<Transform> context = new List<Transform>();
        // ---- Creates circle to choose what colliders are in it ----
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, fishRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    // ---- Update fish count based on slider value ---- 
    void UpdateFishCount(float value)
    {
        int newCount = Mathf.RoundToInt(value);
        if (newCount > agents.Count)
        {
            for (int i = agents.Count; i < newCount; i++)
            {
                SchoolingAgent newAgent = Instantiate(
                    agentPrefab,
                    Random.insideUnitCircle * startingSize * schoolDensity,
                    Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                    transform
                );
                newAgent.name = "Seagull " + i;
                newAgent.Initialize(this);
                agents.Add(newAgent);
            }
        }
        else if (newCount < agents.Count)
        {
            for (int i = agents.Count - 1; i >= newCount; i--)
            {
                Destroy(agents[i].gameObject);
                agents.RemoveAt(i);
            }
        }
    }
}


