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
    [Story(AsA = "As a user",
            IWant = "I want a departments page",
            SoThat = "So that I can manage departments in the system.")]
    public class DepartmentStory
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
                Driver.GoTo(Location.Departments, true, false));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }

        [TearDown]
        public void Clean()
        {
            Driver.GoTo(Location.Departments, true, false);

            DepartmentsPage.Remove("TestDepartmentAdd", true);
            DepartmentsPage.Remove("TestDepartmentEdit", true);
            DepartmentsPage.Remove("TestDepartmentEdit_Edited", true);
            DepartmentsPage.Remove("TestDepartmentRemove", true);
            DepartmentsPage.Remove("TestDepartmentRelevant", true);
            DepartmentsPage.Remove("TestDepartmentNonRelevant", true);
            DepartmentsPage.Remove("TestDepartmentReShownAfterSearch", true);
            DepartmentsPage.Remove("TestDepartmentReShownAfterFilter", true);
        }

        #endregion

        [Test]
        public void DepartmentsControl_AddADepartment_SystemContainsNewDepartment()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .When(x => DepartmentsPage.Add("TestDepartmentAdd", false), "When I add an item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentAdd", true), "Then it is added to the system.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void DepartmentsControl_EditDepartmentName_UpdateIsAppliedToTheSystem()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentEdit", true), "And the department \"TestDepartmentEdit\" exists.")
                .When(x => DepartmentsPage.Edit("TestDepartmentEdit"), "When I edit it's name.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentEdit_Edited", true), "Then it is updated in the system.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void DepartmentsControl_DeleteDepartment_DepartmentIsRemovedFromTheSystem()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentRemove", true), "And the department \"TestDepartmentRemove\" exists.")
                .When(x => DepartmentsPage.Remove("TestDepartmentRemove", false), "When I remove an item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentRemove", false), "Then it is no longer in the system.")
                .BDDfy<DepartmentStory>();
        }
        
        [Test]
        public void DepartmentsSearch_ApplySearchCriteria_ReleventItemsShownAndNonRelevantItemsRemoved()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentRelevant", true), "And the department \"TestDepartmentRelevant\" exists.")
                .And(x => DepartmentsPage.Add("TestDepartmentNonRelevant", true), "And the department \"TestDepartmentNonRelevant\" exists.")
                .When(x => DepartmentsPage.Search("TestDepartmentRelevant"), "When I search \"TestDepartmentRelevant\".")
                .Then(x => DepartmentsPage.Contains("TestDepartmentRelevant", true), "Then the department \"TestDepartmentRelevant\" is shown.")
                .And(x => DepartmentsPage.Contains("TestDepartmentNonRelevant", false), "And the department \"TestDepartmentNonRelevant\" is not shown.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void DepartmentsSearch_SearchCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentReShownAfterSearch", true), "And the department \"TestDepartmentReShownAfterSearch\" exists.")
                .And(x => DepartmentsPage.Search("TestDepartmentOther"), "And I search the name of another item.")
                .And(x => DepartmentsPage.Contains("TestDepartmentReShownAfterSearch", false), "And the department is no longer shown.")
                .When(x => DepartmentsPage.ClearSearch(), "When I clear the search Criteria.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentReShownAfterSearch", true), "Then the department is shown.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void DepartmentsSearch_FilterCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentReShownAfterFilter", true), "And the department \"TestDepartmentReShownAfterFilter\" exists.")
                .And(x => DepartmentsPage.Search("TestDepartmentOther"), "And I search the name of another item.")
                .And(x => DepartmentsPage.Contains("TestDepartmentReShownAfterFilter", false), "And the department is no longer shown.")
                .When(x => DepartmentsPage.ClearFilter(), "When I clear the filter.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentReShownAfterFilter", true), "Then the department is shown.")
                .BDDfy<DepartmentStory>();
        }
    }
}
