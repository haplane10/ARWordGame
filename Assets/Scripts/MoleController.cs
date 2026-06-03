using System.Collections;
using TMPro;
using UnityEngine;

public class MoleController : MonoBehaviour
{
    // animation 이름 : Start, HitDown, Down, JumpDown
    public Animator animator;
    public MoleAnimation currentAnimation = MoleAnimation.None;

    public bool isCorrect = false;
    public bool isWrong = false;

    public TextMeshPro wordText;
    public WordSO easyWords;

    Coroutine moleCoroutine = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moleCoroutine = StartCoroutine(MoleState());
        wordText.text = easyWords.words[Random.Range(0, easyWords.words.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (isCorrect)
        {
            isCorrect = false;
            StopCoroutine(moleCoroutine);
            currentAnimation = MoleAnimation.HitDown;
            moleCoroutine = StartCoroutine(MoleState());
        }
        
        if (isWrong)
        {
            isWrong = false;
            StopCoroutine(moleCoroutine);
            currentAnimation = MoleAnimation.JumpDown;
            moleCoroutine = StartCoroutine(MoleState());
        }
    }

    public IEnumerator MoleState()
    {
        while (true)
        {
            switch (currentAnimation)
            {
                case MoleAnimation.None:
                    yield return new WaitForSeconds(3f);
                    currentAnimation = MoleAnimation.Start;
                    break;
                case MoleAnimation.Start:
                    PlayAnimation("Start");
                    yield return new WaitForSeconds(5f);
                    currentAnimation = MoleAnimation.Down;
                    break;
                case MoleAnimation.HitDown:
                    PlayAnimation("HitDown");
                    yield return new WaitForSeconds(1.5f);
                    currentAnimation = MoleAnimation.None;
                    break;
                case MoleAnimation.Down:
                    PlayAnimation("Down");
                    yield return new WaitForSeconds(1.5f);
                    currentAnimation = MoleAnimation.None;
                    break;
                case MoleAnimation.JumpDown:
                    PlayAnimation("JumpDown");
                    yield return new WaitForSeconds(1.5f);
                    currentAnimation = MoleAnimation.None;
                    break;
                default:
                    break;
            }
        }
    }

    public void PlayAnimation(string animName)
    {
        animator.SetTrigger(animName);
    }
}

public enum MoleAnimation
{
    None,
    Start,
    HitDown,
    Down,
    JumpDown
}
