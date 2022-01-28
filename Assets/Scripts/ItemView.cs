using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private Image _image;

    public Text Text => _text;
    public Image Image => _image;
}