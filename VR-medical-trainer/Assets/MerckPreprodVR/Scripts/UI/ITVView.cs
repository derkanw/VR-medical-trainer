using System.Collections;
using Orbox.Async;

namespace MerckPreprodVR
{
    public interface ITVView : IView
    {
        void PlayVideo(int num);
        void PlayVideo(int a, int b);
        IPromise PlayClip(int i);
        IPromise PlayLogo();
        IPromise PlayIntro();
        IPromise PlayMedicalHistoryPhaseStart();
        IPromise PlayMedicalHistoryPhaseEnd();
        IPromise PlayTemperaturePhaseStart();
        IPromise PlayTemperaturePhaseEnd();
        IPromise PlaySaturationPhaseStart();
        IPromise PlaySaturationPhaseEnd();
        IPromise PlayCTPhaseStart();
        IPromise PlayCTPhaseEnd();
        IPromise PlaySummaryPhaseStart();
        IPromise PlaySummaryPhaseEnd();
        void ShowImage(float num);
        IEnumerator PlayOne(int num);
        IEnumerator PlayTwo(int a, int b);
    }
}
