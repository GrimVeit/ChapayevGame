using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreStrategyModel
{
    public event Action<Strategy> OnOpenStrategy;
    public event Action<Strategy> OnCloseStrategy;

    public event Action<Strategy> OnDeselectStrategy;
    public event Action<Strategy> OnSelectStrategy;


    private StrategyGroup strategyGroup;

    private List<StrategyData> chipDatas = new List<StrategyData>();

    public readonly string FilePath = Path.Combine(Application.persistentDataPath, "Strategy.json");

    public StoreStrategyModel(StrategyGroup strategyGroup)
    {
        this.strategyGroup = strategyGroup;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            StrategyDatas gameTypeDatas = JsonUtility.FromJson<StrategyDatas>(loadedJson);

            Debug.Log("Load data");

            this.chipDatas = gameTypeDatas.Datas.ToList();
        }
        else
        {
            Debug.Log("New Data");

            chipDatas = new List<StrategyData>();

            for (int i = 0; i < strategyGroup.Strategies.Count; i++)
            {
                if (i == 0)
                {
                    chipDatas.Add(new StrategyData(true, true));
                }
                else
                {
                    chipDatas.Add(new StrategyData(false, false));
                }
            }
        }

        for (int i = 0; i < strategyGroup.Strategies.Count; i++)
        {
            strategyGroup.Strategies[i].SetData(chipDatas[i]);

            if (strategyGroup.Strategies[i].StrategyData.IsOpen)
                OnOpenStrategy?.Invoke(strategyGroup.Strategies[i]);
            else
                OnCloseStrategy?.Invoke(strategyGroup.Strategies[i]);
        }
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new StrategyDatas(chipDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void SelectChip(int number)
    {
        var chip = strategyGroup.GetStrategyById(number);

        if (chip == null)
        {
            Debug.LogError($"Not found strategy by id - {number}");
            return;
        }

        if (chip.StrategyData.IsSelect)
        {
            chip.StrategyData.IsSelect = false;
            OnDeselectStrategy?.Invoke(chip);
        }
        else
        {
            chip.StrategyData.IsSelect = true;
            OnSelectStrategy?.Invoke(chip);
        }
    }

    public void OpenStrategy(int number)
    {
        var chip = strategyGroup.GetStrategyById(number);

        if (chip == null)
        {
            Debug.LogError($"Not found strategy by id - {number}");
            return;
        }

        if (chip.StrategyData.IsOpen)
        {
            Debug.LogWarning($"Strategy by id - {number} is already open");
        }
        else
        {
            chip.StrategyData.IsOpen = true;
            OnOpenStrategy?.Invoke(chip);
        }
    }

    public bool IsAvailableStrategy()
    {
        return strategyGroup.Strategies.FirstOrDefault(data => data.StrategyData.IsOpen == false) != null;
    }

    public Strategy GetRandomCloseStrategy()
    {
        return strategyGroup.Strategies.FirstOrDefault(data => data.StrategyData.IsOpen == false);
    }
}

[Serializable]
public class StrategyDatas
{
    public StrategyData[] Datas;

    public StrategyDatas(StrategyData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class StrategyData
{
    public bool IsOpen;
    public bool IsSelect;

    public StrategyData(bool isSelect, bool isOpen)
    {
        this.IsSelect = isSelect;
        this.IsOpen = isOpen;
    }
}
