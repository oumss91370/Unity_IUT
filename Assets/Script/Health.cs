using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    public HealthBar healthBar;

    public int MaxHealth = 100;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    private SpriteRenderer spriteRend;
    private Vector3 respawnPoint;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

        if (anim == null)
        {
            Debug.LogError("Animator component is missing from this game object");
        }

        if (spriteRend == null)
        {
            Debug.LogError("SpriteRenderer component is missing from this game object");
        }

        respawnPoint = transform.position; // Initialiser le point de réapparition à la position initiale du joueur
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (healthBar != null)
        {
            healthBar.SetHealth((int)currentHealth);
        }

        if (currentHealth > 0)
        {
            if (anim != null)
            {
                anim.SetTrigger("hurt");
            }

            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                if (anim != null)
                {
                    anim.SetTrigger("die");
                    anim.SetBool("isDead", true); // Indiquer que le joueur est mort
                }

                // Désactiver le mouvement du joueur et les comportements des ennemis
                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeleEnemy>() != null)
                    GetComponent<MeleEnemy>().enabled = false;

                if (GetComponent<RangeEnemy>() != null)
                    GetComponent<RangeEnemy>().enabled = false;

                dead = true;

                // Appel de la méthode de réapparition après un délai (à ajuster en fonction de la durée de l'animation de mort)
                Invoke("Respawn", 2.0f);
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
        if (healthBar != null)
        {
            healthBar.SetHealth((int)currentHealth);
        }
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }

        Physics2D.IgnoreLayerCollision(10, 11, false);
    }

    private void Desactivate()
    {
        gameObject.SetActive(false);
    }

    private void Respawn()
    {
        transform.position = respawnPoint; // Réinitialiser la position du joueur
        currentHealth = startingHealth; // Réinitialiser la santé du joueur
        if (healthBar != null)
        {
            healthBar.SetHealth((int)currentHealth);
        }

        if (anim != null)
        {
            anim.SetTrigger("respawn"); // Déclencher une animation de réapparition si vous en avez une
            anim.SetBool("isDead", false); // Indiquer que le joueur n'est plus mort
        }

        // Réinitialiser la couleur du sprite
        spriteRend.color = Color.white;

        // Réactiver les composants nécessaires
        if (GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = true;

        dead = false;
    }
}
