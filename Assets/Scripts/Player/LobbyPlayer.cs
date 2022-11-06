using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class LobbyPlayer : MonoBehaviour
{
    [SerializeField, Range(0f, 5f)] private float flySpeed = 1f;
    [SerializeField] public GameObject fireEffect;

    private Vector2 inputValue;

    private StageButton interactStageBtn = null;

    private Rigidbody2D rigid;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (inputValue != Vector2.zero)
        {
            fireEffect.SetActive(true);
            rigid.velocity = new Vector2(inputValue.x * flySpeed, inputValue.y * flySpeed);

            if (inputValue.x > 0)
                sprite.flipX = true;
            else
                sprite.flipX = false;
        }
        else
        {
            fireEffect.SetActive(false);
            rigid.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// LobbyPlayerInputÀÇ Move ±¸Çö
    /// </summary>
    private void OnMove(InputValue inputValue)
    {
        this.inputValue = inputValue.Get<Vector2>();
    }

    private void OnInteract()
    {
        if (interactStageBtn && interactStageBtn.IsInteractable)
        {
            interactStageBtn.Interact();
            StartCoroutine(GameStartCo());
        }
    }

    private IEnumerator GameStartCo()
    {
        yield return new WaitForSeconds(1f);

        FadePanel.Instance.FadeIn();

        while(true)
        {
            if (FadePanel.Instance.IsCompleteFade)
                break;

            yield return new WaitForEndOfFrame();
        }

        GameManager.Instance.GoToStage(interactStageBtn.StageNum);

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out StageButton stageBtn))
        {
            if(interactStageBtn == null)
                interactStageBtn = stageBtn;

            stageBtn.SetActiveLight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out StageButton stageBtn))
        {
            if(stageBtn == interactStageBtn)
                interactStageBtn = null;

            stageBtn.SetActiveLight(false);
        }
    }
}