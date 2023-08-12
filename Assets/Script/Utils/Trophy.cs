using UnityEngine;

[System.Serializable]
public class Trophy
{
    public GameObject trophyItem;
    public int minItemGenerateAmount = 0;
    public int maxItemGenerateAmount = 0;
    public float generateRate = 1f;

    public Trophy(GameObject trophyItem, int minItemGenerateAmount, int maxItemGenerateAmount, float generateRate)
    {
        this.trophyItem = trophyItem;
        this.minItemGenerateAmount = minItemGenerateAmount;
        this.maxItemGenerateAmount = maxItemGenerateAmount;
        this.generateRate = generateRate;
    }

    // 返回要生成物品的数量
    public int GetGenerateAmount()
    {
        // 判断数据合法性
        if (trophyItem == null || maxItemGenerateAmount <= 0 || generateRate <= 0) return 0;

        // 随机数决定是否生成
        if (Random.Range(0f, 1f) > generateRate) return 0;

        // 随机数决定生成多少
        return Random.Range(minItemGenerateAmount, maxItemGenerateAmount + 1);
    }
}