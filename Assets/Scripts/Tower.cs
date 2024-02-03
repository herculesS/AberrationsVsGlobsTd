using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _range = 10f;
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private GameObject _projectile;


    public float Range { get => _range; private set => _range = value; }
    public GameObject Target { get => _target; set => _target = value; }
    public float FireRate { get => _fireRate; private set => _fireRate = value; }

    private GameObject _target;
    private float timeSinceLastShot = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }
    void UpdateTimers()
    {
        timeSinceLastShot += Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        if (Target == null)
        {
            getNewTarget();
        }
        else
        {
            if (timeSinceLastShot > 1f / FireRate)
            {
                Shoot();
                timeSinceLastShot = 0f;
            }
        }


    }
    private void Shoot()
    {
        Vector3 diretion = (Target.transform.position - transform.position).normalized;
        GameObject obj = Instantiate(_projectile, transform.position, Quaternion.identity);
        Projectile proj = obj.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Target = Target;
        }
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = diretion * 12f;
        }
    }
    private void getNewTarget()
    {
        GameObject closestEnemy = EnemyManager.Instance.GetClosestEnemyPosition(transform.position, Range);
        Target = closestEnemy;

    }
}
