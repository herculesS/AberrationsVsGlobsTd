using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D _rb;
    GameObject _target;
    float _speed = 20f;
    int _damage = 1;

    public Rigidbody2D Rb { get => _rb; set => _rb = value; }
    public GameObject Target { get => _target; set => _target = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public int Damage { get => _damage; set => _damage = value; }

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        Vector3 diretion = (Target.transform.position - transform.position).normalized;
        var rot = Quaternion.LookRotation(diretion);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 10f * Time.deltaTime);
        Rb.velocity = diretion * Speed;
    }


}