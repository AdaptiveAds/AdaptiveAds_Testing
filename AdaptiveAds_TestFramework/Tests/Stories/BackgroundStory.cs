using AdaptiveAds_TestFramework;
using AdaptiveAds_TestFramework.Helpers;
using AdaptiveAds_TestFramework.PageFrameworks;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Tests.Stories
{
    [TestFixture]
    [Story(AsA = "As a user",
            IWant = "I want a backgrounds page",
            SoThat = "So that I can manage backgrounds in the system.")]
    public class BackgroundStory
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
                Driver.GoTo(Location.PageBackgrounds, true, false));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }

        [TearDown]
        public void Clean()
        {
            Driver.GoTo(Location.PageBackgrounds, true, false);

            BackgroundsPage.Remove("BackgroundAdd", true);
            BackgroundsPage.Remove("BackgroundEdit", true);
            BackgroundsPage.Remove("BackgroundEdit_Edited", true);
            BackgroundsPage.Remove("BackgroundRemove", true);
            BackgroundsPage.Remove("BackgroundRelevant", true);
            BackgroundsPage.Remove("BackgroundNonRelevant", true);
            BackgroundsPage.Remove("BackgroundReShownAfterSearch", true);
            BackgroundsPage.Remove("BackgroundReShownAfterFilter", true);
        }

        #endregion

        [Test]
        public void BackgroundsControl_AddABackground_SystemContainsNewBackground()
        {
            this.Given(x => Driver.IsAt(Location.PageBackgrounds), "Given I am at the Backgrounds page.")
                .When(x => BackgroundsPage.Add("BackgroundAdd", false), "When I add an item.")
                .Then(x => BackgroundsPage.Contains("BackgroundAdd", true), "Then it is added to the system.")
                .BDDfy<BackgroundStory>();
        }

        [Test]
        public void BackgroundsControl_EditBackgroundName_UpdateIsAppliedToTheSystem()
        {
            this.Given(x => Driver.IsAt(Location.PageBackgrounds), "Given I am at the Backgrounds page.")
                .And(x => BackgroundsPage.Add("BackgroundEdit", true), "And the background \"BackgroundEdit\" exists.")
                .When(x => BackgroundsPage.Edit("BackgroundEdit"), "When I edit its name.")
                .Then(x => BackgroundsPage.Contains("BackgroundEdit_Edited", true), "Then it is updated in the system.")
                .BDDfy<BackgroundStory>();
        }

        [Test]
        public void BackgroundsControl_DeleteBackground_BackgroundIsRemovedFromTheSystem()
        {
            this.Given(x => Driver.IsAt(Location.PageBackgrounds), "Given I am at the Backgrounds page.")
                .And(x => BackgroundsPage.Add("BackgroundRemove", true), "And the background \"BackgroundRemove\" exists.")
                .When(x => BackgroundsPage.Remove("BackgroundRemove", false), "When I remove an item.")
                .Then(x => BackgroundsPage.Contains("BackgroundRemove", false), "Then it is no longer in the system.")
                .BDDfy<BackgroundStory>();
        }
        
        [Test]
        public void BackgroundsSearch_ApplySearchCriteria_ReleventItemsShownAndNonRelevantItemsRemoved()
        {
            this.Given(x => Driver.IsAt(Location.PageBackgrounds), "Given I am at the Backgrounds page.")
                .And(x => BackgroundsPage.Add("BackgroundRelevant", true), "And the background \"BackgroundRelevant\" exists.")
                .And(x => BackgroundsPage.Add("BackgroundNonRelevant", true), "And the background \"BackgroundNonRelevant\" exists.")
                .When(x => BackgroundsPage.Search("BackgroundRelevant"), "When I search \"BackgroundRelevant\".")
                .Then(x => BackgroundsPage.Contains("BackgroundRelevant", true), "Then the background \"BackgroundRelevant\" is shown.")
                .And(x => BackgroundsPage.Contains("BackgroundNonRelevant", false), "And the background \"BackgroundNonRelevant\" is not shown.")
                .BDDfy<BackgroundStory>();
        }

        [Test]
        public void BackgroundsSearch_SearchCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.PageBackgrounds), "Given I am at the Backgrounds page.")
                .And(x => BackgroundsPage.Add("BackgroundReShownAfterSearch", true), "And the background \"BackgroundReShownAfterSearch\" exists.")
                .And(x => BackgroundsPage.Search("BackgroundOther"), "And I search the name of another item.")
                .And(x => BackgroundsPage.Contains("BackgroundReShownAfterSearch", false), "And the background is no longer shown.")
                .When(x => BackgroundsPage.ClearSearch(), "When I clear the search Criteria.")
                .Then(x => BackgroundsPage.Contains("BackgroundReShownAfterSearch", true), "Then the background is shown.")
                .BDDfy<BackgroundStory>();
        }

        [Test]
        public void BackgroundsSearch_FilterCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.PageBackgrounds), "Given I am at the Backgrounds page.")
                .And(x => BackgroundsPage.Add("BackgroundReShownAfterFilter", true), "And the background \"BackgroundReShownAfterFilter\" exists.")
                .And(x => BackgroundsPage.Search("BackgroundOther"), "And I search the name of another item.")
                .And(x => BackgroundsPage.Contains("BackgroundReShownAfterFilter", false), "And the background is no longer shown.")
                .When(x => BackgroundsPage.ClearFilter(), "When I clear the filter.")
                .Then(x => BackgroundsPage.Contains("BackgroundReShownAfterFilter", true), "Then the background is shown.")
                .BDDfy<BackgroundStory>();
        }
    }
}
