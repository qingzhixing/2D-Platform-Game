using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrophyController
{
    public List<Trophy> trophies;

    public TrophyController(List<Trophy> trophies)
    {
        if (trophies == null)
        {
            Debug.LogWarning("Constructing a null List<Trophy> Object");
            this.trophies = new List<Trophy>();
        }
        else
        {
            this.trophies = new List<Trophy>(trophies);
        }
    }

    // 根据generatorObject的属性生成战利品
    public void GenerateTrophies(GameObject generatorObject)
    {
        trophies.ForEach(trophy =>
        {
            for (int generateAmount = trophy.GetGenerateAmount(); generateAmount > 0; generateAmount--)
            {
                GameObject generatedObject = Object.Instantiate(trophy.trophyItem, generatorObject.transform.position, Quaternion.identity);

                // 设置随机速度
                Utilities.SetRandomSpeed2D(generatedObject, new Vector2(-5, -5), new Vector2(5, 5));
            }
        });
    }
}