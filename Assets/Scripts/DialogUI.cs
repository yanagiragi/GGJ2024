using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    [SerializeField] public Image _selfImage;
    [SerializeField] public Image _image;
    [SerializeField] public Sprite q;
    [SerializeField] public Sprite w;
    [SerializeField] public Sprite e;
    [SerializeField] public Sprite a;
    [SerializeField] public Sprite s;
    [SerializeField] public Sprite d;
    [SerializeField] public Sprite z;
    [SerializeField] public Sprite x;
    [SerializeField] public Sprite c;
    [SerializeField] public Sprite i;
    [SerializeField] public Sprite o;
    [SerializeField] public Sprite p;
    [SerializeField] public Sprite j;
    [SerializeField] public Sprite k;
    [SerializeField] public Sprite l;
    [SerializeField] public Sprite n;
    [SerializeField] public Sprite m;

    public void Start()
    {
        _selfImage.enabled = false;
        _image.enabled = false;
    }

    public void Show(string code)
    {
        StartCoroutine(_display(code));
    }

    private IEnumerator _display(string code)
    {
        var sprite = code switch
        {
            "q" => q,
            "w" => w,
            "e" => e,
            "a" => a,
            "s" => s,
            "d" => d,
            "z" => z,
            "x" => x,
            "c" => c,
            "i" => i,
            "o" => o,
            "p" => p,
            "j" => j,
            "k" => k,
            "l" => l,
            "n" => n,
            "m" => m,
            _ => null
        };

        _image.sprite = sprite;
        _selfImage.enabled = true;
        _image.enabled = true;

        yield return new WaitForSeconds(0.5f);

        _selfImage.enabled = false;
        _image.enabled = false;
    }
}
