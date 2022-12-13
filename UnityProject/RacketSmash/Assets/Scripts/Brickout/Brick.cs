using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameObject destroyParticle;
    public AudioSource destroySound;

    private void Awake()
    {
        destroyParticle = Resources.Load<GameObject>("Brick_Destory_Particle");
    }

    private void OnCollisionEnter(Collision collision)
    {
        ShowBrickDestoryEffect(transform.position);
        destroySound.PlayOneShot(destroySound.clip);
        Destroy(gameObject);
    }

    private void ShowBrickDestoryEffect(Vector3 brickPosition)
    {
        GameObject effect = Instantiate(destroyParticle, brickPosition, Quaternion.identity);
        Destroy(effect, 2f);
    }
}
