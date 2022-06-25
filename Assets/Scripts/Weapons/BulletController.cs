using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float damage, bulletRange;
    float impactEffectTime;

    Pooler bulletPool;
    Rigidbody rigidB;

    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject beam;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject trail;
    TrailRenderer trailRenderer;

    void Awake()
    {
        rigidB = GetComponent<Rigidbody>();

        impactEffectTime = impactEffect.GetComponentInChildren<ParticleSystem>().main.startLifetime.constantMax;

        trailRenderer = trail.GetComponent<TrailRenderer>();
    }
    void OnEnable()
    {
        StartCoroutine(nameof(IDestroyBulletAfterTime));
    }
    void OnDisable()
    {
        rigidB.velocity = Vector3.zero;
    }
    void Start()
    {
        bulletPool = transform.parent.GetComponent<Pooler>();
    }
    void OnCollisionEnter(Collision collided)
    {
        StopCoroutine(nameof(IDestroyBulletAfterTime));

        if (collided.gameObject.CompareTag("Enemy"))
        {
            CharacterStats enemyStat = collided.gameObject.GetComponent<CharacterStats>();

            enemyStat.TakeDamage(damage);
        }
        
        StartCoroutine(IPlayImpactEffect());
    }
    IEnumerator IDestroyBulletAfterTime()
    {
        yield return new WaitForSeconds(bulletRange);

        ChildrenSetActive(false);

        bulletPool.ReturnObject(gameObject);
    }
    IEnumerator IPlayImpactEffect()
    {
        rigidB.velocity = Vector3.zero;
        ChildrenSetActive(false);

        impactEffect.transform.position = transform.position;
        impactEffect.transform.rotation = transform.rotation;
        impactEffect.SetActive(true);

        yield return new WaitForSeconds(impactEffectTime);

        impactEffect.SetActive(false);

        bulletPool.ReturnObject(gameObject);
    }
    public void Setup(Transform barrel, Equipment equipment, CharacterStats stats)
    {
        bulletRange = equipment.projectileRangeModifier.GetValue();

        this.damage = stats.damage.GetValue();

        transform.position = barrel.position;
        transform.rotation = barrel.rotation;

        impactEffect.SetActive(false);
        ChildrenSetActive(true);
        gameObject.SetActive(true);

        rigidB.AddRelativeForce(Vector3.forward * equipment.projectileSpeedModifier.GetValue(), ForceMode.Impulse);
    }

    void ChildrenSetActive(bool state)
    {
        trailRenderer.Clear();
        trail.SetActive(state);
        beam.SetActive(state);
        projectile.SetActive(state);
    }
}
