using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(PlayerMove))]
public class PlayerController : CharacterAnimator
{
    public LayerMask movementMask;
    Camera cam;
    PlayerMove mover;
    CharacterCombat characterCombat;
    public Interactable focus;

    void Start()
    {
        cam = Camera.main;
        mover = GetComponent<PlayerMove>();
        characterCombat = GetComponent<CharacterCombat>();
        characterCombat.OnAttack+= OnAttackCallback;
    }
    
    IEnumerator AttackAnimation(float delay)
    {
        StartAttacking();
        yield return new WaitForSeconds(delay);
        StopAttacking();
    }
    

    void OnAttackCallback(){
        StartCoroutine(AttackAnimation(0.5f));
    }

    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable intr = hit.collider.GetComponent<Interactable>();
                if (intr != null)
                {
                    SetFocus(intr);
                }
                else
                {
                    mover.MoveToPoint(hit.point);
                    RemoveFocus();
                }
            }
        }

        /*if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Interactable intr = hit.collider.GetComponent<Interactable>();
                if (intr != null)
                {
                    SetFocus(intr);
                }
            }
        }*/
    }

    void SetFocus(Interactable newFocus)
    {

        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;
            mover.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.OnDefocused();
        }
        focus = null;
        mover.StopFollowingTarget();
    }
}
