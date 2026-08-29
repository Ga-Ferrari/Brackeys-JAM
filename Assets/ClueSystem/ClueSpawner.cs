using UnityEngine;

public class ClueSpawner : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private GameObject prefabPapel;

    private Transform[] locaisSpawn;
    private GameObject pistaAtual;
    private void Awake()
    {
        SpawnPoints[] pontos = FindObjectsByType<SpawnPoints>();

        locaisSpawn = new Transform[pontos.Length];

        for(int i = 0; i < pontos.Length; i++)
        {
            Debug.Log($"Obtidos {i+1} locais de spawn");
            locaisSpawn[i] = pontos[i].transform;
        }
    }

    private void Start()
    {
        turnManager.daylightTurn += SpawnNewClue;
    }

    private void OnDestroy()
    {
        if(turnManager != null) turnManager.daylightTurn -= SpawnNewClue;
    }

    private void SpawnNewClue()
    {
        if(locaisSpawn.Length == 0 || prefabPapel == null) {
            Debug.Log("Nao eh possivel spawnar pista");
            return;
        }

        if(pistaAtual != null) {
            Debug.Log("Deletando pista ja existente...");
            Destroy(pistaAtual);
        }

        int sort = UnityEngine.Random.Range(0, locaisSpawn.Length);
        Transform localEscolhido = locaisSpawn[sort];

        pistaAtual = Instantiate(prefabPapel, localEscolhido.position, localEscolhido.rotation);
    }
}