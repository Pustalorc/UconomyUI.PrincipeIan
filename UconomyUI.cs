using Rocket.Unturned;
using Rocket.Core.Plugins;
using SDG.Unturned;
using Rocket.Unturned.Player;
using fr34kyn01535.Uconomy;
using Rocket.Core.Logging;

namespace UconomyUI.By.PrincipeIan
{
    public class UconomyUI : RocketPlugin<UconomyUIConfig>
    {
        public static UconomyUI Instance;

        protected override void Load()
        {
            Instance = this;

            U.Events.OnPlayerConnected += Events_OnPlayerConnected;
            Uconomy.Instance.OnBalanceUpdate += Instance_OnBalanceUpdate;
            U.Events.OnPlayerDisconnected += Events_OnPlayerDisconnected;

            Logger.Log("Plugin loaded correctly");
            Logger.Log("UconomyUI free By PrincipeIan");
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= Events_OnPlayerConnected;
            Uconomy.Instance.OnBalanceUpdate -= Instance_OnBalanceUpdate;
            U.Events.OnPlayerDisconnected -= Events_OnPlayerDisconnected;

            Instance = null;

            Logger.Log("Plugin unloaded correctly");
            Logger.Log("UconomyUI free By PrincipeIan");
        }

        private void Events_OnPlayerDisconnected(UnturnedPlayer player)
        {
            EffectManager.askEffectClearByID(Configuration.Instance.EffectId, player.CSteamID);
        }

        private void Instance_OnBalanceUpdate(UnturnedPlayer player, decimal amt)
        {
            EffectManager.sendUIEffect(Configuration.Instance.EffectId, (short) Configuration.Instance.EffectId, player.CSteamID, true,$"<color=#{Configuration.Instance.colorBalance}>{Uconomy.Instance.Database.GetBalance(player.CSteamID.ToString()).ToString()}¢</color>");
        }

        private void Events_OnPlayerConnected(UnturnedPlayer player)
        {
            EffectManager.sendUIEffect(Configuration.Instance.EffectId, (short)Configuration.Instance.EffectId, player.CSteamID, true, $"<color=#{Configuration.Instance.colorBalance}>{Uconomy.Instance.Database.GetBalance(player.CSteamID.ToString()).ToString()}¢</color>");
        }
    }
}