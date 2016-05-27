using AdaptiveAds_TestFramework;
using AdaptiveAds_TestFramework.Helpers;
using AdaptiveAds_TestFramework.PageFrameworks;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Tests.Stories
{
    [TestFixture]
    [Story(AsA = "As a user",
            IWant = "I want a playlists page",
            SoThat = "So that I can manage playlists in the system.")]
    public class PlaylistStory
    {
        #region Initialise and clean up

        [OneTimeSetUp]
        public void Init()
        {
            Driver.Initialise();
            Driver.ActionWait(Period.Medium, () =>
                Driver.GoTo(Location.Departments, true, true));
            DepartmentsPage.Add("TestDepartmentForPlaylistTests1", false);
            DepartmentsPage.Add("TestDepartmentForPlaylistTests2", false);
        }

        [SetUp]
        public void SetUp()
        {
            Driver.ActionWait(Period.Medium, () =>
                Driver.GoTo(Location.Playlists, true, false));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.GoTo(Location.Departments, true, true);
            DepartmentsPage.Remove("TestDepartmentForPlaylistTests1", false);
            DepartmentsPage.Remove("TestDepartmentForPlaylistTests2", false);
            Driver.Quit();
        }

        [TearDown]
        public void Clean()
        {
            Driver.GoTo(Location.Playlists, true, false); 
            PlaylistsPage.Remove("TestPlaylistDepartment", true);
            PlaylistsPage.Remove("TestPlaylistOther", true);
            PlaylistsPage.Remove("TestPlaylistAdd", true);
            PlaylistsPage.Remove("TestPlaylistEdit", true);
            PlaylistsPage.Remove("TestPlaylistEdit_Edited", true);
            PlaylistsPage.Remove("TestPlaylistRemove", true);
            PlaylistsPage.Remove("TestPlaylistRelevant", true);
            PlaylistsPage.Remove("TestPlaylistNonRelevant", true);
            PlaylistsPage.Remove("TestPlaylistReShownAfterSearch", true);
            PlaylistsPage.Remove("TestPlaylistReShownAfterFilter", true);
        }

        #endregion

        [Test]
        public void UserCanAddPlaylists()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .When(x => PlaylistsPage.Add("TestPlaylistAdd", "TestDepartmentForPlaylistTests1", false), "When I add an item.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistAdd", true), "Then it is added to the system.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void UserCanEditPlaylists()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistEdit", "TestDepartmentForPlaylistTests1", true), "And the playlist \"TestPlaylistEdit\" exists.")
                .When(x => PlaylistsPage.EditName("TestPlaylistEdit"), "When I edit an item.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistEdit_Edited", true), "Then it is updated in the system.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void UserCanRemovePlaylists()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistRemove", "TestDepartmentForPlaylistTests1", true), "And the playlist \"TestPlaylistRemove\" exists.")
                .When(x => PlaylistsPage.Remove("TestPlaylistRemove", false), "When I remove an item.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistRemove", false), "Then it is no longer in the system.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void AddPlaylist_SpecifyDepartment_PlaylistIsAddedToSpecifiedDepartment()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .When(x => PlaylistsPage.Add("TestPlaylistDepartment", "TestDepartmentForPlaylistTests1", false), "When I add an item specifying the department.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistDepartment", true), "Then it is added to the system.")
                .And(x => PlaylistsPage.PlaylistIsAssignedToDepartment("TestPlaylistDepartment", "TestDepartmentForPlaylistTests1"), "And it is added to the correct department.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void PlaylistDepartment_EditDepartment_PlaylistDepartmentUpdated()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistDepartment", "TestDepartmentForPlaylistTests1", false), "And I add an item specifying the first department.")
                .And(x => PlaylistsPage.Contains("TestPlaylistDepartment", true), "And it is added to the system.")
                .And(x => PlaylistsPage.PlaylistIsAssignedToDepartment("TestPlaylistDepartment", "TestDepartmentForPlaylistTests1"), "And it is added to the correct department.")
                .When(x=>PlaylistsPage.EditPlaylistDepartment("TestPlaylistDepartment", "TestDepartmentForPlaylistTests2"),"When I edit the department of the playlist to the second department.")
                .Then(x => PlaylistsPage.PlaylistIsAssignedToDepartment("TestPlaylistDepartment", "TestDepartmentForPlaylistTests2"), "Then it is updated to the correct department.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void PlaylistsSearch_ApplySearchCriteria_ReleventItemsShownAndNonRelevantItemsRemoved()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistRelevant", "TestDepartmentForPlaylistTests1", true), "And the playlist \"TestPlaylistRelevant\" exists.")
                .And(x => PlaylistsPage.Add("TestPlaylistNonRelevant", "TestDepartmentForPlaylistTests1", true), "And the playlist \"TestPlaylistNonRelevant\" exists.")
                .When(x => PlaylistsPage.Search("TestPlaylistRelevant"), "When I search \"TestPlaylistRelevant\".")
                .Then(x => PlaylistsPage.Contains("TestPlaylistRelevant", true), "Then the playlist \"TestPlaylistRelevant\" is shown.")
                .And(x => PlaylistsPage.Contains("TestPlaylistNonRelevant", false), "And the playlist \"TestPlaylistNonRelevant\" is not shown.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void PlaylistsSearch_SearchCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistReShownAfterSearch", "TestDepartmentForPlaylistTests1", true), "And the playlist \"TestPlaylistReShownAfterSearch\" exists.")
                .And(x => PlaylistsPage.Add("TestPlaylistOther", "TestDepartmentForPlaylistTests1", true), "And the playlist \"TestPlaylistOther\" exists.")
                .And(x => PlaylistsPage.Search("TestPlaylistOther"), "And I search the name of another item.")
                .And(x => PlaylistsPage.Contains("TestPlaylistReShownAfterSearch", false), "And the playlist is no longer shown.")
                .When(x => PlaylistsPage.ClearSearch(), "When I clear the search Criteria.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistReShownAfterSearch", true), "Then the playlist is shown.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void PlaylistsSearch_FilterCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistReShownAfterFilter", "TestDepartmentForPlaylistTests1", true), "And the playlist \"TestPlaylistReShownAfterFilter\" exists.")
                .And(x => PlaylistsPage.Add("TestPlaylistOther", "TestDepartmentForPlaylistTests1", true), "And the playlist \"TestPlaylistOther\" exists.")
                .And(x => PlaylistsPage.Search("TestPlaylistOther"), "And I search the name of another item.")
                .And(x => PlaylistsPage.Contains("TestPlaylistReShownAfterFilter", false), "And the playlist is no longer shown.")
                .When(x => PlaylistsPage.ClearFilter(), "When I clear the filter.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistReShownAfterFilter", true), "Then the playlist is shown.")
                .BDDfy<PlaylistStory>();
        }
    }
}
