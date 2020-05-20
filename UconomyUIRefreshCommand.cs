using System;
using System.Collections.Generic;
using Rocket.API;
using Rocket.Unturned.Player;

namespace UconomyUI.By.PrincipeIan
{
    public class UconomyUIRefreshCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Console;
        public string Name => "uconomyuirefresh";

        public string Help => "Force refresh UI for a player";

        public string Syntax => "<player>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "uconomyuirefresh" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            if (command.Length < 0)
                return;

            var player = UnturnedPlayer.FromName(command[0]);

            if (player == null)
                return;

            var component = player.GetComponent<UconomyUIComponent>();
            if (component.UiRefreshRunning)
                return;

            component.StartUIRefresh();
        }
    }
    public class MoneyUICommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;
        public string Name => "MoneyUI";

        public string Help => "Disable UI for a player";

        public string Syntax => "";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "MoneyUI" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            var component = (caller as UnturnedPlayer).GetComponent<UconomyUIComponent>();

            component.DisableUI();
        }
    }
}