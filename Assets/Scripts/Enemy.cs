using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    List<Vector3> path;
    Vector3 currentTargetPositionOnPath;
    int pathIndex = 0;
    private bool _endedPath = false;

    public bool PathEnded { get => _endedPath; private set => _endedPath = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetPath(List<Vector3> _path)
    {
        path = _path;
        currentTargetPositionOnPath = path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {

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
            EnemyManager.Instance.DestroyEnemy(this.gameObject);
        }

    }

    


}
