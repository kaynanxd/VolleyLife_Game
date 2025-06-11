using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class TransicaoBase : MonoBehaviour
{
    public string mensagemTexto = "Pressione [Enter] para interagir";
    public Text textoUI;
    public GameObject telaCarregamento;
    protected bool podeInteragir = false;
    protected bool cenaJaCarregando = false;

    protected void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        podeInteragir = true;
        if (textoUI != null)
        {
            textoUI.text = mensagemTexto;
            textoUI.gameObject.SetActive(true);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        podeInteragir = false;
        if (textoUI != null) textoUI.gameObject.SetActive(false);
    }

    protected void TrocarCena(string nomeCena)
    {
        if (cenaJaCarregando) return;
        cenaJaCarregando = true;

        if (telaCarregamento != null)
        {
            telaCarregamento.SetActive(true);
        }

        SceneManager.LoadScene(nomeCena);
    }
    
    protected abstract void Trocar();
}
