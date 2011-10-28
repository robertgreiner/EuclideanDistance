using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EuclideanDistance.Reviews;
using NUnit.Framework;

namespace EuclideanDistance.Tests
{
    [TestFixture]
    class EuclideanDistanceTest
    {
        private EuclideanDistance euclideanDistance;

        [SetUp]
        public void SetUp()
        {
            euclideanDistance = new EuclideanDistance(ReviewerBuilder.BuildReviewer1(), ReviewerBuilder.BuildReviewer2());
        }

        [Test]
        public void ReviewersThatHaveReviewedUniqueTitlesShouldNotBeSimilar()
        {
            var r1 = ReviewerBuilder.BuildReviewer1();
            var r2 = ReviewerBuilder.BuildAReviewerThatReviewedSomethingUnique();
            euclideanDistance = new EuclideanDistance(r1, r2);
            Assert.AreEqual(0, euclideanDistance.Score());
        }

        [Test]
        public void ReviewersThatHaveTheSameTasteShouldHaveAPerfectScore()
        {
            var r1 = ReviewerBuilder.BuildReviewer1();
            euclideanDistance = new EuclideanDistance(r1, r1);
            Assert.AreEqual(1.0, euclideanDistance.Score());
        }

        [Test]
        public void TwoReviewersWithSomeSimilarReviewsShouldHaveAScoreBetweenZeroAndOne()
        {
            Assert.AreEqual(0.148, euclideanDistance.Score());
        }

        [Test]
        public void ReviewersThatHaveTheSameTasteShouldHaveAllSimilarItems()
        {
            var r1 = ReviewerBuilder.BuildReviewer1();
            euclideanDistance = new EuclideanDistance(r1, r1);
            Assert.AreEqual(6, euclideanDistance.FindSharedItems().Count);
        }

        [Test]
        public void ShouldHaveASingleMaximumReview()
        {
            var max = ReviewerBuilder.BuildOneReviewMax();
            Assert.AreEqual(5, max.Reviews.First().Value);
        }

        [Test]
        public void TwoSimilarReviewersShouldHaveOneLikeReview()
        {
            var r1 = ReviewerBuilder.BuildOneReviewMax();
            var r2 = ReviewerBuilder.BuildOneReviewMin();

            euclideanDistance = new EuclideanDistance(r1, r2);

            Assert.IsNotEmpty(euclideanDistance.FindSharedItems());
        }
    }
}
