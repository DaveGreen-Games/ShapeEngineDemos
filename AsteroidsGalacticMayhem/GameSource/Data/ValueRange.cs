using System.Numerics;
using ShapeEngine.Core;
using ShapeEngine.Lib;
using ShapeEngine.Random;

namespace AsteroidsGalacticMayhem.GameSource.Data;

public readonly struct ValueRange
{
    public readonly float Min;
    public readonly float Max;

    public ValueRange()
    {
      Min = 0.0f;
      Max = 1f;
    }

    public ValueRange(Vector2 range)
    {
      float x = range.X;
      float y = range.Y;
      if (x > y)
      {
        Max = x;
        Min = y;
      }
      else
      {
        Min = x;
        Max = y;
      }
    }

    public ValueRange(float min, float max)
    {
      if (min > max)
      {
        Max = min;
        Min = max;
      }
      else
      {
        Min = min;
        Max = max;
      }
    }

    public ValueRange(float max)
    {
      if (max < 0.0)
      {
        Min = max;
        Max = 0.0f;
      }
      else
      {
        Min = 0.0f;
        Max = max;
      }
    }


    public Vector2 ToVector2() => new Vector2(this.Min, this.Max);

    public float Rand() => Rng.Instance.RandF(this.Min, this.Max);
    public float Rand(Rng rng) => rng.RandF(this.Min, this.Max);

    public float Lerp(float f) => ShapeMath.LerpFloat(this.Min, this.Max, f);

    public float Inverse(float value) => (float) ((value - Min) / (Max - Min));

    public float Remap(RangeFloat to, float value) => to.Lerp(Inverse(value));

    public float Remap(float newMin, float newMax, float value) => ShapeMath.LerpFloat(newMin, newMax, Inverse(value));

    public float Clamp(float value) => ShapeMath.Clamp(value, Min, Max);
    
}