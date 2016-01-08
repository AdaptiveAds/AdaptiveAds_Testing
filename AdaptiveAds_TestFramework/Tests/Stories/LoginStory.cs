using AdaptiveAds_TestFramework;
using AdaptiveAds_TestFramework.PageFrameworks;
using NUnit.Framework;
using TestStack.BDDfy;

namespace Tests.Stories
{
    [TestFixture]
    [Story( AsA="As the product owner",
            IWant="I want the product to be login protected",
            SoThat="So that only privalaged users may access the system")]
    public class LoginStory
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

        [Test]
        public void UserCanLoginWithCorrectCredentials()
        {
            LoginPage.GoTo();

            this.Given(x => Driver.IsAt(Location.Login), "Given I am at the login page.")
                .When(x => LoginPage.LoginAs("dev")
                                    .WithPassword("password")
                                    .Login(),
                                    "When I provide a correct username and password.")
                .Then(x => Driver.IsNotAt(Location.Login),"Then I should no longer be at the login page.")
                .And(x=>Driver.IsAt(Location.Dashboard),"I should be at the Dashboard page.")
                .BDDfy<LoginStory>();
        }

        [Test]
        public void UserCantLoginWithIncorrectUsername()
        {
            LoginPage.GoTo();

            this.Given(x => Driver.IsAt(Location.Login), "Given I am at the login page.")
                .When(x => LoginPage.LoginAs("deeeeev")
                                    .WithPassword("password")
                                    .Login(),
                                    "When I provide an incorrect username.")
                .Then(x => Driver.IsAt(Location.Login), "Then I should still be at the login screen.")
                .And(x => LoginPage.ShowingErrorMessage(),
                          "And a message is shown to inform of an unsuccessful login attempt.")
                .BDDfy<LoginStory>();
        }

        [Test]
        public void UserCantLoginWithIncorrectPassword()
        {
            LoginPage.GoTo();

            this.Given(x => Driver.IsAt(Location.Login), "Given I am at the login page.")
                .When(x => LoginPage.LoginAs("dev")
                                    .WithPassword("paaaaaaaasword")
                                    .Login(),
                                    "When I provide an incorrect password.")
                .Then(x => Driver.IsAt(Location.Login), "Then I should still be at the login screen.")
                .And(x => LoginPage.ShowingErrorMessage(),
                          "And a message is shown to inform of an unsuccessful login attempt.")
                .BDDfy<LoginStory>();            
        }
    }
}
