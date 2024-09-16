using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed;
    [SerializeField] [Range(1f, 20f)] float _hitPoint = 2.5f;
    [SerializeField] GameObject _hitEffectPrefab;
    void Start()
    {
        Destroy(gameObject, 3f);
    }
    private void Update()
    {
        transform.Translate(Vector2.up * _bulletSpeed *Time.deltaTime); // due to my Canon Sprite

        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 12)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(_hitPoint);
        }
        HitEffect();

        //Debug.Log(name + " attack "+ collision.name);
    }

    private void HitEffect()
    {
        GameObject hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
        Destroy(hitEffect, 0.5f);
        Destroy(gameObject);
    }
}