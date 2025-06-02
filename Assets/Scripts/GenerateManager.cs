using System.Collections.Generic;
using UnityEngine;

public class GenerateManager : MonoBehaviour
{
    [System.Serializable]
    public class GenerateEntry
    {
        [SerializeField, Header("生成するキャラクター")]
        private GameObject characterPrefab;
        [SerializeField, Header("初回生成までの時間(秒)")]
        private float generateStartDelay = 0f;
        [SerializeField, Header("生成される時間(秒)")]
        private float generateTime;
        private float nextGenerateTime = 0f;

        [HideInInspector]
        public bool generated = false;
        public GameObject Prefab => characterPrefab;
        public float GenerateStartDelay => generateStartDelay;
        public float GenerateTime => generateTime;
        public float NextGenerateTime
        {
            get => nextGenerateTime;
            set => nextGenerateTime = value;
        }
    }
    [SerializeField, Header("キャラクターリスト")]
    private List<GenerateEntry> _generateList = new List<GenerateEntry>();

    private float _timer = 0f;

    void Start()
    {
        _timer = 0f;

        for (int i = 0; i < _generateList.Count; i++)
        {
            _generateList[i].NextGenerateTime = _generateList[i].GenerateStartDelay;
        }
    }
    void Update()
    {
        _timer += Time.deltaTime;
        for (int i = 0; i < _generateList.Count; i++)
        {
            if (_timer >= _generateList[i].NextGenerateTime)
            {
                Instantiate(_generateList[i].Prefab, transform.position, Quaternion.identity);
                _generateList[i].NextGenerateTime += _generateList[i].GenerateTime;
            }
        }

    }
}
