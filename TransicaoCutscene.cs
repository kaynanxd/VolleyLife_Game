using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransicaoCutscene : TransicaoBase
{
    public float tempoCutscene = 3f;
    public string nomeCenaCutscene;

    void Start()
    {
        if (string.IsNullOrEmpty(mensagemTexto))
        {
            Invoke(nameof(Trocar), tempoCutscene);
        }
    }

    void Update()
    {
        if (!podeInteragir || cenaJaCarregando) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Invoke(nameof(Trocar), tempoCutscene);
        }
    }

    protected override void Trocar() {
        TrocarCena(nomeCenaCutscene);
    }

}