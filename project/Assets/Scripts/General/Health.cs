using Damage;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public const int maxHealth = 100;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public void TakeDamage(DamageDealer dd)
    {
        if (!isServer) return;
        Debug.Log("Taking " + dd.Ammount + " " + dd.Type + " damage from " + dd.Source);

        if (dd.Type == DamageType.INSTANT_KILL)
        {
            killInstance(dd);
            return;
        }

        currentHealth -= (int)dd.Ammount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            killInstance(dd);
        }
    }

    public void OnChangeHealth(int health)
    {

    }

    public void healFullHealth()
    {
        currentHealth = maxHealth;
    }

    private void killInstance(DamageDealer dd)
    {
        Debug.Log("Kill instance");
        GetComponent<IKillable>().kill(dd);
    }
}
