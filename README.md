# LibShadowbane

This is some junk I half wrote before joining the SBEmu team and is deprecated in favor of [LibTheorybane](https://github.com/SBEmu/LibTheorybane)

## Issues

- Architecture
- Stat system
  - Revamp stats to have a stat and statmultiplier class that is reused between runes, skills, etc.
  - Add resistances
  - Add focus bonuses
  - Fix defense for armor
- Rune system
  - Add rune system
- Skill system
  - Add focus bonuses
  - Improve skill addition API
- Equipment system
  - Add requirement checking
- Effects system
  - Add powers
  - Add conc pot


## Character stat tool

Problems to solve:

- how much def can my build achieve?
- which ring affix is Bis? (def, attack, spell dmg)

### Feature Ideas

- Imperative buffer to go along with runes/discs, e.g. start would subtract 5 from all, apply elf, then apply "tough as nails"
  and require 50 con so add imperative to bump to 50 con on select
- "What if" my character was a fighter or a rogue? imperatives allow change and diff of the approximately the same build

Rune Types:

- Race
- Class
- Profession
- Discipline
- Starting
- Stat
- Mastery

Runes Grant:

- Skills
- Training points per level
- Stat points
- Stat allocations +/-
- Resistance +/-
- HP
- Stamina?
- Defense %
- ATR %

in-game vs. starter, but duplicates of racial start runes shouldn't apply two. Handle by 'appliedrunes' list name match, always prevent dupe

Character

- Runes
- Skills (dynamic from runes? regenerate on rune change?)
- Powers (dynamic from runes? regenerate on rune change?)
- Attribute allocations
- Train allocations into skills
- Train allocations into powers
- Effects (self-cast, others cast, conc)
- Equipment

### Things

- Runes need to know about character (levels) to apply things like the human rune (trains per level)
- Does lucky multiply before, after, or with stance?
- Verify that "melee" buffs apply to ranged

### HP forum Post

[HP Info](https://forums.shadowbaneemulator.com/viewtopic.php?t=1321)

    { ( f(lvl)*CB + g(lvl)*PB ) * ( 0,3 + 0,005*con ) + con + RB } * ( 1 + %focus / 100 * k ) + additional bonuses (e.g. TaN, gear)

    CB = class bonus
    RB = racial bonus
    PB = profession bonus
    For HP: focus = toughness, k = 1/4
    For stam: focus = athletics, k = 1/3

    f(0) = g(0) = 0
    lvl 1 to 9, f(lvl) is incremented by steps of 1 and g(lvl) stays null
    lvl 10 to 19, f(lvl) and g(lvl) are incremented by steps of 1
    lvl 20 to 29, f(lvl) and g(lvl) are incremented by steps of 0.8
    lvl 30 to 39, f(lvl) and g(lvl) are incremented by steps of 0.6
    lvl 40 to 49, f(lvl) and g(lvl) are incremented by steps of 0.4
    lvl 50 to 59, f(lvl) and g(lvl) are incremented by steps of 0.2
    lvl 60 to 75, f(lvl) and g(lvl) are incremented by steps of 0.1

    The values displayed in the stat screen are truncated.

    Note. tireless provides 35 stam and not 30 stam as the description claims.
