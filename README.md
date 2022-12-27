# AltarHelper
Highlights the best option from Primordial Altar based on a Filter.
The plugin use a system of Weight to calculate the bes option.
The pattern of file is Mod|Weight|Choice


How to use:

1. Start Plugin.  
2. Open "Filter.txt" File located in the Pluginfolder  
3. Copy Paste the mod name  (can be found here  "http://www.vhpg.com/primordial-altar/")
4. Use | to separete the Mod name from Weight and Choice
5. In PluginMenu Press "Refresh File"

P.S Also u can use Thanos#6505 Sheet to make your filter

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
