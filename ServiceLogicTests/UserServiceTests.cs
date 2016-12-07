using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceLogic;
using ServiceLogic.Interfaces;

namespace ServiceLogicTests
{
    [TestClass]
    public class UserServiceTests
    {
        #region Bad behavior
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            UserService userService = new UserService();

            userService.AddUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserException))]
        public void Add_EmptyFirstName_InvalidUserThrown()
        {
            UserService userService = new UserService();

            userService.AddUser(new User("", "Kazlou", new DateTime(2000, 10, 14), Gender.Female));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidUserException))]
        public void Add_EmptyLastName_InvalidUserThrown()
        {
            UserService userService = new UserService();

            userService.AddUser(new User("Artsiom", "", new DateTime(2000, 10, 15), Gender.Female));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullUser_ExceptionThrown()
        {
            UserService userService = new UserService();
            userService.RemoveUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUserByPredicate_NullPredicate_ExceptionThrown()
        {
            UserService userService = new UserService();
            userService.GetUserByPredicate(null);
        }
        #endregion
        #region Good behavior
        [TestMethod]
        public void AddUser_GoodUser_GoodBehavior()
        {
            IRepository<User> repository = new UserRepository();
            UserService userService = new UserService(repository);
            User user = new User("Artyom", "Kozlov", new DateTime(2000, 09, 10), Gender.Male);
            Assert.AreEqual(user, userService.AddUser(user));
        }

        [TestMethod]
        public void AddUser_GoodUser_GoodGenerateId()
        {
            IRepository<User> repository = new UserRepository();
            UserService userService = new UserService(repository, i => i + 5);
            User firstUser = new User("Artyom", "Kozlov", new DateTime(2000, 09, 10), Gender.Female);
            User secondUser = new User("Artyom", "Kozlov", new DateTime(2000, 09, 10), Gender.Male);
            User thirdUser = new User("Artyom", "Kozlov", new DateTime(2000, 09, 10), Gender.Male);
            Assert.AreEqual(1, userService.AddUser(firstUser).Id);
            Assert.AreEqual(6, userService.AddUser(secondUser).Id);
            Assert.AreEqual(11, userService.AddUser(thirdUser).Id);
        }

        [TestMethod]
        public void GetUserById_GoodUser_GoodBehavior()
        {
            IRepository<User> repository = new UserRepository();
            UserService userService = new UserService(repository, i => i * 4);
            User firstUser = new User("Artyom", "Kozlov", new DateTime(2000, 09, 10), Gender.Male);
            User secondUser = new User("Artyom", "Kozlov", new DateTime(2000, 09, 10), Gender.Male);
            userService.AddUser(firstUser);
            userService.AddUser(secondUser);
            Assert.AreEqual(firstUser, userService.GetUserById(1));
            Assert.AreEqual(secondUser, userService.GetUserById(4));
        }

        [TestMethod]
        public void GetUserByPredicate_GoodUsers_GoodBehavior()
        {
            UserService userService = new UserService();
            List<User> listOfUsers = new List<User>();
            User firstUser = new User("Artyom", "Kozlov", new DateTime(2000, 09, 10), Gender.Female);
            User secondUser = new User("Nikita", "Kozlov", new DateTime(2000, 09, 10), Gender.Female);
            userService.AddUser(firstUser);
            userService.AddUser(secondUser);
            listOfUsers.Add(firstUser);
            Assert.AreEqual(listOfUsers[0], userService.GetUserByPredicate(m => m.FirstName == "Artyom").ToList()[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteUser_GoodUser_ArgumentOutOfRange()
        {
            UserService userService = new UserService();
            User firstUser = new User("Artyom", "Kozlov", new DateTime(2000, 09, 10), Gender.Male);
            userService.AddUser(firstUser);
            userService.RemoveUser(firstUser);
            userService.GetUserById(1);
        }
        #endregion
    }
}
