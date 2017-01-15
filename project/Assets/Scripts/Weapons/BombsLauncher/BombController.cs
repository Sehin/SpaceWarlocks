using Damage;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public float explosionPower = 700.0f;
    public float explosionRadius = 30.0f;
    public float explosionDamage = 100.0f;
    public float lifetime = 15;
    public GameObject explosion;

    // Use this for initialization
    void Start()
    {
        if (lifetime > 0.0001f) Destroy(this, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        explode(collision);
        var explosionAnim = Instantiate(explosion, transform.parent);
        explosionAnim.transform.position = transform.position;
        Destroy(explosionAnim, 3.0f);  //todo надо обратится к particle system и выдрать duration и подставить вместо 3. МАКС СДЕЛАЙ ЭТО ПОЖАЛУЙСТА :D
    }

    private void explode(Collision collision)
    {
        var explosionPos = transform.position;
        var colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (var hit in colliders)
        {
            var hittedObject = hit.gameObject;
            var objectHealth = hittedObject.GetComponentInParent<Health>(); //todo: Это надо переписать. Или вешать хелс на куб, или как то по другому его искать
            if (objectHealth != null)
            {
                objectHealth.TakeDamage(new DamageDealer(this, (explosionDamage / Vector3.Distance(transform.position, hittedObject.transform.position)), DamageType.EXPLOSSION));
            }
            if (hit.attachedRigidbody)
            {
                hit.attachedRigidbody.AddExplosionForce(explosionPower, explosionPos, explosionRadius);
            }
        }
        Destroy(gameObject);
    }

    public override string ToString()
    {
        return "default bomb";
    }
}
