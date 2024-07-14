using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onetimeplatform : MonoBehaviour
{
    public float destroytime = 0.5f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Optional: Add a delay before destroying the platform
            StartCoroutine(DestroyPlatform());
        }
    }

    private IEnumerator DestroyPlatform()
    {
        // Optional: Add a delay (e.g., 0.5 seconds) before the platform gets destroyed
        yield return new WaitForSeconds(destroytime);

        // Destroy the platform
        Destroy(gameObject);
    }
}
