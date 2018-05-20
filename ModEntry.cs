using System;
using System.Collections.Generic;
using System.IO;
using Mopsy_Ranch_Livin;
using StardewValley;
using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace MopsyRanchLife
{
    public class ModEntry : Mod, IAssetEditor
    {
        private string Suffix { get; set; } = "Ranch";
        private ModConfig _config;

        public override void Entry(IModHelper helper)
        {
            helper.ConsoleCommands.Add("farm_setsuffix", "Sets the player's farm suffix.\n\nUsage: farm_setsuffix <value>\n- value: farm suffix, e.g. Ranch.", ChangeFarmSuffix);
            SaveEvents.AfterLoad += this.SaveEvents_AfterLoad;
        }

        public void SaveEvents_AfterLoad(object sender, EventArgs e)
        {
            _config = Helper.ReadJsonFile<ModConfig>(Path.Combine(Constants.CurrentSavePath, "Mopsy-Ranch-Livin.json")) ?? new ModConfig();
            Console.WriteLine("Debug: _config.Suffix = " + _config.Suffix);
            if (_config.Suffix != Suffix)
            {
                ChangeFarmSuffix("config", new[] { _config.Suffix });
            }
        }

        public void ChangeFarmSuffix(string name, string[] args)
        {
            Suffix = args[0];
            //Console.WriteLine("Debug: " + name);
            if (name == "farm_setsuffix")
            {
                _config.Suffix = Suffix;
                Helper.WriteJsonFile(Path.Combine(Constants.CurrentSavePath, "Mopsy-Ranch-Livin.json"), _config);
            }
            
            Helper.Content.InvalidateCache("Strings/Locations");
            Helper.Content.InvalidateCache("Strings/Notes");
            Helper.Content.InvalidateCache("Strings/SpeechBubbles");
            Helper.Content.InvalidateCache("Strings/StringsFromCSFiles");
            Helper.Content.InvalidateCache("Strings/StringsFromMaps");
            Helper.Content.InvalidateCache("Strings/UI");
        }

        /// <summary>Get whether this instance can edit the given asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public bool CanEdit<T>(IAssetInfo asset)
        {
            return
                asset.AssetNameEquals("Strings/Locations")
                || asset.AssetNameEquals("Strings/Notes")
                || asset.AssetNameEquals("Strings/SpeechBubbles")
                || asset.AssetNameEquals("Strings/StringsFromCSFiles")
                || asset.AssetNameEquals("Strings/StringsFromMaps")
                || asset.AssetNameEquals("Strings/UI");
        }

        /// <summary>Edit a matched asset.</summary>
        /// <param name="asset">A helper which encapsulates metadata about an asset and enables changes to it.</param>
        public void Edit<T>(IAssetData asset)
        {
            const string find = "Farm";
            const string lowerFind = "farm";
            string lowerSuffix = Suffix.ToLower();
            IDictionary<string, string> data = asset.AsDictionary<string, string>().Data;

            if (asset.AssetNameEquals("Strings/Locations"))
            {
                data["ScienceHouse_CarpenterMenu_Construct"] = data["ScienceHouse_CarpenterMenu_Construct"].Replace(find, Suffix);
                data["Farm_WeedsDestruction"] = data["Farm_WeedsDestruction"].Replace(lowerFind, lowerSuffix);
                data["WitchHut_EvilShrineRightActivate"] = data["WitchHut_EvilShrineRightActivate"].Replace(lowerFind, lowerSuffix);
                data["WitchHut_EvilShrineRightDeActivate"] = data["WitchHut_EvilShrineRightDeActivate"].Replace(lowerFind, lowerSuffix);
            }

            if (asset.AssetNameEquals("Strings/Notes"))
            {
                data["2"] = data["2"].Replace(lowerFind, lowerSuffix);
                data["6"] = data["6"].Replace(lowerFind, lowerSuffix);
            }

            if (asset.AssetNameEquals("Strings/SpeechBubbles"))
            {
                data["AnimalShop_Marnie_Greeting3"] = data["AnimalShop_Marnie_Greeting3"].Replace(find, Suffix);
            }

            if (asset.AssetNameEquals("Strings/StringsFromCSFiles"))
            {
                data["Event.cs.1306"] = data["Event.cs.1306"].Replace(lowerFind, lowerSuffix);
                data["Event.cs.1308"] = data["Event.cs.1308"].Replace(find, Suffix);
                data["Event.cs.1315"] = data["Event.cs.1315"].Replace(lowerFind, lowerSuffix);
                data["Event.cs.1317"] = data["Event.cs.1317"].Replace(find, Suffix);
                data["Event.cs.1843"] = data["Event.cs.1843"].Replace(lowerFind, lowerSuffix);
                data["Farmer.cs.1972"] = data["Farmer.cs.1972"].Replace(lowerFind, lowerSuffix);
                data["Game1.cs.2782"] = data["Game1.cs.2782"].Replace(lowerFind, lowerSuffix);
                data["NPC.cs.4474"] = data["NPC.cs.4474"].Replace(lowerFind, lowerSuffix);
                data["NPC.cs.4485"] = data["NPC.cs.4485"].Replace(find, Suffix);
                data["Utility.cs.5229"] = data["Utility.cs.5229"].Replace(lowerFind, lowerSuffix);
                data["Utility.cs.5230"] = data["Utility.cs.5230"].Replace("lowerFind", lowerSuffix);
                data["BlueprintsMenu.cs.10012"] = data["BlueprintsMenu.cs.10012"].Replace(find, Suffix);
                data["CataloguePage.cs.10148"] = data["CataloguePage.cs.10148"].Replace(lowerFind, lowerSuffix);
                data["LoadGameMenu.cs.11019"] = data["LoadGameMenu.cs.11019"].Replace(find, Suffix);
                data["MapPage.cs.11064"] = data["MapPage.cs.11064"].Replace(find, Suffix);
                data["GrandpaStory.cs.12051"] = data["GrandpaStory.cs.12051"].Replace(find, Suffix);
                data["GrandpaStory.cs.12055"] = data["GrandpaStory.cs.12055"].Replace(find, Suffix);
                data["HoeDirt.cs.13919"] = data["HoeDirt.cs.13919"].Replace(lowerFind, lowerSuffix);
                data["Axe.cs.14023"] = data["Axe.cs.14023"].Replace(lowerFind, lowerSuffix);
            }

            if (asset.AssetNameEquals("Strings/StringsFromMaps"))
            {
                data["Forest.1"] = data["Forest.1"].Replace(find, Suffix);
            }

            if (asset.AssetNameEquals("Strings/UI"))
            {
                data["Character_Farm"] = data["Character_Farm"].Replace(find, Suffix);
                data["Character_FarmNameSuffix"] = data["Character_FarmNameSuffix"].Replace(find, Suffix);
                data["Inventory_FarmName"] = data["Inventory_FarmName"].Replace(find, Suffix);
                data["CoopMenu_HostNewFarm"] = data["CoopMenu_HostNewFarm"].Replace(find, Suffix);
                data["CoopMenu_HostFile"] = data["CoopMenu_HostFile"].Replace(find, Suffix);
                data["CoopMenu_RevisitFriendFarm"] = data["CoopMenu_RevisitFriendFarm"].Replace(find, Suffix);
                data["CoopMenu_JoinFriendFarm"] = data["CoopMenu_JoinFriendFarm"].Replace(find, Suffix);
                data["Chat_MuseumComplete"] = data["Chat_MuseumComplete"].Replace(find, Suffix);
                data["Chat_Museum40"] = data["Chat_Museum40"].Replace(find, Suffix);
                data["Chat_Earned15k"] = data["Chat_Earned15k"].Replace(find, Suffix);
                data["Chat_Earned50k"] = data["Chat_Earned50k"].Replace(find, Suffix);
                data["Chat_Earned250k"] = data["Chat_Earned250k"].Replace(find, Suffix);
                data["Chat_Earned1m"] = data["Chat_Earned1m"].Replace(find, Suffix);
                data["Chat_Earned10m"] = data["Chat_Earned10m"].Replace(find, Suffix);
                data["Chat_Earned100m"] = data["Chat_Earned100m"].Replace(find, Suffix);
            }
        }
    }
}
