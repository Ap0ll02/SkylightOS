using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CatGirlWizard : MonoBehaviour
{
    public AbstractSpell fireBall;
    public AbstractSpell iceBall;
    public AbstractSpell Gun;
    public InputActionReference move;

    public Rigidbody2D catGirlBody;
    public int Hearts = 3;
    public int Mana = 10;

    public Vector2 moveDirection;

    public void update()
    {
        moveDirection = move.action.ReadValue<Vector2>();
        MoveDirection();
    }

    public void MoveDirection()
    {
        catGirlBody.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    public void TakeDamage(int damage)
    {
        Hearts -= damage;
    }

    public void Heal(int heal)
    {
        Hearts += heal;
    }

    public void CastSpell()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
