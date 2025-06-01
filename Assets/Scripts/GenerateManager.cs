using System.Collections.Generic;
using UnityEngine;

public class GenerateManager : MonoBehaviour
{
    [System.Serializable]
    public class GenerateEntry
    {
        [SerializeField, Header("生成するキャラクター")]
        private GameObject characterPrefab;
        [SerializeField, Header("生成される時間(秒)")]
        private float generateTime;

        [HideInInspector]
        public bool generated = false;
        public GameObject Prefab => characterPrefab;
        public float GenerateTime => generateTime;
    }
    [SerializeField, Header("キャラクターリスト")]
    private List<GenerateEntry> _generateList = new List<GenerateEntry>();

    private float _timer = 0f;
    void Update()
    {
        _timer += Time.deltaTime;
        for (int i = 0; i < _generateList.Count; i++)
        {
            if (!_generateList[i].generated && _timer >= _generateList[i].GenerateTime)
            {
                Instantiate(_generateList[i].Prefab, transform.position, Quaternion.identity);
                _generateList[i].generated = true;
            }
        }
    }
}
