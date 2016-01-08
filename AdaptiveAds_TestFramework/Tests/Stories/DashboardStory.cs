using AdaptiveAds_TestFramework;
using AdaptiveAds_TestFramework.PageFrameworks;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Tests.Stories
{
    [TestFixture]
    [Story( AsA="As user",
            IWant="I want a dashboard with links to key tasks",
            SoThat="So that I can quickly navigate the system.")]
    public class DashboardStory
    {
        #region Initialise and clean up

        [SetUp]
        public void Init()
        {
            Driver.Initialise();
        }

        [TearDown]
        public void CleanUp()
        {
            Driver.Close();
        }

        #endregion

        //[Test]
        //public void UserCanNavigateToTheAdvertsPageFromTheDashboard()
        //{
        //    DashboardPage.GoTo();

        //    this.Given(x => Driver.IsAt(Route.Location.Dashboard), "Given I am at the Dashboard.")
        //        .And(x => DashboardPage.HasAdvertsLink(), "And I have the option to navigate to the adverts page")
        //        .When(x => DashboardPage.Select("Adverts"), "When I select the adverts link.")
        //        .Then(x => Driver.IsNotAt(Route.Location.Dashboard), "Then I should no longer be at the Dashboard page.")
        //        .And(x => Driver.IsAt(Route.Location.Adverts), "I should be at the Adverts page.")
        //        .BDDfy<DashboardStory>();
        //}

        //[Test]
        //public void UserCanNavigateToThePlaylistsPageFromTheDashboard()
        //{
        //    DashboardPage.GoTo();

        //    this.Given(x => Driver.IsAt(Route.Location.Dashboard), "Given I am at the Dashboard.")
        //        .And(x => DashboardPage.HasAdvertsLink(), "And I have the option to navigate to the adverts page")
        //        .When(x => DashboardPage.Select("Adverts"), "When I select the adverts link.")
        //        .Then(x => Driver.IsNotAt(Route.Location.Dashboard), "Then I should no longer be at the Dashboard page.")
        //        .And(x => Driver.IsAt(Route.Location.Adverts), "I should be at the Adverts page.")
        //        .BDDfy<DashboardStory>();
        //}
    }
}
