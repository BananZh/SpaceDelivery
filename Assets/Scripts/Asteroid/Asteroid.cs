using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private GameObject _asteroidExplosion;
    [SerializeField] private GameObject _bulletExplosion;
    public void SetHP(float val) { _hp = val; }
    void Start()
    {
        _asteroidExplosion = GameObject.Find("AsteroidExplosion");
        _bulletExplosion = GameObject.Find("BulletExplosion");
        if (_asteroidExplosion == null) Debug.LogWarning("Loading of AsteroidExplosion GameObject failed!");
        if (_bulletExplosion == null) Debug.LogWarning("Loading of BulletExplosion GameObject failed!");
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Transform bulletTransform = collision.transform;
            float damage = bulletTransform.GetComponent<Bullet>()._damage;
            _hp -= damage;
            GameObject explosion = Instantiate(_bulletExplosion, bulletTransform.position, Quaternion.identity);
            explosion.GetComponent<ParticleSystem>().Play();
            Destroy(bulletTransform.gameObject);
            print(gameObject.name + " taken " + damage + " damage." + (int)_hp + " hp left.");
            if (_hp <= 0f)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        GameObject explosion = Instantiate(_asteroidExplosion, transform.position, transform.rotation);
        explosion.GetComponent<ParticleSystem>().Play();
        explosion.transform.localScale = transform.localScale * explosion.transform.localScale.x;
        Destroy(gameObject);
    }
}
