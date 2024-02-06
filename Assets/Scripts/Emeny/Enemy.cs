using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    List<Vector3> path;
    Vector3 currentTargetPositionOnPath;
    int pathIndex = 0;
    private bool pathEnded = false;
    int health = 2;

    public EventHandler<EnemyKilledEventArgs> onKilled;

    public bool PathEnded { get => pathEnded; private set => pathEnded = value; }
    public int Health { get => health; set => health = value; }

    public void SetPath(List<Vector3> path)
    {
        this.path = path;
        currentTargetPositionOnPath = path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Destroyed();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Projectile proj = other.gameObject.GetComponent<Projectile>();
        if (proj != null)
        {
            Health -= proj.Damage;
        }
    }
    private bool MoveOnPath()
    {
        if (path == null || path.Count == 0 || pathIndex > path.Count - 1) return false;
        if (currentTargetPositionOnPath == null)
        {
            currentTargetPositionOnPath = path[pathIndex];
        }

        if (Vector3.Distance(currentTargetPositionOnPath, transform.position) < 0.01f && pathIndex < path.Count - 1)
        {
            currentTargetPositionOnPath = path[pathIndex + 1];
            pathIndex++;
        }

        transform.position = Vector3.MoveTowards(transform.position,
                                     currentTargetPositionOnPath,
                                    speed * Time.fixedDeltaTime);
        return true;

    }

    private void FixedUpdate()
    {
        if (!PathEnded)
        {
            if (Vector3.Distance(path[path.Count - 1], transform.position) < 0.1f)
                PathEnded = true;

            MoveOnPath();
        }
        else
        {
            Destroyed();
        }

    }

    void Destroyed()
    {
        onKilled?.Invoke(this, new EnemyKilledEventArgs(gameObject));
    }
}
