namespace MerckPreprodVR
{
    public interface IViewFactory
    {
        ILoggerView CreateLoggerView();
        ITVView CreateTVView();
        IWorldSpaceView CreateWelcomeView();
        IWorldSpaceView CreateMedicalHistoryView();
        IWorldSpaceView CreateRestartView();
        ISummaryView CreateSummaryView();
        ITooltipView CreateMedicalHistoryTooltipView();
        ITooltipView CreateThermometerTooltipView();
        ITooltipView CreateThermometerSnapZoneTooltipView();
        ITooltipView CreatePulseoximeterTooltipView();
        ITooltipView CreatePulseoximeterSnapZoneTooltipView();
        ITooltipView CreateCongratulationsTooltipView();
        ITooltipView CreateCtTooltipView();
        IResearchWindowView CreateResearchWindowView();
    }
}