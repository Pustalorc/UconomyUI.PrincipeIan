using System;
using System.Timers;
using fr34kyn01535.Uconomy;
using Rocket.Unturned.Player;
using SDG.Unturned;

namespace UconomyUI.By.PrincipeIan
{
    public class UconomyUIComponent : UnturnedPlayerComponent
    {
        private Timer _updateUi;
        private double total;

        public bool UiRefreshRunning => _updateUi?.Enabled ?? false;
        public bool Disabled;

        protected override void Load()
        {
            Disabled = false;
            total = 0;
            EffectManager.sendUIEffect(UconomyUI.Instance.Configuration.Instance.EffectId,
                (short)UconomyUI.Instance.Configuration.Instance.EffectId, Player.CSteamID, true,
                $"<color=#{UconomyUI.Instance.Configuration.Instance.colorBalance}>${Uconomy.Instance.Database.GetBalance(Player.CSteamID.ToString()).ToString()}</color>");
        }

        protected override void Unload()
        {
            if (_updateUi != null)
            {
                _updateUi.Stop();
                _updateUi.Elapsed -= UpdateUI;
                _updateUi = null;
            }
        }

        public void StartUIRefresh()
        {
            if (Disabled) return;

            _updateUi = new Timer(UconomyUI.Instance.Configuration.Instance.AutoRefreshDelay);
            _updateUi.Elapsed += UpdateUI;
            _updateUi.Start();
        }

        private void UpdateUI(object sender, ElapsedEventArgs e)
        {
            if (Disabled)
            {
                _updateUi.Stop();
                _updateUi.Elapsed -= UpdateUI;
                _updateUi = null;
                return;
            }

            total += _updateUi.Interval;

            Rocket.Core.Utils.TaskDispatcher.QueueOnMainThread(() =>
            {
                EffectManager.sendUIEffect(UconomyUI.Instance.Configuration.Instance.EffectId,
                    (short)UconomyUI.Instance.Configuration.Instance.EffectId, Player.CSteamID, true,
                    $"<color=#{UconomyUI.Instance.Configuration.Instance.colorBalance}>${Uconomy.Instance.Database.GetBalance(Player.CSteamID.ToString()).ToString()}</color>");
            });

            if (total >= UconomyUI.Instance.Configuration.Instance.AutoRefreshTotalDuration)
            {
                _updateUi.Stop();
                _updateUi.Elapsed -= UpdateUI;
                _updateUi = null;
            }
        }

        internal void DisableUI()
        {
            if (!Disabled)
                EffectManager.askEffectClearByID(UconomyUI.Instance.Configuration.Instance.EffectId, Player.CSteamID);
            else
                EffectManager.sendUIEffect(UconomyUI.Instance.Configuration.Instance.EffectId,
                    (short)UconomyUI.Instance.Configuration.Instance.EffectId, Player.CSteamID, true,
                    $"<color=#{UconomyUI.Instance.Configuration.Instance.colorBalance}>${Uconomy.Instance.Database.GetBalance(Player.CSteamID.ToString()).ToString()}</color>");

            Disabled = !Disabled;
        }
    }
}