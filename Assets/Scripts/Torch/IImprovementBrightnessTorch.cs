using System;

public interface IImprovementBrightnessTorch
{
    public event Action<float, float> BrightnessTorchUpdated;
}