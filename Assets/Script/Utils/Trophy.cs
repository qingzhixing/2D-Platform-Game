using UnityEngine;

[System.Serializable]
public class Trophy
{
    public GameObject trophyItem;
    public Range<int> itemAmontRange;
    public float generateRate = 1f;

    public Trophy(GameObject trophyItem, Range<int> itemAmontRange, float generateRate)
    {
        this.trophyItem = trophyItem;
        this.itemAmontRange = itemAmontRange;
        this.generateRate = generateRate;
    }

    // 返回要生成物品的数量
    public int GetGenerateAmount()
    {
        // 判断数据合法性
        if (trophyItem == null || itemAmontRange.max <= 0 || generateRate <= 0) return 0;

        // 随机数决定是否生成
        if (Random.Range(0f, 1f) > generateRate) return 0;

        // 随机数决定生成多少
        return Random.Range(itemAmontRange.min, itemAmontRange.max + 1);
    }
}