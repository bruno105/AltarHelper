﻿using System.Collections.Generic;

namespace AltarHelper
{
    internal static class AltarModsConstants
    {
        public static Dictionary<string, AffectedTarget> FilterTargetDict { get; } = new Dictionary<string, AffectedTarget> {
            { "Any", AffectedTarget.Any },
            { "Player", AffectedTarget.Player },
            { "Minion", AffectedTarget.Minions },
            { "Boss", AffectedTarget.FinalBoss }
        };
        public static Dictionary<string, AffectedTarget> AltarTargetDict { get; } = new Dictionary<string, AffectedTarget> {
            { "Player gains:", AffectedTarget.Player },
            { "Eldritch Minions gain:", AffectedTarget.Minions },
            { "Map boss gains:", AffectedTarget.FinalBoss }
        };

        public static readonly IReadOnlyList<(string Id, string Name, string Type)> AltarTypes = new List<(string, string, string)>
        {
           ("+#% to maximum Cold Resistance", "+10%% to maximum Cold Resistance", "Boss"),
            ("+#% to Cold Resistance", "+80%% to Cold Resistance", "Boss"),
            ("+#% to maximum Lightning Resistance", "+10%% to maximum Lightning Resistance", "Boss"),
            ("+#% to Lightning Resistance", "+80%% to Lightning Resistance", "Boss"),
            ("#% additional Physical Damage Reduction", "(50–70)%% additional Physical Damage Reduction", "Boss"),
            ("All Damage with Hits can Chill", "All Damage with Hits can Chill", "Boss"),
            ("Hits always Shock", "Hits always Shock", "Boss"),
            ("All Damage can Shock", "All Damage can Shock", "Boss"),
            ("Prevent +#% of Suppressed Spell Damage", "Prevent +(20–30)%% of Suppressed Spell Damage", "Boss"),
            ("+#% chance to Suppress Spell Damage", "+100%% chance to Suppress Spell Damage", "Boss"),
            ("Gain #% of Physical Damage as Extra Damage of a random Element", "Gain (50–80)%% of Physical Damage as Extra Damage of a random Element", "Boss"),
            ("Damage Penetrates #% of Enemy Elemental Resistances", "Damage Penetrates (15–25)%% of Enemy Elemental Resistances", "Boss"),
            ("Gain #% of Physical Damage as Extra Cold Damage", "Gain (50–80)%% of Physical Damage as Extra Cold Damage", "Boss"),
            ("Cover Enemies in Frost on Hit", "Cover Enemies in Frost on Hit", "Boss"),
            ("#% Global chance to Blind Enemies on hit", "100%% Global chance to Blind Enemies on hit", "Boss"),
            ("#% increased Blind Effect", "(100–200)%% increased Blind Effect", "Boss"),
            ("Gain #% of Maximum Life as Extra Maximum Energy Shield", "Gain (50–70)%% of Maximum Life as Extra Maximum Energy Shield", "Boss"),
            ("Gain #% of Physical Damage as Extra Cold Damage", "Gain (70–130)%% of Physical Damage as Extra Cold Damage", "Boss"),
            ("Gain #% of Physical Damage as Extra Lightning Damage", "Gain (70–130)%% of Physical Damage as Extra Lightning Damage", "Boss"),
            ("Eldritch Tentacles", "Eldritch Tentacles", "Boss"),
            ("Create Consecrated Ground on Hit, lasting # seconds", "Create Consecrated Ground on Hit, lasting 6 seconds", "Boss"),
            ("+#% to maximum Fire Resistance", "+10%% to maximum Fire Resistance", "Boss"),
            ("+#% to Fire Resistance", "+80%% to Fire Resistance", "Boss"),
            ("+#% to maximum Chaos Resistance", "+10%% to maximum Chaos Resistance", "Boss"),
            ("+#% to Chaos Resistance", "+80%% to Chaos Resistance", "Boss"),
            ("+# to Armour", "+50000 to Armour", "Boss"),
            ("Gain #% of Physical Damage as Extra Fire Damage", "Gain (50–80)%% of Physical Damage as Extra Fire Damage", "Boss"),
            ("Cover Enemies in Ash on Hit", "Cover Enemies in Ash on Hit", "Boss"),
            ("Hits always Ignite", "Hits always Ignite", "Boss"),
            ("All Damage can Ignite", "All Damage can Ignite", "Boss"),
            ("Poison on Hit", "Poison on Hit", "Boss"),
            ("All Damage from Hits can Poison", "All Damage from Hits can Poison", "Boss"),
            ("Enemies lose # Flask Charges every # seconds and cannot gain Flask Charges for # seconds after being Hit", "Enemies lose 6 Flask Charges every 3 seconds and cannot gain Flask Charges for 6 seconds after being Hit", "Boss"),
            ("Your hits inflict Malediction", "Your hits inflict Malediction", "Boss"),
            ("Gain #% of Physical Damage as Extra Fire Damage", "Gain (70–130)%% of Physical Damage as Extra Fire Damage", "Boss"),
            ("Gain #% of Physical Damage as Extra Chaos Damage", "Gain (70–130)%% of Physical Damage as Extra Chaos Damage", "Boss"),
            ("#% increased Armour", "(100–200)%% increased Armour", "Boss"),
            ("#% increased Evasion Rating", "(100–200)%% increased Evasion Rating", "Boss"),
            ("Nearby Enemies are Hindered, with #% reduced Movement Speed", "Nearby Enemies are Hindered, with 40%% reduced Movement Speed", "Boss"),
            ("hinder aura behaviour variation [#]", "hinder aura behaviour variation [1]", "Boss"),
            ("Final Boss drops # additional Divine Orbs", "Final Boss drops (2–4) additional Divine Orbs", "Boss"),
            ("Final Boss drops # additional Exalted Orbs", "Final Boss drops (2–4) additional Exalted Orbs", "Boss"),
            ("Final Boss drops # additional Regal Orbs", "Final Boss drops (2–4) additional Regal Orbs", "Boss"),
            ("Final Boss drops # additional Lesser Eldritch Ichors", "Final Boss drops (2–4) additional Lesser Eldritch Ichors", "Boss"),
            ("Final Boss drops # additional Greater Eldritch Ichors", "Final Boss drops (2–4) additional Greater Eldritch Ichors", "Boss"),
            ("Final Boss drops # additional Grand Eldritch Ichors", "Final Boss drops (2–4) additional Grand Eldritch Ichors", "Boss"),
            ("Final Boss drops # additional Eldritch Chaos Orbs", "Final Boss drops (2–4) additional Eldritch Chaos Orbs", "Boss"),
            ("Final Boss drops # additional Eldritch Exalted Orbs", "Final Boss drops (2–4) additional Eldritch Exalted Orbs", "Boss"),
            ("Final Boss drops # additional Eldritch Orbs of Annulment", "Final Boss drops (2–4) additional Eldritch Orbs of Annulment", "Boss"),
            ("Final Boss drops # additional Chaos Orbs", "Final Boss drops (2–4) additional Chaos Orbs", "Boss"),
            ("Final Boss drops # additional Orbs of Alteration", "Final Boss drops (2–4) additional Orbs of Alteration", "Boss"),
            ("Final Boss drops # additional Blessed Orbs", "Final Boss drops (2–4) additional Blessed Orbs", "Boss"),
            ("Final Boss drops # additional Orbs of Scouring", "Final Boss drops (2–4) additional Orbs of Scouring", "Boss"),
            ("Final Boss drops # additional Chromatic Orbs", "Final Boss drops (2–4) additional Chromatic Orbs", "Boss"),
            ("Final Boss drops # additional Orbs of Fusing", "Final Boss drops (2–4) additional Orbs of Fusing", "Boss"),
            ("Final Boss drops # additional Jeweller's Orbs", "Final Boss drops (2–4) additional Jeweller's Orbs", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward Currency", "Final Boss drops (2–4) additional Divination Cards which reward Currency", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward Basic Currency", "Final Boss drops (2–4) additional Divination Cards which reward Basic Currency", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward League Currency", "Final Boss drops (2–4) additional Divination Cards which reward League Currency", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward other Divination Cards", "Final Boss drops (2–4) additional Divination Cards which reward other Divination Cards", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward Gems", "Final Boss drops (2–4) additional Divination Cards which reward Gems", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward Levelled Gems", "Final Boss drops (2–4) additional Divination Cards which reward Levelled Gems", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward Quality Gems", "Final Boss drops (2–4) additional Divination Cards which reward Quality Gems", "Boss"),
            ("Final Boss drops # additional Orbs of Binding", "Final Boss drops (2–4) additional Orbs of Binding", "Boss"),
            ("Final Boss drops # additional Orbs of Horizons", "Final Boss drops (2–4) additional Orbs of Horizons", "Boss"),
            ("Final Boss drops # additional Orbs of Unmaking", "Final Boss drops (2–4) additional Orbs of Unmaking", "Boss"),
            ("Final Boss drops # additional Cartographer's Chisels", "Final Boss drops (2–4) additional Cartographer's Chisels", "Boss"),
            ("Final Boss drops # additional Lesser Eldritch Embers", "Final Boss drops (2–4) additional Lesser Eldritch Embers", "Boss"),
            ("Final Boss drops # additional Greater Eldritch Embers", "Final Boss drops (2–4) additional Greater Eldritch Embers", "Boss"),
            ("Final Boss drops # additional Grand Eldritch Embers", "Final Boss drops (2–4) additional Grand Eldritch Embers", "Boss"),
            ("Final Boss drops # additional Orbs of Annulment", "Final Boss drops (2–4) additional Orbs of Annulment", "Boss"),
            ("Final Boss drops # additional Vaal Orbs", "Final Boss drops (2–4) additional Vaal Orbs", "Boss"),
            ("Final Boss drops # additional Enkindling Orbs", "Final Boss drops (2–4) additional Enkindling Orbs", "Boss"),
            ("Final Boss drops # additional Instilling Orbs", "Final Boss drops (2–4) additional Instilling Orbs", "Boss"),
            ("Final Boss drops # additional Orbs of Regret", "Final Boss drops (2–4) additional Orbs of Regret", "Boss"),
            ("Final Boss drops # additional Glassblower's Baubles", "Final Boss drops (2–4) additional Glassblower's Baubles", "Boss"),
            ("Final Boss drops # additional Gemcutter's Prisms", "Final Boss drops (2–4) additional Gemcutter's Prisms", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward a Unique Item", "Final Boss drops (2–4) additional Divination Cards which reward a Unique Item", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward a Unique Weapon", "Final Boss drops (2–4) additional Divination Cards which reward a Unique Weapon", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward a Unique Armour", "Final Boss drops (2–4) additional Divination Cards which reward a Unique Armour", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward Unique Jewellery", "Final Boss drops (2–4) additional Divination Cards which reward Unique Jewellery", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward a Corrupted Unique Item", "Final Boss drops (2–4) additional Divination Cards which reward a Corrupted Unique Item", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward a Map", "Final Boss drops (2–4) additional Divination Cards which reward a Map", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward a Unique Map", "Final Boss drops (2–4) additional Divination Cards which reward a Unique Map", "Boss"),
            ("Final Boss drops # additional Divination Cards which reward a Corrupted Item", "Final Boss drops (2–4) additional Divination Cards which reward a Corrupted Item", "Boss"),
            ("Final Boss drops # additional Lesser Eldritch Ichor", "Final Boss drops 1 additional Lesser Eldritch Ichor", "Boss"),
            ("Final Boss drops # additional Greater Eldritch Ichor", "Final Boss drops 1 additional Greater Eldritch Ichor", "Boss"),
            ("Final Boss drops # additional Grand Eldritch Ichor", "Final Boss drops 1 additional Grand Eldritch Ichor", "Boss"),
            ("Final Boss drops # additional Lesser Eldritch Ember", "Final Boss drops 1 additional Lesser Eldritch Ember", "Boss"),
            ("Final Boss drops # additional Greater Eldritch Ember", "Final Boss drops 1 additional Greater Eldritch Ember", "Boss"),
            ("Final Boss drops # additional Grand Eldritch Ember", "Final Boss drops 1 additional Grand Eldritch Ember", "Boss"),
            ("Final Boss drops # additional Breach Scarabs", "Final Boss drops (2–4) additional Breach Scarabs", "Boss"),
            ("Final Boss drops # additional Delirium Scarabs", "Final Boss drops (2–4) additional Delirium Scarabs", "Boss"),
            ("Final Boss drops # additional Legion Scarabs", "Final Boss drops (2–4) additional Legion Scarabs", "Boss"),
            ("Final Boss drops # additional Blight Scarabs", "Final Boss drops (2–4) additional Blight Scarabs", "Boss"),
            ("Final Boss drops # additional Ritual Scarabs", "Final Boss drops (2–4) additional Ritual Scarabs", "Boss"),
            ("Final Boss drops # additional Harvest Scarabs", "Final Boss drops (2–4) additional Harvest Scarabs", "Boss"),
            ("Final Boss drops # additional Ultimatum Scarabs", "Final Boss drops (2–4) additional Ultimatum Scarabs", "Boss"),
            ("Final Boss drops # additional Abyss Scarabs", "Final Boss drops (2–4) additional Abyss Scarabs", "Boss"),
            ("Final Boss drops # additional Expedition Scarabs", "Final Boss drops (2–4) additional Expedition Scarabs", "Boss"),
            ("Final Boss drops # additional Betrayal Scarabs", "Final Boss drops (2–4) additional Betrayal Scarabs", "Boss"),
            ("Final Boss drops # additional Bestiary Scarabs", "Final Boss drops (2–4) additional Bestiary Scarabs", "Boss"),
            ("Final Boss drops # additional Incursion Scarabs", "Final Boss drops (2–4) additional Incursion Scarabs", "Boss"),
            ("Final Boss drops # additional Sulphite Scarabs", "Final Boss drops (2–4) additional Sulphite Scarabs", "Boss"),
            ("Final Boss drops # additional Influence Scarabs", "Final Boss drops (2–4) additional Influence Scarabs", "Boss"),
            ("Final Boss drops # additional Cartography Scarabs", "Final Boss drops (2–4) additional Cartography Scarabs", "Boss"),
            ("Final Boss drops # additional Divination Scarabs", "Final Boss drops (2–4) additional Divination Scarabs", "Boss"),
            ("Final Boss drops # additional Anarchy Scarabs", "Final Boss drops (2–4) additional Anarchy Scarabs", "Boss"),
            ("Final Boss drops # additional Harbinger Scarabs", "Final Boss drops (2–4) additional Harbinger Scarabs", "Boss"),
            ("Final Boss drops # additional Miscellaneous Scarabs", "Final Boss drops (2–4) additional Miscellaneous Scarabs", "Boss"),
            ("Final Boss drops # additional Beyond Scarabs", "Final Boss drops (2–4) additional Beyond Scarabs", "Boss"),
            ("Final Boss drops # additional Torment Scarabs", "Final Boss drops (2–4) additional Torment Scarabs", "Boss"),
            ("Final Boss drops # additional Ambush Scarabs", "Final Boss drops (2–4) additional Ambush Scarabs", "Boss"),
            ("Final Boss drops # additional Domination Scarabs", "Final Boss drops (2–4) additional Domination Scarabs", "Boss"),
            ("Final Boss drops # additional Essence Scarabs", "Final Boss drops (2–4) additional Essence Scarabs", "Boss"),
            ("Final Boss drops # additional Reliquary Scarabs", "Final Boss drops (2–4) additional Reliquary Scarabs", "Boss"),
            ("Hits have #% chance to ignore Enemy Physical Damage Reduction", "Hits have (50–80)%% chance to ignore Enemy Physical Damage Reduction", "Minion"),
            ("Skills fire # additional Projectiles", "Skills fire (3–5) additional Projectiles", "Minion"),
            ("#% increased Attack Speed", "(30–50)%% increased Attack Speed", "Minion"),
            ("#% increased Cast Speed", "(30–50)%% increased Cast Speed", "Minion"),
            ("#% increased Movement Speed", "(30–50)%% increased Movement Speed", "Minion"),
            ("+#% to maximum Cold Resistance", "+10%% to maximum Cold Resistance", "Minion"),
            ("+#% to Cold Resistance", "+80%% to Cold Resistance", "Minion"),
            ("+#% to maximum Lightning Resistance", "+10%% to maximum Lightning Resistance", "Minion"),
            ("+#% to Lightning Resistance", "+80%% to Lightning Resistance", "Minion"),
            ("#% additional Physical Damage Reduction", "(50–80)%% additional Physical Damage Reduction", "Minion"),
            ("Prevent +#% of Suppressed Spell Damage", "Prevent +(20–30)%% of Suppressed Spell Damage", "Minion"),
            ("+#% chance to Suppress Spell Damage", "+100%% chance to Suppress Spell Damage", "Minion"),
            ("#% chance to remove a random Charge from Enemy on Hit", "100%% chance to remove a random Charge from Enemy on Hit", "Minion"),
            ("Drops Chilled Ground on Death, lasting # seconds", "Drops Chilled Ground on Death, lasting 3 seconds", "Minion"),
            ("#% chance to create Shocked Ground on Death, lasting # seconds", "100%% chance to create Shocked Ground on Death, lasting 3 seconds", "Minion"),
            ("Inflict # Grasping Vine on Hit", "Inflict 1 Grasping Vine on Hit", "Minion"),
            ("Curse Enemies with Punishment on Hit", "Curse Enemies with Punishment on Hit", "Minion"),
            ("Gain #% of Physical Damage as Extra Lightning Damage", "Gain (70–130)%% of Physical Damage as Extra Lightning Damage", "Minion"),
            ("Gain #% of Physical Damage as Extra Cold Damage", "Gain (70–130)%% of Physical Damage as Extra Cold Damage", "Minion"),
            ("Drops Burning Ground on Death, lasting # seconds", "Drops Burning Ground on Death, lasting 3 seconds", "Minion"),
            ("Create Consecrated Ground on Death, lasting # seconds", "Create Consecrated Ground on Death, lasting 6 seconds", "Minion"),
            ("Gain #% of Physical Damage as Extra Damage of a random Element", "Gain (70–130)%% of Physical Damage as Extra Damage of a random Element", "Minion"),
            ("Inflict Fire, Cold, and Lightning Exposure on Hit", "Inflict Fire, Cold, and Lightning Exposure on Hit", "Minion"),
            ("Enemies lose # Flask Charges every # seconds and cannot gain Flask Charges for # seconds after being Hit", "Enemies lose 6 Flask Charges every 3 seconds and cannot gain Flask Charges for 6 seconds after being Hit", "Minion"),
            ("+#% to maximum Fire Resistance", "+10%% to maximum Fire Resistance", "Minion"),
            ("+#% to Fire Resistance", "+80%% to Fire Resistance", "Minion"),
            ("+#% to maximum Chaos Resistance", "+10%% to maximum Chaos Resistance", "Minion"),
            ("+#% to Chaos Resistance", "+80%% to Chaos Resistance", "Minion"),
            ("+# to Armour", "+50000 to Armour", "Minion"),
            ("#% increased Area of Effect", "(70–130)%% increased Area of Effect", "Minion"),
            ("#% increased Evasion Rating", "(250–500)%% increased Evasion Rating", "Minion"),
            ("Hits always Ignite", "Hits always Ignite", "Minion"),
            ("All Damage can Ignite", "All Damage can Ignite", "Minion"),
            ("Poison on Hit", "Poison on Hit", "Minion"),
            ("All Damage from Hits can Poison", "All Damage from Hits can Poison", "Minion"),
            ("Curse Enemies with Vulnerability on Hit", "Curse Enemies with Vulnerability on Hit", "Minion"),
            ("Gain #% of Physical Damage as Extra Chaos Damage", "Gain (70–130)%% of Physical Damage as Extra Chaos Damage", "Minion"),
            ("Gain #% of Physical Damage as Extra Fire Damage", "Gain (70–130)%% of Physical Damage as Extra Fire Damage", "Minion"),
            ("Upside Eldritch Minions gain:", "Upside Eldritch Minions gain:", "Minion"),
            ("#% chance to drop an additional Divine Orb", "(1.6–3.2)%% chance to drop an additional Divine Orb", "Minion"),
            ("#% chance to drop an additional Exalted Orb", "(1.6–3.2)%% chance to drop an additional Exalted Orb", "Minion"),
            ("#% chance to drop an additional Regal Orb", "(1.6–3.2)%% chance to drop an additional Regal Orb", "Minion"),
            ("#% chance to drop an additional Lesser Eldritch Ichor", "(1.6–3.2)%% chance to drop an additional Lesser Eldritch Ichor", "Minion"),
            ("#% chance to drop an additional Greater Eldritch Ichor", "(1.6–3.2)%% chance to drop an additional Greater Eldritch Ichor", "Minion"),
            ("#% chance to drop an additional Grand Eldritch Ichor", "(1.6–3.2)%% chance to drop an additional Grand Eldritch Ichor", "Minion"),
            ("#% chance to drop an additional Eldritch Chaos Orb", "(1.6–3.2)%% chance to drop an additional Eldritch Chaos Orb", "Minion"),
            ("#% chance to drop an additional Eldritch Exalted Orb", "(1.6–3.2)%% chance to drop an additional Eldritch Exalted Orb", "Minion"),
            ("#% chance to drop an additional Eldritch Orb of Annulment", "(1.6–3.2)%% chance to drop an additional Eldritch Orb of Annulment", "Minion"),
            ("#% chance to drop an additional Chaos Orb", "(1.6–3.2)%% chance to drop an additional Chaos Orb", "Minion"),
            ("#% chance to drop an additional Orb of Alteration", "(1.6–3.2)%% chance to drop an additional Orb of Alteration", "Minion"),
            ("#% chance to drop an additional Blessed Orb", "(1.6–3.2)%% chance to drop an additional Blessed Orb", "Minion"),
            ("#% chance to drop an additional Orb of Scouring", "(1.6–3.2)%% chance to drop an additional Orb of Scouring", "Minion"),
            ("#% chance to drop an additional Chromatic Orb", "(1.6–3.2)%% chance to drop an additional Chromatic Orb", "Minion"),
            ("#% chance to drop an additional Orb of Fusing", "(1.6–3.2)%% chance to drop an additional Orb of Fusing", "Minion"),
            ("#% chance to drop an additional Jeweller's Orb", "(1.6–3.2)%% chance to drop an additional Jeweller's Orb", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards Currency", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards Currency", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards Basic Currency", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards Basic Currency", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards League Currency", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards League Currency", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards other Divination Cards", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards other Divination Cards", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards Gems", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards Gems", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards Levelled Gems", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards Levelled Gems", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards Quality Gems", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards Quality Gems", "Minion"),
            ("#% chance to drop an additional Orb of Binding", "(1.6–3.2)%% chance to drop an additional Orb of Binding", "Minion"),
            ("#% chance to drop an additional Orb of Horizons", "(1.6–3.2)%% chance to drop an additional Orb of Horizons", "Minion"),
            ("#% chance to drop an additional Orb of Unmaking", "(1.6–3.2)%% chance to drop an additional Orb of Unmaking", "Minion"),
            ("#% chance to drop an additional Cartographer's Chisel", "(1.6–3.2)%% chance to drop an additional Cartographer's Chisel", "Minion"),
            ("#% chance to drop an additional Lesser Eldritch Ember", "(1.6–3.2)%% chance to drop an additional Lesser Eldritch Ember", "Minion"),
            ("#% chance to drop an additional Greater Eldritch Ember", "(1.6–3.2)%% chance to drop an additional Greater Eldritch Ember", "Minion"),
            ("#% chance to drop an additional Grand Eldritch Ember", "(1.6–3.2)%% chance to drop an additional Grand Eldritch Ember", "Minion"),
            ("#% chance to drop an additional Orb of Annulment", "(1.6–3.2)%% chance to drop an additional Orb of Annulment", "Minion"),
            ("#% chance to drop an additional Vaal Orb", "(1.6–3.2)%% chance to drop an additional Vaal Orb", "Minion"),
            ("#% chance to drop an additional Enkindling Orb", "(1.6–3.2)%% chance to drop an additional Enkindling Orb", "Minion"),
            ("#% chance to drop an additional Instilling Orb", "(1.6–3.2)%% chance to drop an additional Instilling Orb", "Minion"),
            ("#% chance to drop an additional Orb of Regret", "(1.6–3.2)%% chance to drop an additional Orb of Regret", "Minion"),
            ("#% chance to drop an additional Glassblower's Bauble", "(1.6–3.2)%% chance to drop an additional Glassblower's Bauble", "Minion"),
            ("#% chance to drop an additional Gemcutter's Prism", "(1.6–3.2)%% chance to drop an additional Gemcutter's Prism", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards a Unique Item", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards a Unique Item", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards a Unique Weapon", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards a Unique Weapon", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards a Unique Armour", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards a Unique Armour", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards Unique Jewellery", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards Unique Jewellery", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards a Corrupted Unique Item", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards a Corrupted Unique Item", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards a Map", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards a Map", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards a Unique Map", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards a Unique Map", "Minion"),
            ("#% chance to drop an additional Divination Card which rewards a Corrupted Item", "(1.6–3.2)%% chance to drop an additional Divination Card which rewards a Corrupted Item", "Minion"),
            ("#% chance to drop an additional Lesser Eldritch Ichor", "(1.2–2.4)%% chance to drop an additional Lesser Eldritch Ichor", "Minion"),
            ("#% chance to drop an additional Greater Eldritch Ichor", "(1.2–2.4)%% chance to drop an additional Greater Eldritch Ichor", "Minion"),
            ("#% chance to drop an additional Grand Eldritch Ichor", "(1.2–2.4)%% chance to drop an additional Grand Eldritch Ichor", "Minion"),
            ("#% chance to drop an additional Lesser Eldritch Ember", "(1.2–2.4)%% chance to drop an additional Lesser Eldritch Ember", "Minion"),
            ("#% chance to drop an additional Greater Eldritch Ember", "(1.2–2.4)%% chance to drop an additional Greater Eldritch Ember", "Minion"),
            ("#% chance to drop an additional Grand Eldritch Ember", "(1.2–2.4)%% chance to drop an additional Grand Eldritch Ember", "Minion"),
            ("#% chance to drop an additional Breach Scarab", "(1.6–3.2)%% chance to drop an additional Breach Scarab", "Minion"),
            ("#% chance to drop an additional Delirium Scarab", "(1.6–3.2)%% chance to drop an additional Delirium Scarab", "Minion"),
            ("#% chance to drop an additional Legion Scarab", "(1.6–3.2)%% chance to drop an additional Legion Scarab", "Minion"),
            ("#% chance to drop an additional Blight Scarab", "(1.6–3.2)%% chance to drop an additional Blight Scarab", "Minion"),
            ("#% chance to drop an additional Ritual Scarab", "(1.6–3.2)%% chance to drop an additional Ritual Scarab", "Minion"),
            ("#% chance to drop an additional Harvest Scarab", "(1.6–3.2)%% chance to drop an additional Harvest Scarab", "Minion"),
            ("#% chance to drop an additional Ultimatum Scarab", "(1.6–3.2)%% chance to drop an additional Ultimatum Scarab", "Minion"),
            ("#% chance to drop an additional Abyss Scarab", "(1.6–3.2)%% chance to drop an additional Abyss Scarab", "Minion"),
            ("#% chance to drop an additional Expedition Scarab", "(1.6–3.2)%% chance to drop an additional Expedition Scarab", "Minion"),
            ("#% chance to drop an additional Betrayal Scarab", "(1.6–3.2)%% chance to drop an additional Betrayal Scarab", "Minion"),
            ("#% chance to drop an additional Bestiary Scarab", "(1.6–3.2)%% chance to drop an additional Bestiary Scarab", "Minion"),
            ("#% chance to drop an additional Incursion Scarab", "(1.6–3.2)%% chance to drop an additional Incursion Scarab", "Minion"),
            ("#% chance to drop an additional Sulphite Scarab", "(1.6–3.2)%% chance to drop an additional Sulphite Scarab", "Minion"),
            ("#% chance to drop an additional Influence Scarab", "(1.6–3.2)%% chance to drop an additional Influence Scarab", "Minion"),
            ("#% chance to drop an additional Cartography Scarab", "(1.6–3.2)%% chance to drop an additional Cartography Scarab", "Minion"),
            ("#% chance to drop an additional Divination Scarab", "(1.6–3.2)%% chance to drop an additional Divination Scarab", "Minion"),
            ("#% chance to drop an additional Anarchy Scarab", "(1.6–3.2)%% chance to drop an additional Anarchy Scarab", "Minion"),
            ("#% chance to drop an additional Harbinger Scarab", "(1.6–3.2)%% chance to drop an additional Harbinger Scarab", "Minion"),
            ("#% chance to drop an additional Miscellaneous Scarab", "(1.6–3.2)%% chance to drop an additional Miscellaneous Scarab", "Minion"),
            ("#% chance to drop an additional Beyond Scarab", "(1.6–3.2)%% chance to drop an additional Beyond Scarab", "Minion"),
            ("#% chance to drop an additional Torment Scarab", "(1.6–3.2)%% chance to drop an additional Torment Scarab", "Minion"),
            ("#% chance to drop an additional Ambush Scarab", "(1.6–3.2)%% chance to drop an additional Ambush Scarab", "Minion"),
            ("#% chance to drop an additional Domination Scarab", "(1.6–3.2)%% chance to drop an additional Domination Scarab", "Minion"),
            ("#% chance to drop an additional Essence Scarab", "(1.6–3.2)%% chance to drop an additional Essence Scarab", "Minion"),
            ("#% chance to drop an additional Reliquary Scarab", "(1.6–3.2)%% chance to drop an additional Reliquary Scarab", "Minion"),
            ("#% to Cold Resistance", "(-60–-40)%% to Cold Resistance", "Player"),
            ("#% to Lightning Resistance", "(-60–-40)%% to Lightning Resistance", "Player"),
            ("#% additional Physical Damage Reduction", "(-60–-40)%% additional Physical Damage Reduction", "Player"),
            ("#% reduced Defences per Frenzy Charge", "(30–50)%% reduced Defences per Frenzy Charge", "Player"),
            ("#% reduced Recovery Rate of Life, Mana and Energy Shield per Endurance Charge", "(10–20)%% reduced Recovery Rate of Life, Mana and Energy Shield per Endurance Charge", "Player"),
            ("#% to Critical Strike Multiplier per Power Charge", "(-40–-20)%% to Critical Strike Multiplier per Power Charge", "Player"),
            ("#% chance for Enemies to drop Chilled Ground when Hitting you, no more than once every 2 seconds", "(25–35)%% chance for Enemies to drop Chilled Ground when Hitting you, no more than once every 2 seconds", "Player"),
            ("#% chance for Enemies to drop Shocked Ground when Hitting you, no more than once every 2 seconds", "(25–35)%% chance for Enemies to drop Shocked Ground when Hitting you, no more than once every 2 seconds", "Player"),
            ("All Damage taken from Hits can Sap you", "All Damage taken from Hits can Sap you", "Player"),
            ("#% chance to be Sapped when Hit", "(25–35)%% chance to be Sapped when Hit", "Player"),
            ("Nearby Enemies Gain #% of their Physical Damage as Extra Cold Damage", "Nearby Enemies Gain 100%% of their Physical Damage as Extra Cold Damage", "Player"),
            ("Nearby Enemies Gain #% of their Physical Damage as Extra Lightning Damage", "Nearby Enemies Gain 100%% of their Physical Damage as Extra Lightning Damage", "Player"),
            ("Projectiles are fired in random directions", "Projectiles are fired in random directions", "Player"),
            ("Spell Hits have #% chance to Hinder you", "Spell Hits have (25–35)%% chance to Hinder you", "Player"),
            ("Non-Damaging Ailments you inflict are reflected back to you", "Non-Damaging Ailments you inflict are reflected back to you", "Player"),
            ("number of grasping vines to gain every second while stationary [#]", "number of grasping vines to gain every second while stationary [1]", "Player"),
            ("#% to Fire Resistance", "(-60–-40)%% to Fire Resistance", "Player"),
            ("#% to Chaos Resistance", "(-60–-40)%% to Chaos Resistance", "Player"),
            ("-# to Armour", "-3000 to Armour", "Player"),
            ("-# to Evasion Rating", "-3000 to Evasion Rating", "Player"),
            ("#% increased Flask Charges used", "(20–40)%% increased Flask Charges used", "Player"),
            ("#% reduced Flask Effect Duration", "(40–60)%% reduced Flask Effect Duration", "Player"),
            ("Take # Chaos Damage per second during any Flask Effect", "Take 600 Chaos Damage per second during any Flask Effect", "Player"),
            ("Spell Hits have #% chance to Hinder you", "Spell Hits have (20–30)%% chance to Hinder you", "Player"),
            ("All Damage taken from Hits can Scorch you", "All Damage taken from Hits can Scorch you", "Player"),
            ("#% chance to be Scorched when Hit", "(25–35)%% chance to be Scorched when Hit", "Player"),
            ("Curses you inflict are reflected back to you", "Curses you inflict are reflected back to you", "Player"),
            ("#% chance for Enemies to drop Burning Ground when Hitting you, no more than once every 2 seconds", "(15–20)%% chance for Enemies to drop Burning Ground when Hitting you, no more than once every 2 seconds", "Player"),
            ("#% chance to be targeted by a Meteor when you use a Flask", "30%% chance to be targeted by a Meteor when you use a Flask", "Player"),
            ("Nearby Enemies Gain #% of their Physical Damage as Extra Fire Damage", "Nearby Enemies Gain 100%% of their Physical Damage as Extra Fire Damage", "Player"),
            ("Nearby Enemies Gain #% of their Physical Damage as Extra Chaos Damage", "Nearby Enemies Gain 100%% of their Physical Damage as Extra Chaos Damage", "Player"),
            ("Basic Currency Items dropped by slain Enemies have #% chance to be Duplicated", "Basic Currency Items dropped by slain Enemies have (15–30)%% chance to be Duplicated", "Player"),
            ("Gems dropped by slain Enemies have #% chance to be Duplicated", "Gems dropped by slain Enemies have (15–30)%% chance to be Duplicated", "Player"),
            ("Unique Items dropped by slain Enemies have #% chance to be Duplicated", "Unique Items dropped by slain Enemies have (15–30)%% chance to be Duplicated", "Player"),
            ("Scarabs dropped by slain Enemies have #% chance to be Duplicated", "Scarabs dropped by slain Enemies have (15–30)%% chance to be Duplicated", "Player"),
            ("Maps dropped by slain Enemies have #% chance to be Duplicated", "Maps dropped by slain Enemies have (15–30)%% chance to be Duplicated", "Player"),
            ("Divination Cards dropped by slain Enemies have #% chance to be Duplicated", "Divination Cards dropped by slain Enemies have (15–30)%% chance to be Duplicated", "Player"),
            ("#% increased Quantity of Items found in this Area", "(10–20)%% increased Quantity of Items found in this Area", "Player"),
            ("#% increased Rarity of Items found in this Area", "(15–35)%% increased Rarity of Items found in this Area", "Player"),
            ("Basic Currency Items dropped by slain Enemies have #% chance to be Duplicated", "Basic Currency Items dropped by slain Enemies have (10–15)%% chance to be Duplicated", "Player"),
            ("Gems dropped by slain Enemies have #% chance to be Duplicated", "Gems dropped by slain Enemies have (10–15)%% chance to be Duplicated", "Player"),
            ("Maps dropped by slain Enemies have #% chance to be Duplicated", "Maps dropped by slain Enemies have (10–15)%% chance to be Duplicated", "Player"),
            ("#% increased Quantity of Items found in this Area", "(10–15)%% increased Quantity of Items found in this Area", "Player"),
            ("#% increased Rarity of Items found in this Area", "(5–10)%% increased Rarity of Items found in this Area", "Player"),
            ("#% increased Experience gain", "(8–12)%% increased Experience gain", "Player"),

        };





    }

    public enum AffectedTarget
    {
        Any = 0,
        Player = 1,
        Minions = 2,
        FinalBoss = 3,
    }
    public enum EffectType
    {
        Neutral,
        Upside,
        Downside,
    }



}