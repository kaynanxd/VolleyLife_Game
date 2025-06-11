using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransicaoTeletransporte : TransicaoBase {
    public GameObject painelTeletransporte;
    public Button[] botoesCenarios;

    void Update() {
        if (!podeInteragir || cenaJaCarregando) return;

        if (Input.GetKeyDown(KeyCode.Return)) {
            painelTeletransporte.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            painelTeletransporte.SetActive(false);
        }
    }

    protected void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);

        for (int i = 0; i < botoesCenarios.Length; i++) {
            Button botao = botoesCenarios[i]; 

            string destino = botao.name;
            botao.onClick.RemoveAllListeners();
            botao.onClick.AddListener(() => TrocarCena(destino));
        }
    }

    protected void OnTriggerExit(Collider other) {
        base.OnTriggerExit(other);

        painelTeletransporte.SetActive(false);
    }

    protected override void Trocar() {
        painelTeletransporte.SetActive(true);
    }

}