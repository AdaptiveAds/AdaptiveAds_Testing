using System;
using System.Collections.ObjectModel;
using System.Threading;
using AdaptiveAds_TestFramework.Helpers;
using OpenQA.Selenium;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// Adverts page interaction framework, allows for items on the Adverts page to be interacted with and manipulated.
    /// </summary>
    public static class AdvertsPage
    {
        /// <summary>
        /// Adds an advert.
        /// </summary>
        /// <param name="advertName">Name of the advert to add.</param>
        /// <param name="departmentName">Name of the department to add the advert to. Leaving blank will add the new advert to the first department.</param>
        /// <param name="check">Asserts the advert is added to the system if true.</param>
        public static void Add(string advertName, string departmentName, bool check)
        {
            IWebElement addButton = Driver.Instance.FindElement(By.Name(ConfigData.AdvertAdd));
            addButton.Click();
            Thread.Sleep(1000);//wait for pop-up to open

            IWebElement nameInput = Driver.Instance.FindElement(By.Name(ConfigData.AdvertAddName));
            nameInput.SendKeys(advertName);
            Thread.Sleep(1000);//wait for text to be entered fully

            IWebElement departmentInput = Driver.Instance.FindElement(By.Name(ConfigData.AdvertAddDepartments));
            departmentInput.SendKeys(departmentName);
            Thread.Sleep(1000);//wait for text to be entered fully

            IWebElement saveButton = Driver.Instance.FindElement(By.Name(ConfigData.AdvertAddSave));
            saveButton.Click();
            Thread.Sleep(1000);//wait for pop-up to collapse

            //check
            if (check)
            {
                Contains(advertName, true);
            }
        }

        /// <summary>
        /// Validates whether a given advert exists.
        /// </summary>
        /// <param name="advertName">Name of the search advert.</param>
        /// <param name="doesContain">State to assert agents the advert existence.</param>
        /// <exception cref="InvalidElementStateException">Thrown if the advert was found when it was not expected to be.</exception>
        /// <exception cref="NoSuchElementException">Thrown if the advert was not found when it was expected to be.</exception>
        public static void Contains(string advertName, bool doesContain)
        {
            try
            {
                Driver.Instance.FindElement(By.Name(advertName));
                if (!doesContain)
                {
                    throw new InvalidElementStateException("Found the item " + advertName + " when not expected.");
                }
            }
            catch (NotFoundException e)
            {
                // Item was not found
                if (doesContain)
                {
                    throw new NoSuchElementException("Could not find item " + advertName + ".", e);
                }
            }
        }

        /// <summary>
        /// Finds the position of a given advert in the adverts list.
        /// </summary>
        /// <param name="advertName">Name of the advert to find.</param>
        /// <param name="throwIfNotFound">If true (default) then an exception will be thrown if not found, if false then -1 is returned.</param>
        /// <returns>Number representing the position of the advert in the list.(-1 if not found and throwIfNotFound is false.)</returns>
        /// <exception cref="NotFoundException">Thrown if the advert is not found and throwIfNotFound is true.</exception>
        public static int NumberInList(string advertName, bool throwIfNotFound = true)
        {
            int number = 1;

            IWebElement wrapper = Driver.Instance.FindElement(By.Name(ConfigData.AdvertContainer));
            string[] splitted = wrapper.Text.Split(new Char[] { '\n', '\r' });
            Collection<string> items = new Collection<String>();
            foreach (string s in splitted)
            {
                if (!s.Equals("Configure") && !s.Equals("Edit") && !s.Equals("") && !s.Equals("Delete"))
                {
                    items.Add(s);
                }
            }

            foreach (string s in items)
            {
                if (s.Equals(advertName))
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
        /// Edits the name of a advert.
        /// </summary>
        /// <param name="advertName">Name of the advert to change.</param>
        public static void EditName(string advertName)
        {
            OpenEditWindow(advertName);

            var nameInput = Driver.Instance.FindElement(By.Name(ConfigData.AdvertEditName));
            nameInput.Clear();
            nameInput.SendKeys(advertName + "_Edited");
            Thread.Sleep(1000);//wait for text to be entered fully

            var confirmButton = Driver.Instance.FindElement(By.Name(ConfigData.AdvertEditSave));
            confirmButton.Click();
            Thread.Sleep(1000);// wait for window to close
        }

        /// <summary>
        /// Opens the Editor window for a given advert.
        /// </summary>
        /// <param name="advertName">Name of the advert to open edit window for.</param>
        public static void OpenEditWindow(string advertName)
        {
            var editButtons = Driver.Instance.FindElements(By.Name(ConfigData.AdvertEdit));
            editButtons[NumberInList(advertName) - 1].Click();
            Thread.Sleep(1000);//wait for pop-up to become visible
        }

        /// <summary>
        /// Saves the Editor window.
        /// </summary>
        public static void SaveEditWindow()
        {
            IWebElement closeButton = Driver.Instance.FindElement(By.Name(ConfigData.AdvertEditSave));
            closeButton.Click();
            Thread.Sleep(1000);// wait for window to close
        }

        /// <summary>
        /// Switches the assigned department of a given advert.
        /// </summary>
        /// <param name="advertName">Name of the advert to edit department.</param>
        /// <param name="departmentName">Name of the department to switch to.</param>
        public static void EditAdvertDepartment(string advertName, string departmentName)
        {
            OpenEditWindow(advertName);

            IWebElement departmentInput = Driver.Instance.FindElement(By.Name(ConfigData.AdvertAddDepartments));
            departmentInput.SendKeys(departmentName);
            Thread.Sleep(1000); //wait for text to be input fully

            SaveEditWindow();
        }

        /// <summary>
        /// Removes a given advert.
        /// </summary>
        /// <param name="advertName">Name of the advert to remove.</param>
        /// <param name="check">Asserts the advert is removed if true.</param>
        public static void Remove(string advertName, bool check)
        {
            //delete
            var deleteButtons = Driver.Instance.FindElements(By.Name(ConfigData.AdvertDelete));
            int position = NumberInList(advertName, false);
            if (position != -1)
            {
                deleteButtons[position - 1].Click();
                Thread.Sleep(1000);//wait for pop-up to become visible

                var confirmButtons = Driver.Instance.FindElements(By.Name(ConfigData.AdvertDeleteConfirm));
                confirmButtons[1].Click();
                Thread.Sleep(1000);// wait for updated data
            }

            //check
            if (check)
            {
                Contains(advertName, false);
            }
        }

        /// <summary>
        /// Updates the Adverts search filter.
        /// </summary>
        /// <param name="searchCriteria">New search filter to apply.</param>
        public static void Search(string searchCriteria)
        {
            var searchBox = Driver.Instance.FindElement(By.Name(ConfigData.AdvertSearchBox));
            searchBox.Clear();
            searchBox.SendKeys(searchCriteria);
            searchBox.SendKeys(Keys.Return);
            Thread.Sleep(1000);// wait for updated data
        }

        /// <summary>
        /// Checks the given advert belongs to the given department.
        /// </summary>
        /// <param name="advertName">Name of the advert to find the depart of.</param>
        /// <param name="departmentName">Name of the department to compare with.</param>
        public static void AdvertIsAssignedToDepartment(string advertName, string departmentName)
        {
            OpenEditWindow(advertName);

            IWebElement departmentsSelection = Driver.Instance.FindElement(By.Name(ConfigData.AdvertEditDepartments));
            string assignedDepartment = "";
            int selected = 0;
            int.TryParse(departmentsSelection.GetAttribute("selectedIndex"), out selected);
            string[] split = departmentsSelection.Text.Split(new char[] { '\r', '\n' });
            int emptyOffset = 0;
            for (int i = 0; i < selected+1; i++)
            {
                if (string.IsNullOrWhiteSpace(split[i + emptyOffset]))
                {
                    emptyOffset++;
                    i--;
                }
                else
                {
                    if (i == selected)
                    {
                        assignedDepartment = split[i + emptyOffset];
                    }
                }
            }

            if (assignedDepartment == "")
            {
                throw new Exception("Department selection not found.");
            }

            if (!assignedDepartment.Equals(departmentName))
            {
                throw new Exception("Advert Is not assigned to that department, actual department is " + assignedDepartment + ".");
            }

            SaveEditWindow();//no changes are made so save is ok

        }

        /// <summary>
        /// Clears the search box used to filter Adverts.
        /// </summary>
        public static void ClearSearch()
        {
            var searchBox = Driver.Instance.FindElement(By.Name(ConfigData.AdvertSearchBox));
            searchBox.Clear();
            searchBox.SendKeys(Keys.Enter);
            Thread.Sleep(1000);// wait for updated data
        }

        /// <summary>
        /// Clears the filter on Adverts.
        /// </summary>
        public static void ClearFilter()
        {
            var clearFilterButton = Driver.Instance.FindElement(By.Name(ConfigData.AdvertClearFilterButton));
            clearFilterButton.Click();
            Thread.Sleep(1000);// wait for updated data
        }
    }
}
