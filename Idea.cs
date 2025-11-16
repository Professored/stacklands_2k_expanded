using System;
using System.Collections.Generic;
using System.Text;

namespace Stacklands2KExpanded
{
    public class Idea
    {
        public readonly string Id;
        public readonly string Name;
        public readonly BlueprintGroup Group;
        public readonly List<Subprint> Subprints;
        public readonly bool NeedsExactMatch;
        public readonly string Description;
        public readonly string ResultDescription;

        public Idea(
            string id,
            string name,
            string description,
            string resultDescription,
            BlueprintGroup group,
            List<Subprint> subprints,
            bool needsExactMatch = true
        )
        {
            Id = id;
            Name = name;
            Description = description;
            ResultDescription = resultDescription;
            Group = group;
            Subprints = subprints;
            NeedsExactMatch = needsExactMatch;
        }
    }
}
