using System.Collections;
using System.Collections.Generic;
using Damage;
using UnityEngine;
using UnityEngine.Networking;

public class Character : NetworkBehaviour, IKillable
{
    public AbstractWeapon weapon;
    public float acceleration = 1; //Ускорение персонажа
    public float velMax = 10;

    private GameManager gameManager;
    private Team team;
    private Rigidbody m_Rigidbody;
    private bool alive;

    public GameManager GameManager
    {
        set { gameManager = value; }
    }

    public Team Team
    {
        get { return team; }
        set { team = value; }
    }

    void Start (){
        m_Rigidbody = GetComponent<Rigidbody>();
        alive = true;
    }

    void Update ()
    {
        if (!alive || !isServer) return;

        if (transform.position.y < -10) GetComponent<Health>().TakeDamage(new DamageDealer(
            "Killer plane", 0, DamageType.INSTANT_KILL));
    }

    public void Move(Vector3 moveDirection)
    {
		if (isGrounded())
			m_Rigidbody.velocity += moveDirection.normalized * acceleration;

//          Не уверен насколько такое решение будет адекватным
//          Считаем вектор скорости для текущего объекта sqrt(x^2+z^2)
//          Сверяем полученное значение с максимумом
        if (Mathf.Sqrt ((m_Rigidbody.velocity.x * m_Rigidbody.velocity.x) + (m_Rigidbody.velocity.z * m_Rigidbody.velocity.z)) > velMax)
			m_Rigidbody.velocity = m_Rigidbody.velocity.normalized * velMax;
        //m_Rigidbody.AddForce(moveDirection.normalized * m_MovePower);
    }

	public void Shoot(Vector3 target){
        weapon.shoot(target);

	}

    public bool isGrounded()
    {
        // Если по y нет скорости - то значит он не падает и не взлетает - значит он на земле :)
        if (Mathf.Abs(m_Rigidbody.velocity.y) < 0.05f)
            return true;
        return false;
    }

    public void kill(DamageDealer dd)
    {
        if (!alive) return;
        alive = false;
        gameManager.onCharacterDeath(this, this);
    }

    #region respawn
    public void respawnAt(SpawnPoint sp)
    {
        respawnAt(sp.transform.position);
    }

    public void respawnAt(Vector3 position)
    {
        RpcRespawnAt(position);
    }

    [ClientRpc]
    void RpcRespawnAt(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        alive = true;
    }
    #endregion
}
