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

            Driver.GoTo(Location.Departments, true, false);
            DepartmentsPage.Remove("TestDepartmentForPlaylists", true);
            DepartmentsPage.Remove("TestDepartmentForPlaylists1", true);
            DepartmentsPage.Remove("TestDepartmentForPlaylists2", true);
        }

        #endregion

        [Test]
        public void UserCanAddPlaylists()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .When(x => PlaylistsPage.Add("TestPlaylistAdd", "", false), "When I add an item.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistAdd", true), "Then it is added to the system.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void UserCanEditPlaylists()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistEdit", "", true), "And the playlist \"TestPlaylistEdit\" exists.")
                .When(x => PlaylistsPage.EditName("TestPlaylistEdit"), "When I edit an item.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistEdit_Edited", true), "Then it is updated in the system.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void UserCanRemovePlaylists()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistRemove", "", true), "And the playlist \"TestPlaylistRemove\" exists.")
                .When(x => PlaylistsPage.Remove("TestPlaylistRemove", false), "When I remove an item.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistRemove", false), "Then it is no longer in the system.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void AddPlaylist_SpecifyDepartment_PlaylistIsAddedToSpecifiedDepartment()
        {
            this.Given(x => Driver.GoTo(Location.Departments, true, true), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentForPlaylists", false), "And I add a new test department.")
                .And(x => DepartmentsPage.Contains("TestDepartmentForPlaylists", true), "And it is successfully added to the system.")
                .When(x => Driver.GoTo(Location.Playlists, true, true), "When I go to the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistDepartment", "TestDepartmentForPlaylists", false), "And I add an item specifying the department.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistDepartment", true), "Then it is added to the system.")
                .And(x => PlaylistsPage.PlaylistIsAssignedToDepartment("TestPlaylistDepartment", "TestDepartmentForPlaylists"), "And it is added to the correct department.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void PlaylistDepartment_EditDepartment_PlaylistDepartmentUpdated()
        {
            this.Given(x => Driver.GoTo(Location.Departments, true, true), "Given I am at the Departments page.")
                .And(x => DepartmentsPage.Add("TestDepartmentForPlaylists1", false), "And I add a new test department.")
                .And(x => DepartmentsPage.Contains("TestDepartmentForPlaylists1", true), "And it is successfully added to the system.")
                .And(x => DepartmentsPage.Add("TestDepartmentForPlaylists2", false), "And I add another.")
                .And(x => DepartmentsPage.Contains("TestDepartmentForPlaylists2", true), "And it is also successfully added to the system.")
                .And(x => Driver.GoTo(Location.Playlists, true, true), "And I go to the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistDepartment", "TestDepartmentForPlaylists1", false), "And I add an item specifying the first department.")
                .And(x => PlaylistsPage.Contains("TestPlaylistDepartment", true), "And it is added to the system.")
                .And(x => PlaylistsPage.PlaylistIsAssignedToDepartment("TestPlaylistDepartment", "TestDepartmentForPlaylists1"), "And it is added to the correct department.")
                .When(x=>PlaylistsPage.EditPlaylistDepartment("TestPlaylistDepartment", "TestDepartmentForPlaylists2"),"When I edit the department of the playlist to the second department.")
                .Then(x => PlaylistsPage.PlaylistIsAssignedToDepartment("TestPlaylistDepartment", "TestDepartmentForPlaylists2"), "Then it is updated to the correct department.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void PlaylistsSearch_ApplySearchCriteria_ReleventItemsShownAndNonRelevantItemsRemoved()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistRelevant", "", true), "And the playlist \"TestPlaylistRelevant\" exists.")
                .And(x => PlaylistsPage.Add("TestPlaylistNonRelevant", "", true), "And the playlist \"TestPlaylistNonRelevant\" exists.")
                .When(x => PlaylistsPage.Search("TestPlaylistRelevant"), "When I search \"TestPlaylistRelevant\".")
                .Then(x => PlaylistsPage.Contains("TestPlaylistRelevant", true), "Then the playlist \"TestPlaylistRelevant\" is shown.")
                .And(x => PlaylistsPage.Contains("TestPlaylistNonRelevant", false), "And the playlist \"TestPlaylistNonRelevant\" is not shown.")
                .BDDfy<PlaylistStory>();
        }

        [Test]
        public void PlaylistsSearch_SearchCleared_NonRelevantItemsReShown()
        {
            this.Given(x => Driver.IsAt(Location.Playlists), "Given I am at the Playlists page.")
                .And(x => PlaylistsPage.Add("TestPlaylistReShownAfterSearch", "", true), "And the playlist \"TestPlaylistReShownAfterSearch\" exists.")
                .And(x => PlaylistsPage.Add("TestPlaylistOther", "", true), "And the playlist \"TestPlaylistOther\" exists.")
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
                .And(x => PlaylistsPage.Add("TestPlaylistReShownAfterFilter", "", true), "And the playlist \"TestPlaylistReShownAfterFilter\" exists.")
                .And(x => PlaylistsPage.Add("TestPlaylistOther", "", true), "And the playlist \"TestPlaylistOther\" exists.")
                .And(x => PlaylistsPage.Search("TestPlaylistOther"), "And I search the name of another item.")
                .And(x => PlaylistsPage.Contains("TestPlaylistReShownAfterFilter", false), "And the playlist is no longer shown.")
                .When(x => PlaylistsPage.ClearFilter(), "When I clear the filter.")
                .Then(x => PlaylistsPage.Contains("TestPlaylistReShownAfterFilter", true), "Then the playlist is shown.")
                .BDDfy<PlaylistStory>();
        }
    }
}
