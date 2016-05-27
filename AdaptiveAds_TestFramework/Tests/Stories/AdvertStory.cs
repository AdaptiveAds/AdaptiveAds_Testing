using AdaptiveAds_TestFramework;
using AdaptiveAds_TestFramework.Helpers;
using AdaptiveAds_TestFramework.PageFrameworks;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Tests.Stories
{
    [TestFixture]
    [Story(AsA = "As a user",
            IWant = "I want an adverts page",
            SoThat = "So that I can manage adverts in the system.")]
    public class AdvertStory
    {
        #region Initialise and clean up

        [OneTimeSetUp]
        public void Init()
        {
            Driver.Initialise();
            Driver.ActionWait(Period.Medium, () =>
                Driver.GoTo(Location.Departments, true, true));
            DepartmentsPage.Add("TestDepartmentForAdvertTests1", false);
            DepartmentsPage.Add("TestDepartmentForAdvertTests2", false);
            Driver.ActionWait(Period.Medium, () =>
                Driver.GoTo(Location.PageBackgrounds, true, true));
            BackgroundsPage.Add("TestBackgroundAdvert1", false);
            BackgroundsPage.Add("TestBackgroundAdvert2", false);
        }

        [SetUp]
        public void SetUp()
        {
            Driver.ActionWait(Period.Medium, () =>
                Driver.GoTo(Location.Adverts, true, false));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.GoTo(Location.Departments, true, true);
            DepartmentsPage.Remove("TestDepartmentForAdvertTests1", false);
            DepartmentsPage.Remove("TestDepartmentForAdvertTests2", false);
            Driver.GoTo(Location.PageBackgrounds, true, true);
            BackgroundsPage.Remove("TestBackgroundAdvert1", false);
            BackgroundsPage.Remove("TestBackgroundAdvert2", false);
            Driver.Quit();
        }

        [TearDown]
        public void Clean()
        {
            Driver.GoTo(Location.Adverts, true, false);
            AdvertsPage.Remove("TestAdvertDepartment", true);
            AdvertsPage.Remove("TestAdvertOther", true);
            AdvertsPage.Remove("TestAdvertAdd", true);
            AdvertsPage.Remove("TestAdvertEdit", true);
            AdvertsPage.Remove("TestAdvertEdit_Edited", true);
            AdvertsPage.Remove("TestAdvertRemove", true);
            AdvertsPage.Remove("TestAdvertRelevant", true);
            AdvertsPage.Remove("TestAdvertNonRelevant", true);
            AdvertsPage.Remove("TestAdvertReShownAfterSearch", true);
            AdvertsPage.Remove("TestAdvertReShownAfterFilter", true);
        }

        #endregion

        [Test]
        public void UserCanAddAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .When(x => AdvertsPage.Add("TestAdvertAdd", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", false), "When I add an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertAdd", true), "Then it is added to the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void UserCanEditAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertEdit", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", true), "And the advert \"TestAdvertEdit\" exists.")
                .When(x => AdvertsPage.EditName("TestAdvertEdit"), "When I edit an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertEdit_Edited", true), "Then it is updated in the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void UserCanRemoveAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertRemove", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", true), "And the advert \"TestAdvertRemove\" exists.")
                .When(x => AdvertsPage.Remove("TestAdvertRemove", false), "When I remove an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertRemove", false), "Then it is no longer in the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AddAdvert_SpecifyDepartment_AdvertIsAddedToSpecifiedDepartment()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .When(x => AdvertsPage.Add("TestAdvertDepartment", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", false), "When I add an item specifying the department.")
                .Then(x => AdvertsPage.Contains("TestAdvertDepartment", true), "Then it is added to the system.")
                .And(x => AdvertsPage.AdvertIsAssignedToDepartment("TestAdvertDepartment", "TestDepartmentForAdvertTests1"), "And it is added to the correct department.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertDepartment_EditDepartment_AdvertDepartmentUpdated()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertDepartment", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", false), "And I add an item specifying the first department.")
                .And(x => AdvertsPage.Contains("TestAdvertDepartment", true), "And it is added to the system.")
                .And(x => AdvertsPage.AdvertIsAssignedToDepartment("TestAdvertDepartment", "TestDepartmentForAdvertTests1"), "And it is added to the correct department.")
                .When(x=>AdvertsPage.EditAdvertDepartment("TestAdvertDepartment", "TestDepartmentForAdvertTests2"),"When I edit the department of the advert to the second department.")
                .Then(x => AdvertsPage.AdvertIsAssignedToDepartment("TestAdvertDepartment", "TestDepartmentForAdvertTests2"), "Then it is updated to the correct department.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertsSearch_ApplySearchCriteria_ReleventItemsShownAndNonRelevantItemsRemoved()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertRelevant", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", true), "And the advert \"TestAdvertRelevant\" exists.")
                .And(x => AdvertsPage.Add("TestAdvertNonRelevant", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", true), "And the advert \"TestAdvertNonRelevant\" exists.")
                .When(x => AdvertsPage.Search("TestAdvertRelevant"), "When I search \"TestAdvertRelevant\".")
                .Then(x => AdvertsPage.Contains("TestAdvertRelevant", true), "Then the advert \"TestAdvertRelevant\" is shown.")
                .And(x => AdvertsPage.Contains("TestAdvertNonRelevant", false), "And the advert \"TestAdvertNonRelevant\" is not shown.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertsSearch_SearchCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertReShownAfterSearch", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", true), "And the advert \"TestAdvertReShownAfterSearch\" exists.")
                .And(x => AdvertsPage.Add("TestAdvertOther", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", true), "And the advert \"TestAdvertOther\" exists.")
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
                .And(x => AdvertsPage.Add("TestAdvertReShownAfterFilter", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", true), "And the advert \"TestAdvertReShownAfterFilter\" exists.")
                .And(x => AdvertsPage.Add("TestAdvertOther", "TestDepartmentForAdvertTests1", "TestBackgroundAdvert1", true), "And the advert \"TestAdvertOther\" exists.")
                .And(x => AdvertsPage.Search("TestAdvertOther"), "And I search the name of another item.")
                .And(x => AdvertsPage.Contains("TestAdvertReShownAfterFilter", false), "And the advert is no longer shown.")
                .When(x => AdvertsPage.ClearFilter(), "When I clear the filter.")
                .Then(x => AdvertsPage.Contains("TestAdvertReShownAfterFilter", true), "Then the advert is shown.")
                .BDDfy<AdvertStory>();
        }
    }
}
