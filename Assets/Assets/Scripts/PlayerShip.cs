using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerShip : MonoBehaviour, IDamagable
{
    [SerializeField] private PathFollower pathFollower;
    [SerializeField] private IntEvent scoreEvent;
    [SerializeField] private Inventory inventory;
    [SerializeField] private IntVariable score;
    [SerializeField] private floatVariable health;

    [SerializeField] protected GameObject hitPrefab;
    [SerializeField] protected GameObject destroyPrefab;

    private void Start()
    {
        scoreEvent.Subscribe(AddPoints);
        health.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            inventory.Use();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            inventory.OnStopUse();
        }

        pathFollower.speed = (Input.GetKey(KeyCode.LeftShift)) ? 5 : 1;
    }

    public void AddPoints(int points)
    {
        score.value += points;
        Debug.Log("Score: " + score.value);
    }

    public void ApplyDamage(float damage)
    {
        health.value -= damage;
        if (health.value <= 0)
        {
            if (destroyPrefab != null)
            {
                Instantiate(destroyPrefab, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
        else
        {
            if (hitPrefab != null)
            {
                Instantiate(hitPrefab, gameObject.transform.position, Quaternion.identity);
            }
        }
    }

    public void ApplyHealth (float health)
    {
        this.health.value += health;
        this.health.value = Mathf.Min(this.health.value, 100);
    }
}
