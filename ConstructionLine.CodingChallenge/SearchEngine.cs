using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            if (options?.Colors == null || options.Sizes == null) throw new ArgumentNullException(nameof(options),"Null Exception");

            var shirts = _shirts.Where(shirt =>
                (options.Colors.None() || options.Colors.Any(color => color == shirt.Color)) &&
                (options.Sizes.None() || options.Sizes.Any(size => size == shirt.Size))).ToList();
            
            var colorCounts = (from shirt in shirts
                group shirt by shirt.Color
                into colorGroup
                select new ColorCount {Color = colorGroup.Key, Count = colorGroup.Count()}).ToList();
            colorCounts.AddRange(from c in Color.All
                where colorCounts.None(x => x.Color == c)
                select new ColorCount {Color = c, Count = 0});
            
            var sizeCounts = (from shirt in shirts
                group shirt by shirt.Size
                into sizeGroup
                select new SizeCount {Size = sizeGroup.Key, Count = sizeGroup.Count()}).ToList();
            sizeCounts.AddRange(from s in Size.All
                where sizeCounts.None(x => x.Size == s)
                select new SizeCount {Size = s, Count = 0});
            
            return new SearchResults {Shirts = shirts, ColorCounts = colorCounts, SizeCounts = sizeCounts};
        }
    }
}