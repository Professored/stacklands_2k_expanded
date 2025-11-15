using System;
using System.Collections.Generic;
using System.Text;

namespace Stacklands2KExpanded
{
    public class Idea
    {
        public readonly string Name;
        public readonly BlueprintGroup Group;
        public readonly List<Subprint> Subprints;
        public readonly bool NeedsExactMatch;

        public Idea(
            string name,
            BlueprintGroup group,
            List<Subprint> subprints,
            bool needsExactMatch = true
        )
        {
            Name = name;
            Group = group;
            Subprints = subprints;
            NeedsExactMatch = needsExactMatch;
        }
    }
}
