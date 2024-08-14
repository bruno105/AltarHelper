# AltarHelper
Highlights the best option from Primordial Altar based on a Filter.
The plugin use a system of Weight to calculate the bes option.

How to use:

1. Start Plugin.  
2. Configure the weight of mods inside plugin settings
3. Have fun.



Special Thanks for #thanos6505 for help me with mods settings and tests




======================04-08-24=====================
* Rewrote the Filter Setting logic
  - Now u dont need a filter.txt, all the configs mods are inside of plugin
  - U can share ur filter with file localized in PoeHelper/configs/globals/AltarHelper_settings.json
* Added a feature to play a alert sound
  - U need a audio file .wav inside PoeHelper/Sounds/

======================26-12=====================
* Rewrote the whole concept of reading mods and regex
 - Now u can use filter strings like:
    "(1.6–3.2)% chance to drop an additional Divine Orb|200000|Minion"  Or
    
    "#% chance to drop an additional Divine Orb|200000|Minion"

* Added a new section for Debugs
 - Now we can debug with more accuracy with new debug mods, Also, u can use Debug Weight to see the mods calculation on ur altar
* Added a section for Filter List
======================21-02=====================
* Added Switch mode:
  - Switch mode = 1 - Calcule Weight of all choices ( Player, Minion and Bosses)
  - Switch mode = 2 - Calcule Weight of only Minions and player choices (will not show bosses choices)
  - Switch mode = 3 - Calcule Weight of only Bosses and player choices (will not show minion choices)
* Added a Slide to add Bonus weight to minion or bosses mod
* Added a hotkey to switch between modes
* Added on git a example of filter.txt with all Upside mods and my particular weight
