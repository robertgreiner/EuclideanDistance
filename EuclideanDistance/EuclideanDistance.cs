using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EuclideanDistance.Reviews;

namespace EuclideanDistance
{
    public class EuclideanDistance : ISimilarityScore
    {
        private readonly Reviewer CompareTo;
        private readonly Reviewer CompareWith;

        public EuclideanDistance(Reviewer compareTo, Reviewer compareWith)
        {
            CompareTo = compareTo;
            CompareWith = compareWith;
        }

        public double Score()
        {
            var similarTitles = FindSharedItems();
            if (similarTitles.Count == 0)
            {
                return 0.0;
            }
            double sumOfSquares = similarTitles.Sum(title => Math.Pow(CompareTo.Reviews[title] - CompareWith.Reviews[title], 2));
            return Math.Round(1 / (1 + sumOfSquares), 3);
        }

        public List<string> FindSharedItems()
        {
            return (from r in CompareTo.Reviews where CompareWith.Reviews.ContainsKey(r.Key) select r.Key).ToList();
        }
    }
}
