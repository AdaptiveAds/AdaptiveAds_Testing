using System;
using System.Collections.ObjectModel;
using AdaptiveAds_TestFramework;
using AdaptiveAds_TestFramework.Helpers;
using AdaptiveAds_TestFramework.PageFrameworks;
using NUnit.Framework;
using OpenQA.Selenium;
using TestStack.BDDfy;

namespace Tests.Stories
{
    [TestFixture]
    [Story(AsA = "As user",
            IWant = "I want an adverts page",
            SoThat = "So that I can manage adverts in the system.")]
    public class AdvertStory
    {
        #region Initialise and clean up

        [OneTimeSetUp]
        public void Init()
        {
            Driver.Initialise();
        }

        [SetUp]
        public void SetUp()
        {
            Driver.ActionWait(Period.Medium, () =>
                Driver.GoTo(Location.Adverts, true));
            AdvertsPage.Add("TestAdvertOther", true);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }

        [TearDown]
        public void Clean()
        {
            Driver.GoTo(Location.Adverts,true,false);
            AdvertsPage.Remove("TestAdvertOther", true);

            AdvertsPage.Remove("TestAdvertAdd", true);
            AdvertsPage.Remove("TestAdvertEdit", true);
            AdvertsPage.Remove("TestAdvertEdit_Edited", true);
            AdvertsPage.Remove("TestAdvertRemove", true);
            AdvertsPage.Remove("TestAdvertNonRelevant", true);
            AdvertsPage.Remove("TestAdvertSearch", true);
            AdvertsPage.Remove("TestAdvertReShownAfterSearch", true);
            AdvertsPage.Remove("TestAdvertReShownAfterFilter", true);
        }

        #endregion

        [Test]
        public void UserCanAddAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .When(x => AdvertsPage.Add("TestAdvertAdd",false), "When I add an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertAdd", true), "Then it is added to the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void UserCanEditAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x=> AdvertsPage.Add("TestAdvertEdit", true), "And the advert \"TestAdvertEdit\" exists.")
                .When(x => AdvertsPage.EditName("TestAdvertEdit"), "When I edit an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertEdit_Edited", true), "Then it is updated in the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void UserCanRemoveAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertRemove", true), "And the advert \"TestAdvertRemove\" exists.")
                .When(x => AdvertsPage.Remove("TestAdvertRemove", false), "When I remove an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertRemove", false), "Then it is no longer in the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertsSearch_ApplySearchCriteria_NonRelevantItemsRemovedFromResults()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertNonRelevant", true), "And the advert \"TestAdvertNonRelevant\" exists.")
                .When(x => AdvertsPage.Search("TestAdvertOther"), "When I search the name of another item.")
                .Then(x => AdvertsPage.Contains("TestAdvertNonRelevant", false), "Then the original advert is no longer shown.")
                .BDDfy<AdvertStory>();
        }
        
        [Test]
        public void AdvertsSearch_ApplySearchCriteria_ReleventItemsShownInResults()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertSearch", true), "And the advert \"TestAdvertSearch\" exists.")
                .When(x => AdvertsPage.Search("TestAdvertSearch"), "When I search the name of the item.")
                .Then(x => AdvertsPage.Contains("TestAdvertSearch", true), "Then the advert is shown.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertsSearch_SearchCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertReShownAfterSearch", true), "And the advert \"TestAdvertReShownAfterSearch\" exists.")
                .And(x => AdvertsPage.Search("TestAdvertOther"), "And I search the name of another item.")
                .And(x => AdvertsPage.Contains("TestAdvertReShownAfterSearch", false), "And the advert is no longer shown.")
                .When(x => AdvertsPage.ClearSearch(), "When I clear the search Criteria.")
                .Then(x => AdvertsPage.Contains("TestAdvertReShownAfterSearch", true), "Then the advert is shown.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertsSearch_FilterCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertReShownAfterFilter", true), "And the advert \"TestAdvertReShownAfterFilter\" exists.")
                .And(x => AdvertsPage.Search("TestAdvertOther"), "And I search the name of another item.")
                .And(x => AdvertsPage.Contains("TestAdvertReShownAfterFilter", false), "And the advert is no longer shown.")
                .When(x => AdvertsPage.ClearFilter(), "When I clear the filter.")
                .Then(x => AdvertsPage.Contains("TestAdvertReShownAfterFilter", true), "Then the advert is shown.")
                .BDDfy<AdvertStory>();
        }
    }
}
