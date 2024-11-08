using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	public int maxHealth;
	public int currentHealth;
	public float maxStam = 100;
	public float currentStam;

	public HealthBar healthBar;

	public Image StamFill;
	public bool running = false;

	public float RunCost;
	public float StamChargeRate;
    public int HealthChargeRate;

    private Coroutine stamRecharge;
	private Coroutine healthRecharge;

     //Start is called before the first frame update
    void Start()
    {
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

     //Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(10);

            if (healthRecharge != null) StopCoroutine(healthRecharge);
            healthRecharge = StartCoroutine(RechargeHealth());
        }

		if (Input.GetKeyDown("left shift"))
		{
			running = true;
		}
		else if (Input.GetKeyUp("left shift"))
		{ 
			running = false; 
		}

		if (running) //modify this to require an active direction when implemented to main code
		{
			currentStam -= RunCost * Time.deltaTime;
			if(currentStam < 0) currentStam = 0;
			StamFill.fillAmount = currentStam / maxStam;

			if(stamRecharge != null) StopCoroutine(stamRecharge);
			stamRecharge = StartCoroutine(RechargeStamina());

        }
    }

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);

		if (currentHealth == 0)
		{
            Debug.Log("You Died.");
        }
    }

	private IEnumerator RechargeStamina()
	{
		yield return new WaitForSeconds(2f);

		while (currentStam < maxStam)
		{
			currentStam += StamChargeRate / 10f;
			if(currentStam > maxStam) currentStam = maxStam;
			StamFill.fillAmount = currentStam / maxStam;
			yield return new WaitForSeconds(.1f);
		}
	}

    private IEnumerator RechargeHealth()
    {
        yield return new WaitForSeconds(4f);

        while (currentHealth < 30 && currentHealth > 0)
        {
            currentHealth += HealthChargeRate;
			healthBar.SetHealth(currentHealth);
            if (currentHealth > maxHealth) currentHealth = maxHealth;
			yield return new WaitForSeconds(.1f);
        }

        while (currentHealth < 60 && currentHealth > 30)
        {
            currentHealth += HealthChargeRate;
            healthBar.SetHealth(currentHealth);
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            yield return new WaitForSeconds(.1f);
        }

        while (currentHealth < 90 && currentHealth > 60)
        {
            currentHealth += HealthChargeRate;
            healthBar.SetHealth(currentHealth);
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            yield return new WaitForSeconds(.1f);
        }
    }
}
