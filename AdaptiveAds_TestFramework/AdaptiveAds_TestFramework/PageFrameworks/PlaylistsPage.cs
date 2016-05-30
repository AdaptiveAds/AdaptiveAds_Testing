using System;
using System.Collections.ObjectModel;
using System.Threading;
using AdaptiveAds_TestFramework.Helpers;
using OpenQA.Selenium;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// Playlists page interaction framework, allows for items on the Playlists page to be interacted with and manipulated.
    /// </summary>
    public static class PlaylistsPage
    {
        /// <summary>
        /// Adds a playlist.
        /// </summary>
        /// <param name="playlistName">Name of the playlist to add.</param>
        /// <param name="departmentName">Name of the department to add the playlist to. Leaving blank will add the new playlist to the first department.</param>
        /// <param name="check">Asserts the playlist is added to the system if true.</param>
        public static void Add(string playlistName, string departmentName, bool check)
        {
            IWebElement addButton = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistAdd));
            addButton.Click();
            Thread.Sleep(500);//wait for pop-up to open

            IWebElement nameInput = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistAddName));
            nameInput.SendKeys(playlistName);
            Thread.Sleep(500);//wait for text to be entered fully

            IWebElement departmentInput = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistAddDepartments));
            departmentInput.SendKeys(departmentName);
            Thread.Sleep(500);//wait for text to be entered fully

            IWebElement saveButton = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistAddSave));
            saveButton.Click();
            Thread.Sleep(500);//wait for pop-up to collapse

            //check
            if (check)
            {
                Contains(playlistName, true);
            }
        }

        /// <summary>
        /// Validates whether a given playlist exists.
        /// </summary>
        /// <param name="playlistName">Name of the search playlist.</param>
        /// <param name="doesContain">State to assert agents the playlist existence.</param>
        /// <exception cref="InvalidElementStateException">Thrown if the playlist was found when it was not expected to be.</exception>
        /// <exception cref="NoSuchElementException">Thrown if the playlist was not found when it was expected to be.</exception>
        public static void Contains(string playlistName, bool doesContain)
        {
            try
            {
                Driver.Instance.FindElement(By.Name(playlistName));
                if (!doesContain)
                {
                    throw new InvalidElementStateException("Found the item " + playlistName + " when not expected.");
                }
            }
            catch (NotFoundException e)
            {
                // Item was not found
                if (doesContain)
                {
                    throw new NoSuchElementException("Could not find item " + playlistName + ".", e);
                }
            }
        }

        /// <summary>
        /// Finds the position of a given playlist in the playlists list.
        /// </summary>
        /// <param name="playlistName">Name of the playlist to find.</param>
        /// <param name="throwIfNotFound">If true (default) then an exception will be thrown if not found, if false then -1 is returned.</param>
        /// <returns>Number representing the position of the playlist in the list.(-1 if not found and throwIfNotFound is false.)</returns>
        /// <exception cref="NotFoundException">Thrown if the playlist is not found and throwIfNotFound is true.</exception>
        public static int NumberInList(string playlistName, bool throwIfNotFound = true)
        {
            int number = 1;

            IWebElement wrapper = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistContainer));
            string[] splitted = wrapper.Text.Split(new Char[] { '\n', '\r' });
            Collection<string> items = new Collection<String>();
            foreach (string s in splitted)
            {
                if (!s.Equals("Edit") && !s.Equals("Design") && !s.Equals("") && !s.Equals("Delete"))
                {
                    items.Add(s);
                }
            }

            foreach (string s in items)
            {
                if (s.Equals(playlistName))
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

        private static int NumberInListDeleteable(string playlistName, bool throwIfNotFound = true)
        {
            int number = 1;

            IWebElement wrapper = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistContainer));
            string[] splitted = wrapper.Text.Split(new Char[] { '\n', '\r' });
            Collection<string> items = new Collection<String>();
            foreach (string s in splitted)
            {
                if (!s.Equals("Edit") && !s.Equals("Design") && !s.Equals("") && !s.Equals("Delete") && !s.Equals("Global Playlist"))
                {
                    items.Add(s);
                }
            }

            foreach (string s in items)
            {
                if (s.Equals(playlistName))
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
        /// Edits the name of a playlist.
        /// </summary>
        /// <param name="playlistName">Name of the playlist to change.</param>
        public static void EditName(string playlistName)
        {
            OpenEditWindow(playlistName);

            var nameInput = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistEditName));
            nameInput.Clear();
            nameInput.SendKeys(playlistName + "_Edited");
            Thread.Sleep(500);//wait for text to be entered fully

            var confirmButton = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistEditSave));
            confirmButton.Click();
            Thread.Sleep(500);// wait for window to close
        }

        /// <summary>
        /// Opens the Editor window for a given playlist.
        /// </summary>
        /// <param name="playlistName">Name of the playlist to open edit window for.</param>
        public static void OpenEditWindow(string playlistName)
        {
            var editButtons = Driver.Instance.FindElements(By.Name(ConfigData.PlaylistEdit));
            editButtons[NumberInList(playlistName) - 1].Click();
            Thread.Sleep(500);//wait for pop-up to become visible
        }

        /// <summary>
        /// Saves the Editor window.
        /// </summary>
        public static void SaveEditWindow()
        {
            IWebElement closeButton = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistEditSave));
            closeButton.Click();
            Thread.Sleep(500);// wait for window to close
        }

        /// <summary>
        /// Switches the assigned department of a given playlist.
        /// </summary>
        /// <param name="playlistName">Name of the playlist to edit department.</param>
        /// <param name="departmentName">Name of the department to switch to.</param>
        public static void EditPlaylistDepartment(string playlistName, string departmentName)
        {
            OpenEditWindow(playlistName);

            IWebElement departmentInput = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistAddDepartments));
            departmentInput.SendKeys(departmentName);
            Thread.Sleep(500); //wait for text to be input fully

            SaveEditWindow();
        }

        /// <summary>
        /// Removes a given playlist.
        /// </summary>
        /// <param name="playlistName">Name of the playlist to remove.</param>
        /// <param name="check">Asserts the playlist is removed if true.</param>
        public static void Remove(string playlistName, bool check)
        {
            //delete
            var deleteButtons = Driver.Instance.FindElements(By.Name(ConfigData.PlaylistDelete));
            int position = NumberInListDeleteable(playlistName, false);
            if (position != -1)
            {
                deleteButtons[position - 1].Click();
                Thread.Sleep(500);//wait for pop-up to become visible

                var confirmButtons = Driver.Instance.FindElements(By.Name(ConfigData.PlaylistDeleteConfirm));
                confirmButtons[1].Click();
                Thread.Sleep(500);// wait for updated data
            }

            //check
            if (check)
            {
                Contains(playlistName, false);
            }
        }

        /// <summary>
        /// Updates the Playlists search filter.
        /// </summary>
        /// <param name="searchCriteria">New search filter to apply.</param>
        public static void Search(string searchCriteria)
        {
            var searchBox = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistSearchBox));
            searchBox.Clear();
            searchBox.SendKeys(searchCriteria);
            Thread.Sleep(500); //wait for text to be input fully
            searchBox.SendKeys(Keys.Return);
            Thread.Sleep(500);// wait for updated data
        }

        /// <summary>
        /// Checks the given playlist belongs to the given department.
        /// </summary>
        /// <param name="playlistName">Name of the playlist to find the depart of.</param>
        /// <param name="departmentName">Name of the department to compare with.</param>
        public static void PlaylistIsAssignedToDepartment(string playlistName, string departmentName)
        {
            OpenEditWindow(playlistName);

            IWebElement departmentsSelection = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistEditDepartments));
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
                throw new Exception("Playlist Is not assigned to that department, actual department is " + assignedDepartment + ".");
            }

            SaveEditWindow();//no changes are made so save is ok

        }

        /// <summary>
        /// Clears the search box used to filter Playlists.
        /// </summary>
        public static void ClearSearch()
        {
            var searchBox = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistSearchBox));
            searchBox.Clear();
            searchBox.SendKeys(Keys.Enter);
            Thread.Sleep(500);// wait for updated data
        }

        /// <summary>
        /// Clears the filter on Playlists.
        /// </summary>
        public static void ClearFilter()
        {
            var clearFilterButton = Driver.Instance.FindElement(By.Name(ConfigData.PlaylistClearFilterButton));
            clearFilterButton.Click();
            Thread.Sleep(500);// wait for updated data
        }
    }
}
