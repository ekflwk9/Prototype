using DG.Tweening;
using UnityEngine;

using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeManager : MonoBehaviour
{
    public static GlobalVolumeManager Instance { get; private set; }
    [Header("글로벌 볼륨"), SerializeField] private Volume globalVolume;

    private Vignette vignette;
    private FilmGrain filmGrain;
    private ChromaticAberration chromaticAberration;

#if UNITY_EDITOR
    private void Reset()
    {
        globalVolume = this.TryGetComponent<Volume>();
    }
#endif

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Init();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private T TryGet<T>() where T : VolumeComponent
    {
        if (globalVolume.profile.TryGet<T>(out var _component))
        {
            LogHelper.Log($"GlobalVolumeManager에 {_component.name}이란 볼륨은 추가되지 않음");
            return null;
        }

        else
        {
            return _component;
        }
    }

    private void Init()
    {
        filmGrain = TryGet<FilmGrain>();
        filmGrain.active = true;
        filmGrain.intensity.overrideState = true;

        chromaticAberration = TryGet<ChromaticAberration>();
        chromaticAberration.active = true;
        chromaticAberration.intensity.overrideState = true;

        vignette = TryGet<Vignette>();
        vignette.active = true;
        vignette.intensity.overrideState = true;
    }

    /// <summary>
    /// Global Volume의 FilmGrain - intensity를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="duration">fade하는데 걸리는 시간</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetFilmGrain(float intensity, float duration = 1f, bool fade = true)
    {
        if (fade) DOTween.To(GetFilmGrainIntensity, SetFilmGrainIntensity, intensity, duration);
        else filmGrain.intensity.value = intensity;
    }

    private float GetFilmGrainIntensity() => filmGrain.intensity.value;
    private void SetFilmGrainIntensity(float x) => filmGrain.intensity.value = x;

    /// <summary>
    /// Global Volume의 ChromaticAberration - intensity를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="duration">fade하는데 걸리는 시간</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetChromaticIntensity(float intensity, float duration = 1f, bool fade = true)
    {
        if (fade) DOTween.To(GetChromaticValue, SetChromaticValue, intensity, duration);
        else chromaticAberration.intensity.value = intensity;
    }

    private float GetChromaticValue() => chromaticAberration.intensity.value;
    private void SetChromaticValue(float _value) => chromaticAberration.intensity.value = _value;

    /// <summary>
    /// Global Volume의 Vignette - color를 변경
    /// </summary>
    /// <param name="color">변화시킬 color의 목표치</param>
    /// <param name="duration">fade하는데 걸리는 시간</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetVignetteColor(Color color, float duration = 1f, bool fade = true)
    {
        if (fade) DOTween.To(GetVignetteColor, SetVignetteColor, color, duration);
        else vignette.color.value = color;
    }

    private Color GetVignetteColor() => vignette.color.value;
    private void SetVignetteColor(Color _color) => vignette.color.value = _color;

    /// <summary>
    /// Global Volume의 Vignette - Color를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetVignette(float intensity, float duration = 1f, bool fade = true)
    {
        if (fade)  DOTween.To(GetVignetteValue, SetVignetteValue, intensity, 1.0f);
        else vignette.intensity.value = intensity;
    }

    private float GetVignetteValue() => vignette.intensity.value;
    private void SetVignetteValue(float x) => vignette.intensity.value = x;
}