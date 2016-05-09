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

        #endregion

        [Test]
        public void UserCanAddDepartments()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .When(x => DepartmentsPage.Add("TestDepartmentAdd",false), "When I add an item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentAdd", true), "Then it is added to the system.")
                .Then(x=> DepartmentsPage.Remove("TestDepartmentAdd", true), "And this test cleans up.")
                .BDDfy<DepartmentStory>();
        }

        [Test]
        public void UserCanEditDepartments()
        {
            this.Given(x => Driver.IsAt(Location.Departments), "Given I am at the Departments page.")
                .And(x=> DepartmentsPage.Add("TestDepartmentEdit", true), "And the department \"TestDepartmentEdit\" exists.")
                .When(x => DepartmentsPage.Edit("TestDepartmentEdit"), "When I edit an item.")
                .Then(x => DepartmentsPage.Contains("TestDepartmentEdit_Edited", true), "Then it is updated in the system.")
                .Then(x => DepartmentsPage.Remove("TestDepartmentEdit_Edited", true), "This test cleans up.")
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
    }
}
