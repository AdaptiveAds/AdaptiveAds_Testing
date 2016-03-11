﻿using AdaptiveAds_TestFramework;
using AdaptiveAds_TestFramework.Helpers;
using AdaptiveAds_TestFramework.PageFrameworks;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Tests.Stories
{
    [TestFixture]
    [Story(AsA = "As user",
            IWant = "I want a dashboard with links to key tasks",
            SoThat = "So that I can quickly navigate the system.")]
    public class DashboardStory
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
                Driver.GoTo(Location.Dashboard, true));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }

        #endregion

        [Test]
        public void UserCanNavigateToTheAdvertsPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Adverts), "When I select the adverts link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Adverts), "I should be at the Adverts page.")
                .BDDfy<DashboardStory>();
        }

        [Test]
        public void UserCanNavigateToThePlaylistsPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Playlists), "When I select the playlists link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Playlists), "I should be at the Playlists page.")
                .BDDfy<DashboardStory>();
        }
    }
}
