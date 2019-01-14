using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject healthBar;

    Transform ui;
    Image healthSlider;
    Transform cam;


    // Use this for initialization
    void Start()
    {
        /*cam = Camera.main.transform;

        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefarb, c.transform).transform;
                healthSlider = ui.GetChild(1).GetComponent<Image>();
                break;
            }
        }
		*/
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnMouseEnter()
    {
        //Debug.Log("OnMouseEnter");
        var healthBar=CanvasManager.Instance.enemyHealthBar;
        healthBar.SetActive(true);


        if (healthBar != null)
        {   
            var stats = gameObject.GetComponent<EnemyStats>();
            
            float percents = ((float)stats.currentHealth / (float)stats.maxHealth.GetValue());

            healthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = percents;
            healthBar.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0} (Level {1})", stats.CharacterLabel, stats.level.GetValue());
        }
    }

    private void OnMouseExit()
    {
        CanvasManager.Instance.enemyHealthBar.SetActive(false);
    }

    void OnHealthChanged(float maxHealth, float currentHealth)
    {
        
        var healthBar=CanvasManager.Instance.enemyHealthBar;
        
        if(currentHealth<=0){
        		healthBar.SetActive(false);
			}

        if (healthBar != null)
        {
            var stats = gameObject.GetComponent<EnemyStats>();
            float percents = ((float)stats.currentHealth / (float)stats.maxHealth.GetValue());

            healthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = percents;
            healthBar.transform.GetChild(1).GetComponent<Text>().text = string.Format("{0} (Level {1})", stats.CharacterLabel, stats.level.GetValue());
        }
    }
}
