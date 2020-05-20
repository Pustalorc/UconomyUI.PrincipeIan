using Rocket.API;

namespace UconomyUI.By.PrincipeIan
{
    public class UconomyUIConfig : IRocketPluginConfiguration
    {
        public double AutoRefreshTotalDuration;
        public double AutoRefreshDelay;
        public ushort EffectId;
        public string colorBalance;

        public void LoadDefaults()
        {
            AutoRefreshTotalDuration = 50000;
            AutoRefreshDelay = 1500;
            EffectId = 18143;
            colorBalance = "#06B409";
        }
    }
}