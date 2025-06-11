using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransicaoGenerica : TransicaoBase {
    public string nomeCenaGenerica;

    void Update() {
        if (!podeInteragir || cenaJaCarregando) return;

        if (Input.GetKeyDown(KeyCode.Return)) {
            Trocar();
        }
    }

    protected override void Trocar() {
        TrocarCena(nomeCenaGenerica);
    }

}