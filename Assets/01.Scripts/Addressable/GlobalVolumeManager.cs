using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using UnityEngine.Rendering;

public class GlobalVolumeManager : MonoBehaviour
{
    private static GlobalVolumeManager instance;

    public static GlobalVolumeManager Instance
    {
        // 외부에서 사용할 때 Awake()가 아닌 Start()에서 사용해야 함
        get { return instance; }
    }

    [Header("Global Volume")] [SerializeField]
    private Volume globalVolume;

    private FilmGrain filmGrain;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;

    private void Reset()
    {
        globalVolume = GetComponent<Volume>();
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitGlobalVolume();
        }
    }

    private void InitGlobalVolume() // 글로벌 볼륨 초기화
    {
        if (globalVolume == null)
        {
            LogHelper.LogError("GlobalVolumeManager에 Global Volume이 할당되지 않았습니다.", this);
            return;
        }

        if (!globalVolume.profile.TryGet<FilmGrain>(out filmGrain))
        {
            LogHelper.LogError("Global Volume에 FilmGrain이 없습니다.", this);
        }
        else
        {
            filmGrain.active = true;
            filmGrain.intensity.overrideState = true;
        }

        if (!globalVolume.profile.TryGet<ChromaticAberration>(out chromaticAberration))
        {
            LogHelper.LogError("Global Volume에 ChromaticAberration이 없습니다.", this);
        }
        else
        {
            chromaticAberration.active = true;
            chromaticAberration.intensity.overrideState = true;
        }

        if (!globalVolume.profile.TryGet<Vignette>(out vignette))
        {
            LogHelper.LogError("Global Volume에 Vignette가 없습니다.", this);
        }
        else
        {
            vignette.active = true;
            vignette.intensity.overrideState = true;
        }
    }

    /// <summary>
    /// Global Volume의 FilmGrain - intensity를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetFilmGrain(float intensity, bool fade = true)
    {
        if (filmGrain == null)
        {
            LogHelper.LogError("Global Volume에 FilmGrain이 초기화되지 않았습니다.", this);
            return;
        }

        if (fade)
        {
            DOTween.To(GetFilmGrainIntensity, SetFilmGrainIntensity, intensity, 1.0f);
        }
        else
        {
            filmGrain.intensity.value = intensity;
        }
    }

    /// <summary>
    /// Global Volume의 FilmGrain - intensity를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="duration">fade하는데 걸리는 시간</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetFilmGrain(float intensity, float duration, bool fade = true)
    {
        if (filmGrain == null)
        {
            LogHelper.LogError("Global Volume에 FilmGrain이 초기화되지 않았습니다.", this);
            return;
        }

        if (fade)
        {
            DOTween.To(GetFilmGrainIntensity, SetFilmGrainIntensity, intensity, duration);
        }
        else
        {
            filmGrain.intensity.value = intensity;
        }
    }

    private float GetFilmGrainIntensity()
    {
        return filmGrain.intensity.value;
    }

    private void SetFilmGrainIntensity(float x)
    {
        filmGrain.intensity.value = x;
    }

    /// <summary>
    /// Global Volume의 ChromaticAberration - intensity를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetChromaticAberration(float intensity, bool fade = true)
    {
        if (chromaticAberration == null)
        {
            LogHelper.LogError("Global Volume에 ChromaticAberration이 초기화되지 않았습니다.", this);
            return;
        }

        if (fade)
        {
            DOTween.To(GetChromaticAberrationIntensity, SetChromaticAberrationIntensity, intensity, 1f);
        }
        else
        {
            chromaticAberration.intensity.value = intensity;
        }
    }

    /// <summary>
    /// Global Volume의 ChromaticAberration - intensity를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="duration">fade하는데 걸리는 시간</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetChromaticAberration(float intensity, float duration, bool fade = true)
    {
        if (chromaticAberration == null)
        {
            LogHelper.LogError("Global Volume에 ChromaticAberration이 초기화되지 않았습니다.", this);
            return;
        }

        if (fade)
        {
            DOTween.To(GetChromaticAberrationIntensity, SetChromaticAberrationIntensity, intensity, duration);
        }
        else
        {
            chromaticAberration.intensity.value = intensity;
        }
    }

    private float GetChromaticAberrationIntensity()
    {
        return chromaticAberration.intensity.value;
    }

    private void SetChromaticAberrationIntensity(float x)
    {
        chromaticAberration.intensity.value = x;
    }

    /// <summary>
    /// Global Volume의 Vignette - color를 변경
    /// </summary>
    /// <param name="color">변화시킬 color의 목표치</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetVignette(Color color, bool fade = true)
    {
        if (vignette == null)
        {
            LogHelper.LogError("Global Volume에 Vignette가 초기화되지 않았습니다.", this);
            return;
        }

        if (fade)
        {
            DOTween.To(GetVignetteColor, SetVignetteColor, color, 1.0f);
        }
        else
        {
            vignette.color.value = color;
        }
    }

    /// <summary>
    /// Global Volume의 Vignette - color를 변경
    /// </summary>
    /// <param name="color">변화시킬 color의 목표치</param>
    /// <param name="duration">fade하는데 걸리는 시간</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetVignette(Color color, float duration, bool fade = true)
    {
        if (vignette == null)
        {
            LogHelper.LogError("Global Volume에 Vignette가 초기화되지 않았습니다.", this);
            return;
        }

        if (fade)
        {
            DOTween.To(GetVignetteColor, SetVignetteColor, color, duration);
        }
        else
        {
            vignette.color.value = color;
        }
    }

    private Color GetVignetteColor()
    {
        return vignette.color.value;
    }

    private void SetVignetteColor(Color x)
    {
        vignette.color.value = x;
    }

    /// <summary>
    /// Global Volume의 Vignette - intensity를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetVignette(float intensity, bool fade = true)
    {
        if (vignette == null)
        {
            LogHelper.LogError("Global Volume에 Vignette가 초기화되지 않았습니다.", this);
            return;
        }

        if (fade)
        {
            DOTween.To(GetVignetteIntensity, SetVignetteIntensity, intensity, 1.0f);
        }
        else
        {
            vignette.intensity.value = intensity;
        }
    }

    /// <summary>
    /// Global Volume의 Vignette - intensity를 변경
    /// </summary>
    /// <param name="intensity">변화시킬 intensity의 목표치</param>
    /// <param name="duration">fade하는데 걸리는 시간</param>
    /// <param name="fade">false를 입력하지 않으면 기본적으로 fade처리</param>
    public void SetVignette(float intensity, float duration, bool fade = true)
    {
        if (vignette == null)
        {
            LogHelper.LogError("Global Volume에 Vignette가 초기화되지 않았습니다.", this);
            return;
        }

        if (fade)
        {
            DOTween.To(GetVignetteIntensity, SetVignetteIntensity, intensity, duration);
        }
        else
        {
            vignette.intensity.value = intensity;
        }
    }

    private float GetVignetteIntensity()
    {
        return vignette.intensity.value;
    }

    private void SetVignetteIntensity(float x)
    {
        vignette.intensity.value = x;
    }
}