namespace Pathfinder.CharacterBuilder

open FSharp.Data.JsonProvider
//open FSharp.Data
//open FSharp.Data.Runtime

module PathfinderApi =
    type Action = JsonProvider<Sample="action.json", RootTypeName="Action", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.action.json">
    type Ancestry = JsonProvider<Sample="ancestry.json", RootTypeName="Ancestry", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.ancestry.json">
    type AncestryFeature = JsonProvider<Sample="ancestryFeature.json", RootTypeName="AncestryFeature", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.ancestryFeature.json">
    type Archetype = JsonProvider<Sample="archetype.json", RootTypeName="Archetype", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.archetype.json">
    type Background = JsonProvider<Sample="background.json", RootTypeName="Background", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.background.json">
    type Class = JsonProvider<Sample="class.json", RootTypeName="Class", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.class.json">
    type ClassFeature = JsonProvider<Sample="classFeature.json", RootTypeName="ClassFeature", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.classFeature.json">
    type Deity = JsonProvider<Sample="deity.json", RootTypeName="Deity", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.deity.json">
    type Equipment = JsonProvider<Sample="equipment.json", RootTypeName="Equipment", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.equipment.json">
    type Feat = JsonProvider<Sample="feat.json", RootTypeName="Feat", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.feat.json">
    type Spell = JsonProvider<Sample="spell.json", RootTypeName="Spell", EmbeddedResource="Pathfinder.CharacterBuilder, Pathfinder.CharacterBuilder.spell.json">

    let Actions = Action.GetSample()
    let Ancestries = Ancestry.GetSample()
    let AncestryFeatures = AncestryFeature.GetSample()
    let Archetypes = Archetype.GetSample()
    let Backgrounds = Background.GetSample()
    let Classes = Class.GetSample()
    let ClassFeatures = ClassFeature.GetSample()
    let Deities = Deity.GetSample()
    let Equipment = Equipment.GetSample()
    let Feats = Feat.GetSample()
    let Spells = Spell.GetSample()
