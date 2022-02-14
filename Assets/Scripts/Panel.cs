using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField, Range(0, 1)] protected float speed = 0.2f;
    [SerializeField] protected Transform startTransform;
    [SerializeField] protected Transform finishTransform;
    [SerializeField] protected GameObject ballPrefab;

    protected RectTransform rectTransform;
    protected List<GameObject> activeBall;

    public void OnCloseButtonClick() 
    {
        ClearActiveBall();
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        activeBall = new List<GameObject>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartPanel());
    }

    private IEnumerator StartPanel() 
    {
        #region scale Panel
        float i = 0f;
        Vector3 localScale;
        while (i <= 1) 
        {
            localScale = Vector3.Lerp(new Vector3(0.4f, 0.4f), new Vector3(1, 1), i);
            rectTransform.localScale = localScale;
            i += speed;
            yield return null;
        }
        #endregion

        #region Ball Move
        while (true) 
        {
            StartCoroutine(StartMoveBall());
            yield return new WaitForSeconds(0.5f);
        }
        #endregion
    }

    private IEnumerator StartMoveBall() 
    {
        var ball = Instantiate(ballPrefab, startTransform.position, Quaternion.identity, transform);
        activeBall.Add(ball);
        float i = 0f;
        while (i<=1)
        {
            ball.transform.position = Vector3.Lerp(startTransform.position, finishTransform.position, i);
            i += speed / 20f;
            yield return null;
        }
        activeBall.Remove(ball);
        Destroy(ball);
    }
    private void ClearActiveBall() 
    {
        foreach (var item in activeBall)
        {
            Destroy(item);
        }
        activeBall = new List<GameObject>();
    }
}
