using AdaptiveAds_TestFramework;
using AdaptiveAds_TestFramework.Helpers;
using AdaptiveAds_TestFramework.PageFrameworks;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Tests.Stories
{
    [TestFixture]
    [Story(AsA = "As the product owner",
            IWant = "I want the product to be login protected",
            SoThat = "So that only privileged users may access the system")]
    public class LoginStory
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
            {
                Driver.SignOut(errorIfAlreadySignedOut: false);
                Driver.GoTo(Location.Login);
            });
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }

        #endregion

        [Test]
        public void UserCanLoginWithCorrectCredentials()
        {
            this.Given(x => Driver.IsAt(Location.Login), "Given I am at the login page.")
                .When(x => LoginPage.LoginAs(ConfigData.Username)
                                    .WithPassword(ConfigData.Password)
                                    .Login(),
                                    "When I provide a correct username and password.")
                .Then(x => Driver.IsNotAt(Location.Login), "Then I should no longer be at the login page.")
                .And(x => Driver.IsAt(Location.Dashboard), "I should be at the Dashboard page.")
                .BDDfy<LoginStory>();
        }

        [Test]
        public void UserCantLoginWithIncorrectUsername()
        {
            this.Given(x => Driver.IsAt(Location.Login), "Given I am at the login page.")
                .When(x => LoginPage.LoginAs("Incorrect Username")
                                    .WithPassword(ConfigData.Password)
                                    .Login(),
                                    "When I provide an incorrect username.")
                .Then(x => Driver.IsAt(Location.Login), "Then I should still be at the login screen.")
                .And(x => LoginPage.ErrorMessage(true),
                          "And a message is shown to inform of an unsuccessful login attempt.")
                .BDDfy<LoginStory>();
        }

        [Test]
        public void UserCantLoginWithIncorrectPassword()
        {
            this.Given(x => Driver.IsAt(Location.Login), "Given I am at the login page.")
                .When(x => LoginPage.LoginAs(ConfigData.Username)
                                    .WithPassword("Incorrect Password")
                                    .Login(),
                                    "When I provide an incorrect password.")
                .Then(x => Driver.IsAt(Location.Login), "Then I should still be at the login screen.")
                .And(x => LoginPage.ErrorMessage(true),
                          "And a message is shown to inform of an unsuccessful login attempt.")
                .BDDfy<LoginStory>();
        }
    }
}
