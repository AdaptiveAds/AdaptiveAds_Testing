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
                Driver.GoTo(Location.Departments, true));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }

        [TearDown]
        public void Clean()
        {
            Driver.GoTo(Location.Departments,true,false);

            DepartmentsPage.Remove("TestDepartmentAdd", true);
            DepartmentsPage.Remove("TestDepartmentEdit", true);
            DepartmentsPage.Remove("TestDepartmentEdit_Edited", true);
            DepartmentsPage.Remove("TestDepartmentRemove", true);
            DepartmentsPage.Remove("TestDepartmentNonRelevant", true);
            DepartmentsPage.Remove("TestDepartmentSearch", true);
            DepartmentsPage.Remove("TestDepartmentReShown", true);
        }

        #endregion

        [Test]
        public void UserCanAddDepartments()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .When(x => DepartmentsPage.Add("TestDepartmentAdd",false), "When I add an item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentAdd", true), "Then it is added to the system.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void UserCanEditDepartments()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x=> DepartmentsPage.Add("TestDepartmentEdit", true), "And the department \"TestDepartmentEdit\" exists.")
                .When(x => DepartmentsPage.Edit("TestDepartmentEdit"), "When I edit an item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentEdit_Edited", true), "Then it is updated in the system.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void UserCanRemoveDepartments()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentRemove", true), "And the department \"TestDepartmentRemove\" exists.")
                .When(x => DepartmentsPage.Remove("TestDepartmentRemove", false), "When I remove an item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentRemove", false), "Then it is no longer in the system.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void DepartmentsSearch_NonRelevantItemsRemovedFromResults()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentNonRelevant", true), "And the department \"TestDepartmentNonRelevant\" exists.")
                .When(x => DepartmentsPage.Search("TestDepartmentOther"), "When I search the name of another item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentNonRelevant", false), "Then the original department is no longer shown.")
                .BDDfy<DepartmentStory>();
        }
        
        [Test]
        public void DepartmentsSearch_ItemsShownInResults()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentSearch", true), "And the department \"TestDepartmentSearch\" exists.")
                .When(x => DepartmentsPage.Search("TestDepartmentSearch"), "When I search the name of the item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentSearch", true), "Then the department is shown.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void DepartmentsSearch_NonRelevantItemsReShownWhenSearchCleared()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentReShown", true), "And the department \"TestDepartmentReShown\" exists.")
                .And(x => DepartmentsPage.Search("TestDepartmentOther"), "And I search the name of another item.")
                .And(x => DepartmentsPage.Contains("TestDepartmentReShown", false), "And the department is no longer shown.")
                .When(x=> DepartmentsPage.ClearSearch(),"When I clear the search Criteria.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentReShown", true), "Then the department is shown.")
                .BDDfy<DepartmentStory>();
        }
    }
}
