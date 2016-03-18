using AdaptiveAds_TestFramework;
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

        [Test]
        public void UserCanNavigateToTheLocationsPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Locations), "When I select the locations link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Locations), "I should be at the Locations page.")
                .BDDfy<DashboardStory>();
        }

        [Test]
        public void UserCanNavigateToTheDepartmentsPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Departments), "When I select the departments link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Departments), "I should be at the Departments page.")
                .BDDfy<DashboardStory>();
        }

        [Test]
        public void UserCanNavigateToTheScreensPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Screens), "When I select the screens link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Screens), "I should be at the Screens page.")
                .BDDfy<DashboardStory>();
        }

        [Test]
        public void UserCanNavigateToTheUsersPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Users), "When I select the users link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Users), "I should be at the Users page.")
                .BDDfy<DashboardStory>();
        }

        [Test]
        public void UserCanNavigateToTheTemplatesPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Templates), "When I select the templates link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Templates), "I should be at the Templates page.")
                .BDDfy<DashboardStory>();
        }

        [Test]
        public void UserCanNavigateToTheSkinsPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Skins), "When I select the skins link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Skins), "I should be at the Skins page.")
                .BDDfy<DashboardStory>();
        }

        [Test]
        public void UserCanNavigateToThePrivilegesPageFromTheDashboard()
        {
            this.Given(x => Driver.IsAt(Location.Dashboard), "Given I am at the Dashboard.")
                .When(x => DashboardPage.Select(DashboardLink.Privileges), "When I select the privileges link.")
                .Then(x => Driver.IsNotAt(Location.Dashboard), "Then I should no longer be at the Dashboard page.")
                .And(x => Driver.IsAt(Location.Privileges), "I should be at the Privileges page.")
                .BDDfy<DashboardStory>();
        }
    }
}
