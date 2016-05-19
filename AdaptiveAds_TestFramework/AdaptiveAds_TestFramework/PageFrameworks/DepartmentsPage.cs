using System;
using System.Collections.ObjectModel;
using System.Threading;
using AdaptiveAds_TestFramework.Helpers;
using OpenQA.Selenium;

namespace AdaptiveAds_TestFramework.PageFrameworks
{
    /// <summary>
    /// Departments page interaction framework, allows for items on the Departments page to be interacted with and manipulated.
    /// </summary>
    public static class DepartmentsPage
    {
        /// <summary>
        /// Adds a department.
        /// </summary>
        /// <param name="departmentName">Name of the department to add.</param>
        /// <param name="check">Asserts the department is added to the system if true.</param>
        public static void Add(string departmentName, bool check)
        {
            IWebElement addButton = Driver.Instance.FindElement(By.Name(ConfigData.DepartmentAdd));
            addButton.Click();
            IWebElement nameInput = Driver.Instance.FindElement(By.Name(ConfigData.DepartmentAddName));
            nameInput.SendKeys(departmentName);
            IWebElement saveButton = Driver.Instance.FindElement(By.Name(ConfigData.DepartmentAddSave));
            saveButton.Click();

            //check
            if (check)
            {
                Contains(departmentName,true);
            }
        }

        /// <summary>
        /// Validates whether a given department exists.
        /// </summary>
        /// <param name="departmentName">Name of the search department.</param>
        /// <param name="doesContain">state to assert agents the departments existence.</param>
        /// <exception cref="InvalidElementStateException">Thrown if the department was not expected to be found.</exception>
        /// <exception cref="NoSuchElementException">Thrown if the department was not found when it was expected to be.</exception>
        public static void Contains(string departmentName, bool doesContain)
        {
            try
            {
                Driver.Instance.FindElement(By.Name(departmentName));
                if (!doesContain)
                {
                    throw new InvalidElementStateException("Found the item " + departmentName + " when not expected.");
                }
            }
            catch (NotFoundException e)
            {
                // Item was not found
                if (doesContain)
                {
                    throw new NoSuchElementException("Could not find item " + departmentName + ".",e);
                }
            }
        }

        /// <summary>
        /// Finds the position of a given department in the departments list.
        /// </summary>
        /// <param name="departmentName">Name of the department to find.</param>
        /// <returns>Number representing the position of the department in the list.</returns>
        /// <exception cref="NotFoundException">Thrown if the department is not found.</exception>
        public static int NumberInList(string departmentName)
        {
            int number = 1;

            IWebElement wrapper = Driver.Instance.FindElement(By.Name(ConfigData.DepartmentsContainer));
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
                if (s.Equals(departmentName))
                {
                    return number;
                }
                number++;
            }
            throw new NotFoundException("Item was not found");
        }

        /// <summary>
        /// Edits the name of a department.
        /// </summary>
        /// <param name="departmentName">Name of the department to change.</param>
        public static void Edit(string departmentName)
        {
            var editButtons = Driver.Instance.FindElements(By.Name(ConfigData.DepartmentEdit));
            editButtons[NumberInList(departmentName)-1].Click();

            Thread.Sleep(250);//wait for pop-up to become visible

            var nameInput = Driver.Instance.FindElement(By.Name(ConfigData.DepartmentEditName));
            nameInput.Clear();
            nameInput.SendKeys(departmentName + "_Edited");

            Thread.Sleep(500);//wait for text to be entered fully

            var confirmButton = Driver.Instance.FindElement(By.Name(ConfigData.DepartmentEditSave));
            confirmButton.Click();
        }

        /// <summary>
        /// Removes a given department.
        /// </summary>
        /// <param name="departmentName">Name of the department to remove.</param>
        /// <param name="check">Asserts the department is removed if true.</param>
        public static void Remove(string departmentName, bool check)
        {
            //delete
            var deleteButtons = Driver.Instance.FindElements(By.Name(ConfigData.DepartmentDelete));
            deleteButtons[NumberInList(departmentName)-1].Click();

            Thread.Sleep(500);

            var confirmButtons = Driver.Instance.FindElements(By.Name(ConfigData.DepartmentDeleteConfirm));
            confirmButtons[1].Click();

            //check
            if (check)
            {
                Contains(departmentName, false);
            }
        }
    }
}
