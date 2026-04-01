using System;

public interface IWaveProvider
{
  int CurrentIndexWave { get; }
  event Action<int> OnWaveIndexChanged;
}