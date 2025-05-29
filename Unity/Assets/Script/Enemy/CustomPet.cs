using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CustomPet : MonoBehaviour
{
    private Path path;
    public float speed = 200f;
    public Transform target;
    public float nextWayPointDistance = 3f;
    private int currenWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Seeker seeker;
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();

        //seeker.StartPath(rb2d.position, target.position, OnPathComplete);
        InvokeRepeating("UpdatePath", 1f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone()) { seeker.StartPath(rb2d.position, target.position, OnPathComplete); }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currenWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null) return;
        if (currenWaypoint >= path.vectorPath.Count)
        { reachedEndOfPath = true; return; }
        else { reachedEndOfPath = false; }
        Vector2 diraction = ((Vector2)path.vectorPath[currenWaypoint] - rb2d.position).normalized;
        Vector2 force = diraction * speed * Time.deltaTime;
        rb2d.AddForce(force);
        float distance = Vector2.Distance(rb2d.position, path.vectorPath[currenWaypoint]);
        if (distance < nextWayPointDistance) { currenWaypoint++; }
        if (force.x >= 0.01f) { transform.localScale = new Vector3(-1, 1, 1); }
        else if (force.x <= 0.01f) { transform.localScale = new Vector3(1, 1, 1); }
    }
}
