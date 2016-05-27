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

            Driver.GoTo(Location.Departments, true, false);
            DepartmentsPage.Remove("TestDepartmentForAdverts", true);
            DepartmentsPage.Remove("TestDepartmentForAdverts1", true);
            DepartmentsPage.Remove("TestDepartmentForAdverts2", true);
        }

        #endregion

        [Test]
        public void UserCanAddAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .When(x => AdvertsPage.Add("TestAdvertAdd", "", false), "When I add an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertAdd", true), "Then it is added to the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void UserCanEditAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertEdit", "", true), "And the advert \"TestAdvertEdit\" exists.")
                .When(x => AdvertsPage.EditName("TestAdvertEdit"), "When I edit an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertEdit_Edited", true), "Then it is updated in the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void UserCanRemoveAdverts()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertRemove", "", true), "And the advert \"TestAdvertRemove\" exists.")
                .When(x => AdvertsPage.Remove("TestAdvertRemove", false), "When I remove an item.")
                .Then(x => AdvertsPage.Contains("TestAdvertRemove", false), "Then it is no longer in the system.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AddAdvert_SpecifyDepartment_AdvertIsAddedToSpecifiedDepartment()
        {
            this.Given(x => Driver.GoTo(Location.Departments, true, true), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentForAdverts", false), "And I add a new test department.")
                .And(x => DepartmentsPage.Contains("TestDepartmentForAdverts", true), "And it is successfully added to the system.")
                .When(x => Driver.GoTo(Location.Adverts, true, true), "When I go to the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertDepartment", "TestDepartmentForAdverts", false), "And I add an item specifying the department.")
                .Then(x => AdvertsPage.Contains("TestAdvertDepartment", true), "Then it is added to the system.")
                .And(x => AdvertsPage.AdvertIsAssignedToDepartment("TestAdvertDepartment", "TestDepartmentForAdverts"), "And it is added to the correct department.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertDepartment_EditDepartment_AdvertDepartmentUpdated()
        {
            this.Given(x => Driver.GoTo(Location.Departments, true, true), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentForAdverts1", false), "And I add a new test department.")
                .And(x => DepartmentsPage.Contains("TestDepartmentForAdverts1", true), "And it is successfully added to the system.")
                .And(x => DepartmentsPage.Add("TestDepartmentForAdverts2", false), "And I add another.")
                .And(x => DepartmentsPage.Contains("TestDepartmentForAdverts2", true), "And it is also successfully added to the system.")
                .And(x => Driver.GoTo(Location.Adverts, true, true), "And I go to the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertDepartment", "TestDepartmentForAdverts1", false), "And I add an item specifying the first department.")
                .And(x => AdvertsPage.Contains("TestAdvertDepartment", true), "And it is added to the system.")
                .And(x => AdvertsPage.AdvertIsAssignedToDepartment("TestAdvertDepartment", "TestDepartmentForAdverts1"), "And it is added to the correct department.")
                .When(x=>AdvertsPage.EditAdvertDepartment("TestAdvertDepartment", "TestDepartmentForAdverts2"),"When I edit the department of the advert to the second department.")
                .Then(x => AdvertsPage.AdvertIsAssignedToDepartment("TestAdvertDepartment", "TestDepartmentForAdverts2"), "Then it is updated to the correct department.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertsSearch_ApplySearchCriteria_ReleventItemsShownAndNonRelevantItemsRemoved()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertRelevant", "", true), "And the advert \"TestAdvertRelevant\" exists.")
                .And(x => AdvertsPage.Add("TestAdvertNonRelevant", "", true), "And the advert \"TestAdvertNonRelevant\" exists.")
                .When(x => AdvertsPage.Search("TestAdvertRelevant"), "When I search \"TestAdvertRelevant\".")
                .Then(x => AdvertsPage.Contains("TestAdvertRelevant", true), "Then the advert \"TestAdvertRelevant\" is shown.")
                .And(x => AdvertsPage.Contains("TestAdvertNonRelevant", false), "And the advert \"TestAdvertNonRelevant\" is not shown.")
                .BDDfy<AdvertStory>();
        }

        [Test]
        public void AdvertsSearch_SearchCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Adverts), "Given I am at the Adverts page.")
                .And(x => AdvertsPage.Add("TestAdvertReShownAfterSearch", "", true), "And the advert \"TestAdvertReShownAfterSearch\" exists.")
                .And(x => AdvertsPage.Add("TestAdvertOther", "", true), "And the advert \"TestAdvertOther\" exists.")
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
                .And(x => AdvertsPage.Add("TestAdvertReShownAfterFilter", "", true), "And the advert \"TestAdvertReShownAfterFilter\" exists.")
                .And(x => AdvertsPage.Add("TestAdvertOther", "", true), "And the advert \"TestAdvertOther\" exists.")
                .And(x => AdvertsPage.Search("TestAdvertOther"), "And I search the name of another item.")
                .And(x => AdvertsPage.Contains("TestAdvertReShownAfterFilter", false), "And the advert is no longer shown.")
                .When(x => AdvertsPage.ClearFilter(), "When I clear the filter.")
                .Then(x => AdvertsPage.Contains("TestAdvertReShownAfterFilter", true), "Then the advert is shown.")
                .BDDfy<AdvertStory>();
        }
    }
}
