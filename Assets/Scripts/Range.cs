public struct Range
{
    public float min;
    public float max;
    public float range { get { return max - min + 1; } }
    public Range(float aMin, float aMax)
    {
        min = aMin; max = aMax;
    }
}