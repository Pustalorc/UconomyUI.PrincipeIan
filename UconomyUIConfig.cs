using Rocket.API;

namespace UconomyUI.By.PrincipeIan
{
    public class UconomyUIConfig : IRocketPluginConfiguration
    {
        public ushort EffectId;
        public string colorBalance;

        public void LoadDefaults()
        {
            EffectId = 18143;
            colorBalance = "#06B409";
        }
    }
}