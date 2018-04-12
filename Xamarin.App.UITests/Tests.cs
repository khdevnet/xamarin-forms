using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Xamarin.App.UITests
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            // app.Repl();
            // Arrange - Nothing to do because the queries have already been initialized.
            AppResult[] result = app.Query(c => c.Marked("MyLabel").Text("Welcome to Xamarin.Forms!"));
            Assert.IsTrue(result.Any(), "The initial message string isn't correct - maybe the app wasn't re-started?");
           // app.Screenshot("First screen.");
        }

        [Test]
        public void GoToButtonTest()
        {
            // app.Repl();
            // Arrange - Nothing to do because the queries have already been initialized.
            app.Tap(c => c.Button().Text("Go to About"));
            AppResult[] result = app.Query(c => c.Text("Welcome to About Page!"));
            Assert.IsTrue(result.Any(), "The initial message string isn't correct - maybe the app wasn't re-started?");
            // app.Screenshot("First screen.");
        }
    }
}