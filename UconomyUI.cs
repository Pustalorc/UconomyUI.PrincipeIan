using Rocket.Core.Plugins;
using SDG.Unturned;
using Rocket.Unturned.Player;
using fr34kyn01535.Uconomy;
using Rocket.Core.Logging;
using Rocket.Unturned.Chat;
using System;

namespace UconomyUI.By.PrincipeIan
{
    public class UconomyUI : RocketPlugin<UconomyUIConfig>
    {
        public static UconomyUI Instance;

        protected override void Load()
        {
            Instance = this;

            Uconomy.Instance.OnBalanceUpdate += Instance_OnBalanceUpdate;

            Logger.Log("Plugin loaded correctly");
            Logger.Log("UconomyUI free By PrincipeIan");
        }

        protected override void Unload()
        {
            Uconomy.Instance.OnBalanceUpdate -= Instance_OnBalanceUpdate;

            Instance = null;

            Logger.Log("Plugin unloaded correctly");
            Logger.Log("UconomyUI free By PrincipeIan");
        }

        private void Instance_OnBalanceUpdate(UnturnedPlayer player, decimal amt)
        {
            if (player == null) return;
            if (player.Player == null) return;
            if (player.GetComponent<UconomyUIComponent>() == null) return;
            if (player.GetComponent<UconomyUIComponent>().Disabled) return;

            EffectManager.sendUIEffect(Configuration.Instance.EffectId, (short) Configuration.Instance.EffectId,
                player.CSteamID, true,
                $"<color=#{Configuration.Instance.colorBalance}>${Uconomy.Instance.Database.GetBalance(player.CSteamID.ToString()).ToString()}</color>");
        }
    }
}