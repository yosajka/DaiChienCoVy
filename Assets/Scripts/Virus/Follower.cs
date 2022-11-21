using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

public class Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    private float speed = 5f;

    float distanceTraveled;
    private float destination_x;
    private bool hasDestination = false;
    private float minDistance = 1000;
    private bool reached = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += speed * Time.deltaTime;

        Vector2 nextpointPos = new Vector2();
        nextpointPos = pathCreator.path.GetPointAtDistance(distanceTraveled);

        if (hasDestination) MoveToDestination(nextpointPos);
        else JustMove(nextpointPos);
    }

    private void JustMove(Vector2 nextpointPos)
    {
        transform.position = nextpointPos;
    }

    private void MoveToDestination(Vector2 nextpointPos)
    {
        if (reached) return;
        float distance = Mathf.Abs(transform.position.x - destination_x);
        if (!reached && minDistance > distance)
        {
            minDistance = distance;
            transform.position = nextpointPos;
        }
        else
        {
            reached = true;
            gameObject
                .GetComponent<EnemyCampaignControl>()
                .StartFire();
        };
    }

    public void SetDestination(float x)
    {
        hasDestination = true;
        destination_x = x;
    }

    public void SetDestinationByRational()
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }


}
