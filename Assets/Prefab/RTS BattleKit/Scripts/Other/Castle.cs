using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour {
	
	//variables visible in the inspector
	public float lives;
    public float startLives;
	public float size;

    public float regeneration = 0f;

    public Image healthBar;

    private void Start()
    {
        StartCoroutine(endGameAlternatif());
    }
    
    public IEnumerator endGameAlternatif()
    {
        yield return new WaitForSeconds(180f);
        Die();
    }

    void Update()
    {
        healthBar.fillAmount = lives / startLives;

        if (lives <= 0f)
        {
            lives = 0;
            Destroy(gameObject);
            Die();
        }

        if (lives < startLives)
        {
            lives += regeneration + Time.deltaTime;

            if (lives > 200f)
            {
                lives = 200f;
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
