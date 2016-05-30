using System;
using System.Collections.ObjectModel;
using System.Threading;
using AdaptiveAds_TestFramework.Helpers;
using OpenQA.Selenium;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// Backgrounds page interaction framework, allows for items on the Backgrounds page to be interacted with and manipulated.
    /// </summary>
    public static class BackgroundsPage
    {
        /// <summary>
        /// Adds a background.
        /// </summary>
        /// <param name="backgroundName">Name of the background to add.</param>
        /// <param name="check">Asserts the background is added to the system if true.</param>
        public static void Add(string backgroundName, bool check)
        {
            IWebElement addButton = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundAdd));
            addButton.Click();
            Thread.Sleep(500);//wait for pop-up to open

            IWebElement nameInput = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundAddName));
            nameInput.SendKeys(backgroundName);
            Thread.Sleep(500);//wait for text to be entered fully

            IWebElement saveButton = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundAddSave));
            saveButton.Click();
            Thread.Sleep(500);//wait for pop-up to collapse

            //check
            if (check)
            {
                Contains(backgroundName, true);
            }
        }

        /// <summary>
        /// Validates whether a given background exists.
        /// </summary>
        /// <param name="backgroundName">Name of the search background.</param>
        /// <param name="doesContain">State to assert agents the background existence.</param>
        /// <exception cref="InvalidElementStateException">Thrown if the background was found when it was not expected to be.</exception>
        /// <exception cref="NoSuchElementException">Thrown if the background was not found when it was expected to be.</exception>
        public static void Contains(string backgroundName, bool doesContain)
        {
            try
            {
                Driver.Instance.FindElement(By.Name(backgroundName));
                if (!doesContain)
                {
                    throw new InvalidElementStateException("Found the item " + backgroundName + " when not expected.");
                }
            }
            catch (NotFoundException e)
            {
                // Item was not found
                if (doesContain)
                {
                    throw new NoSuchElementException("Could not find item " + backgroundName + ".", e);
                }
            }
        }

        /// <summary>
        /// Finds the position of a given background in the backgrounds list.
        /// </summary>
        /// <param name="backgroundName">Name of the background to find.</param>
        /// <param name="throwIfNotFound">If true (default) then an exception will be thrown if not found, if false then -1 is returned.</param>
        /// <returns>Number representing the position of the background in the list.(-1 if not found and throwIfNotFound is false.)</returns>
        /// <exception cref="NotFoundException">Thrown if the background is not found and throwIfNotFound is true.</exception>
        public static int NumberInList(string backgroundName, bool throwIfNotFound = true)
        {
            int number = 1;

            IWebElement wrapper = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundContainer));
            string[] splitted = wrapper.Text.Split(new Char[] { '\n', '\r' });
            Collection<string> items = new Collection<String>();
            foreach (string s in splitted)
            {
                if (!s.Equals("Edit") && !s.Equals("") && !s.Equals("Delete"))
                {
                    items.Add(s);
                }
            }

            foreach (string s in items)
            {
                if (s.Equals(backgroundName))
                {
                    return number;
                }
                number++;
            }
            if (throwIfNotFound)
            {
                throw new NotFoundException("Item was not found");
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Edits the name of a background.
        /// </summary>
        /// <param name="backgroundName">Name of the background to change.</param>
        public static void Edit(string backgroundName)
        {
            OpenEditWindow(backgroundName);

            var nameInput = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundEditName));
            nameInput.Clear();
            nameInput.SendKeys(backgroundName + "_Edited");
            Thread.Sleep(500);//wait for text to be entered fully

            var confirmButton = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundEditSave));
            confirmButton.Click();
            Thread.Sleep(500);// wait for window to close
        }

        /// <summary>
        /// Opens the Editor window for a given background.
        /// </summary>
        /// <param name="backgroundName">Name of the background to open edit window for.</param>
        public static void OpenEditWindow(string backgroundName)
        {
            var editButtons = Driver.Instance.FindElements(By.Name(ConfigData.BackgroundEdit));
            editButtons[NumberInList(backgroundName) - 1].Click();
            Thread.Sleep(500);//wait for pop-up to become visible
        }

        /// <summary>
        /// Saves the Editor window.
        /// </summary>
        public static void SaveEditWindow()
        {
            IWebElement closeButton = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundEditSave));
            closeButton.Click();
            Thread.Sleep(500);// wait for window to close
        }

        /// <summary>
        /// Removes a given background.
        /// </summary>
        /// <param name="backgroundName">Name of the background to remove.</param>
        /// <param name="check">Asserts the background is removed if true.</param>
        public static void Remove(string backgroundName, bool check)
        {
            //delete
            var deleteButtons = Driver.Instance.FindElements(By.Name(ConfigData.BackgroundDelete));
            int position = NumberInList(backgroundName, false);
            if (position != -1)
            {
                deleteButtons[position - 1].Click();
                Thread.Sleep(500);//wait for pop-up to become visible

                var confirmButtons = Driver.Instance.FindElements(By.Name(ConfigData.BackgroundDeleteConfirm));
                confirmButtons[1].Click();
                Thread.Sleep(500);// wait for updated data
            }

            //check
            if (check)
            {
                Contains(backgroundName, false);
            }
        }

        /// <summary>
        /// Updates the Backgrounds search filter.
        /// </summary>
        /// <param name="searchCriteria">New search filter to apply.</param>
        public static void Search(string searchCriteria)
        {
            var searchBox = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundSearchBox));
            searchBox.Clear();
            searchBox.SendKeys(searchCriteria);
            searchBox.SendKeys(Keys.Return);
            Thread.Sleep(500);// wait for updated data
        }

        /// <summary>
        /// Clears the search box used to filter Backgrounds.
        /// </summary>
        public static void ClearSearch()
        {
            var searchBox = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundSearchBox));
            searchBox.Clear();
            searchBox.SendKeys(Keys.Enter);
            Thread.Sleep(500);// wait for updated data
        }

        /// <summary>
        /// Clears the filter on Backgrounds.
        /// </summary>
        public static void ClearFilter()
        {
            var clearFilterButton = Driver.Instance.FindElement(By.Name(ConfigData.BackgroundClearFilterButton));
            clearFilterButton.Click();
            Thread.Sleep(500);// wait for updated data
        }
    }
}
