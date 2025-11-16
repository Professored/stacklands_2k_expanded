using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace Stacklands2KExpanded
{
    static class CardLoader
    {
        private static GameObject cardContainer;

        public static List<SokTerm> Translations = new List<SokTerm>();

        public static void AddTranslation(string id, string text)
        {
            Translations.Add(new SokTerm(SokLoc.FallbackSet, id.ToLower(), text));
        }

        public static void AddCards(List<CardData> allCards)
        {
            //General top-level handler for exception handling on cards - mostly for debugging.
            var log = Plugin.StaticLogger;

            cardContainer = new GameObject("CardContainer - CompactStorage");
            cardContainer.gameObject.SetActive(false);

            foreach (var c in CardDefs.Cards)
            {
                try
                {
                    log.Log("Adding card: " + c.Id);
                    var go = new GameObject(c.Id);
                    go.transform.SetParent(cardContainer.transform);
                    var card = (CardData)go.AddComponent(c.ScriptType);
                    card.Id = c.Id;
                    card.NameTerm = c.Id;
                    log.Log($"Adding translation: {card.NameTerm}::{c.Name}");
                    AddTranslation(card.NameTerm, c.Name);
                    card.DescriptionTerm = c.Id + "_desc";
                    AddTranslation(card.DescriptionTerm, c.Description);
                    var path = Path.Combine(Plugin.Instance.Path, "icons", c.Id + ".raw");
                    if (File.Exists(path))
                    {
                        var tex = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
                        
                        card.Icon = Sprite.Create(tex, new Rect(0, 0, 1024, 1024), new Vector2(0.5f, 0.5f));
                    }
                    card.Value = c.Value;
                    card.MyCardType = c.CardType;
                    card.IsBuilding = c.IsBuilding;
                    card.CardUpdateType = CardUpdateType.Mod;
                    if (c.CardType == CardType.Structures)
                    {
                        card.PickupSoundGroup = PickupSoundGroup.Heavy;
                    }
                    if (c.Init != null)
                        c.Init(card);
                    if (c.CustomColour != null)
                    {
                        card.MyPalette = c.CustomColour;
                    }
                    if (c.Connections != null)
                    {
                        card.EnergyConnectors = c.Connections;
                    }
                    allCards.Add(card);
                }
                catch (Exception e)
                {
                    log.Log("Error adding card: " + e);
                    throw;
                }
            }

            foreach (var idea in CardDefs.Ideas)
            {
                var id = idea.Id;
                try
                {
                    log.Log("Adding idea card: " + idea.Name);
                    var go = new GameObject(id);
                    go.transform.SetParent(cardContainer.transform);
                    var card = go.AddComponent<Blueprint>();
                    card.Id = id;
                    card.NameTerm = idea.Id;
                    card.DescriptionTerm = idea.Id + "_desc";
                    card.ResultDescriptionTerm = idea.Id + "_result_desc";
                    AddTranslation(id, idea.Name);
                    AddTranslation(card.DescriptionTerm, idea.Description);
                    card.MyCardType = CardType.Ideas;
                    card.BlueprintGroup = idea.Group;
                    card.CombineResultCards = true;
                    card.NeedsExactMatch = idea.NeedsExactMatch;
                    card.CardUpdateType = CardUpdateType.Mod;
                    foreach (var sub in card.Subprints)
                    {
                        var term = idea.Id + "_status_desc";
                        AddTranslation(term, sub.StatusTerm);
                        sub.StatusTerm = term;
                        card.Subprints.Add(sub);
                    }
                    allCards.Add(card);
                }
                catch (Exception e)
                {
                    log.LogException($"Error adding idea card ({id}): {e}");
                    throw;
                }
            }            
        }
    }
}
