using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
            var log = Plugin.StaticLogger;

            cardContainer = new GameObject("CardContainer - CompactStorage");
            cardContainer.gameObject.SetActive(false);

            foreach (var c in CardDefs.Cards)
            {
                log.Log("Adding card: " + c.Id);
                var go = new GameObject(c.Id);
                go.transform.SetParent(cardContainer.transform);
                var card = (CardData)go.AddComponent(c.ScriptType);
                card.Id = c.Id;
                card.NameTerm = c.Id;
                AddTranslation(card.NameTerm, c.Name);
                card.DescriptionTerm = c.Id + "_desc";
                AddTranslation(card.DescriptionTerm, c.Description);
                var path = Path.Combine(Plugin.Instance.Path, "icons", c.Id + ".png");
                if (File.Exists(path))
                {
                    var tex = new Texture2D(1024, 1024, TextureFormat.RGBA32, false);
                    //tex.LoadImage(File.ReadAllBytes(path));
                    tex.LoadRawTextureData(File.ReadAllBytes(path));
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

            foreach (var idea in CardDefs.Ideas)
            {
                var id = Consts.Idea(idea.Name);
                var go = new GameObject(id);
                go.transform.SetParent(cardContainer.transform);
                var card = go.AddComponent<Blueprint>();
                card.Id = id;
                card.NameTerm = idea.Name;
                card.MyCardType = CardType.Ideas;
                card.BlueprintGroup = idea.Group;
                card.Subprints = idea.Subprints;
                card.NeedsExactMatch = idea.NeedsExactMatch;
                card.CardUpdateType = CardUpdateType.Mod;
                foreach (var sub in card.Subprints)
                {
                    var term = Consts.PREFIX + sub.StatusTerm.Replace(" ", "").ToLower();
                    AddTranslation(term, sub.StatusTerm);
                    sub.StatusTerm = term;
                }
                allCards.Add(card);
                
            }
        }
    }
}
