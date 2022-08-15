using System;
using System.Collections;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    private int health;
    private bool invincible = false;
    public GameObject UIpoints;
    public SpriteRenderer sr;
    public int flashes;
    public float flashTime, knockTime;
    public Color flashColor, regularColor;
    public PlayerController playerC;
    private void Awake() {
        HealthReset();
    }
    public void HealthReset() {
        health = 10;
    }
    public void Damage(int dmg) {
        StartCoroutine(ProcessDamage(dmg));
    }
    private IEnumerator ProcessDamage(int diff) {
        if (invincible) {
            yield break;
        }
        
        for (int i = 1; i <= diff; i++) {
            UIpoints.transform.GetChild(health - i).gameObject.SetActive(false);
        }
        health -= diff;
        
        //if health hits 0, game over
        if (health <= 0) {
            health = 0;
            Time.timeScale = 0;
            Debug.Log("Game over");
        }
        
        //show damage and invincibility frames
        StartCoroutine(Knockback());
        invincible = true;
        for (int i = 0; i < flashes; i++) {
            sr.color = flashColor;
            yield return new WaitForSeconds(flashTime);
            sr.color = regularColor;
            yield return new WaitForSeconds(flashTime);
        }
        invincible = false;
    }
    private IEnumerator Knockback() {
        playerC.canWalk = false;
        yield return new WaitForSeconds(knockTime);
        playerC.canWalk = true;
    }

    public int GetHealth() {
        return health;
    }

}