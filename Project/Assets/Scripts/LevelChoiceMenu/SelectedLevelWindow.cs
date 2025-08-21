using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelChoiceScene
{
    public class SelectedLevelWindow : MonoBehaviour
    {
        [SerializeField] private GameObject singleEnemyInfoPrefab;
        [SerializeField] private GameObject singleTierInfoPrefab;

        [SerializeField] private GameObject columnEnemiesInfo;
        [SerializeField] private GameObject columnTiersInfo;

        private List<SingleEnemyInfo> singleEnemiesInfosPooler = new List<SingleEnemyInfo>();
        private List<SingleTierInfo> singleTiersInfosPooler = new List<SingleTierInfo>();

        public void InitialzieWindow(EnemyData[] levelEnemiesPreset, CountOfEnemiesOfTier[] countOfEnemiesOfTierPreset)
        {
            InitializeEnemiesInfos(levelEnemiesPreset, countOfEnemiesOfTierPreset);
            InitialzieTiersInfo(countOfEnemiesOfTierPreset);
        }

        private void InitializeEnemiesInfos(EnemyData[] levelEnemiesPreset, CountOfEnemiesOfTier[] countOfEnemiesOfTierPreset)
        {
            for(int i = 0; i < levelEnemiesPreset.Length; i++)
            {
                if (i >= singleEnemiesInfosPooler.Count)
                {
                    singleEnemiesInfosPooler.Add(Instantiate(singleEnemyInfoPrefab, columnEnemiesInfo.transform).GetComponent<SingleEnemyInfo>());
                }
                else singleEnemiesInfosPooler[i].gameObject.SetActive(true);

                singleEnemiesInfosPooler[i].InitializeSingleEnemy(levelEnemiesPreset[i]);
            }
        }

        private void InitialzieTiersInfo(CountOfEnemiesOfTier[] countOfEnemiesOfTierPreset)
        {
            for(int i = 0; i < countOfEnemiesOfTierPreset.Length; i++)
            {
                if (i >= singleTiersInfosPooler.Count)
                {
                    singleTiersInfosPooler.Add(Instantiate(singleTierInfoPrefab, columnTiersInfo.transform).GetComponent<SingleTierInfo>());
                }
                else singleTiersInfosPooler[i].gameObject.SetActive(true);

                singleTiersInfosPooler[i].Initialize(countOfEnemiesOfTierPreset[i].Tier.ToString() + ": " + countOfEnemiesOfTierPreset[i].Count.ToString());
            }
        }

        public void HideThyself()
        {
            foreach(SingleEnemyInfo element in singleEnemiesInfosPooler)
            {
                element.gameObject.SetActive(false);
            }

            foreach(SingleTierInfo element in singleTiersInfosPooler)
            {
                element.gameObject.SetActive(false);
            }

            this.gameObject.SetActive(false);
        }
    }
}
