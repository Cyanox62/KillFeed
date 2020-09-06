using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System.Collections.Generic;

namespace KillFeed
{
	partial class EventHandlers
	{
		private List<Player> killFeedPlayers = new List<Player>();

		internal void OnConsoleCommand(SendingConsoleCommandEventArgs ev)
		{
			string cmd = ev.Name.ToLower();
			if ((cmd == "killfeed" || cmd == "kf") && ev.Player.ReferenceHub.serverRoles.RemoteAdmin)
			{
				ev.IsAllowed = false;
				ev.Color = "white";
				if (killFeedPlayers.Contains(ev.Player))
				{
					killFeedPlayers.Remove(ev.Player);
					ev.ReturnMessage = "You have disabled the kill feed.";
				}
				else
				{
					killFeedPlayers.Add(ev.Player);
					ev.ReturnMessage = "You have enabled the kill feed.";
				}
			}
		}

		internal void OnPlayerDeath(DiedEventArgs ev)
		{
			if (ev.Target.UserId != string.Empty && ev.Killer.UserId != string.Empty && ev.Killer != ev.Target)
			{
				foreach (Player player in killFeedPlayers)
				{
					string damagetype = VerifyDamageType(ev.HitInformations.GetDamageType());
					string msg = KillFeed.instance.Config.KillMessage
						.Replace("%killername", $"<color=#{GetColor(ev.Killer.Role)}>{ev.Killer.Nickname}</color>")
						.Replace("%killerid", ev.Killer.Id.ToString())
						.Replace("%victimname", $"<color=#{GetColor(ev.Target.Role)}>{ev.Target.Nickname}</color>")
						.Replace("%victimid", ev.Target.Id.ToString())
						.Replace("%weapon", damagetype != string.Empty ? damagetype : ev.HitInformations.GetDamageName());
					player.SendConsoleMessage(msg, KillFeed.instance.Config.KillColor);
				}
			}
		}

		internal void OnRoundRestart() => killFeedPlayers.Clear();
	}
}
