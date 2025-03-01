using Naninovel;
using UnityEngine;
using UnityEngine.UI;

public class ShowMap : MonoBehaviour
{
    public Button mapButton;
    public string loadMap;

    private void Start()
    {
        mapButton.onClick.AddListener(OnButtonClick);
    }

    async public void OnButtonClick()
    {
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        await scriptPlayer.PreloadAndPlayAsync(loadMap);
    }
}
