using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class PopUpText : MonoBehaviour
{
    private TextMeshPro _numberText;
    private Camera _camera;
    

    private void Awake()
    {
        _numberText = GetComponent<TextMeshPro>();
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(_camera.transform.forward, _camera.transform.up);
    }

    public void Initialize(int number)
    {
        _numberText.text = number.ToString();

        transform.DOMoveY(30, 6);
        transform.DOScale(0.6f, 7).OnComplete(DestroyMySelf);
    }

    private void DestroyMySelf()
    {
        Destroy(gameObject);
    }
}
