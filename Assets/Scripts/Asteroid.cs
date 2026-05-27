using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _hp;
    public void SetHP(float val) { _hp = val; }
    // void Start()
    // {
    //     _hp = transform.localScale.x;
    // }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            float damage = collision.gameObject.GetComponent<Bullet>()._damage;
            _hp -= damage;
            Destroy(collision.gameObject);
            print(gameObject.name + " taken " + damage + " damage.");
            if (_hp <= 0f)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
