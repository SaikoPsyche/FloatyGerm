using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private float fadeAmount = 0.85f;
    [SerializeField] private bool isDying = false;

    private Transform childTransform;
    private Material dissolveMaterial;

    private void Start()
    {
        childTransform = transform.GetChild(0);
        dissolveMaterial = GetComponentInChildren<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (childTransform.position.y < -6)
        {
            isDying = true;
        }

        PlayerDeath();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Obstacle"))
        {
            isDying = true;
        }
    }

    private void PlayerDeath()
    {
        if (isDying == true)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        // yield return new WaitForSeconds(0.05f);

        SoundFX.instance.deathSound.Play();
        fadeAmount -= Time.deltaTime;
        dissolveMaterial.SetFloat("_Fade", fadeAmount);

        yield return new WaitForSeconds(1.05f);

        isDying = false;
        EventManager.onPlayerDeath();
        gameObject.SetActive(false);
    }
}
